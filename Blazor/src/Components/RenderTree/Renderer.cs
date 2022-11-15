// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Wangkanai.Blazor.Components.HotReload;
using Wangkanai.Blazor.Components.Reflection;
using Wangkanai.Blazor.Components.Rendering;
using Wangkanai.Internal;

namespace Wangkanai.Blazor.Components.RenderTree;

public abstract partial class Renderer : IDisposable, IAsyncDisposable
{
    private readonly IServiceProvider                       _serviceProvider;
    private readonly Dictionary<int, ComponentState>        _componentStateById         = new Dictionary<int, ComponentState>();
    private readonly Dictionary<IComponent, ComponentState> _componentStateByComponent  = new Dictionary<IComponent, ComponentState>();
    private readonly RenderBatchBuilder                     _batchBuilder               = new RenderBatchBuilder();
    private readonly Dictionary<ulong, EventCallback>       _eventBindings              = new Dictionary<ulong, EventCallback>();
    private readonly Dictionary<ulong, ulong>               _eventHandlerIdReplacements = new Dictionary<ulong, ulong>();
    private readonly ILogger<Renderer>                      _logger;
    private readonly ComponentFactory                       _componentFactory;
    private          Dictionary<int, ParameterView>?        _rootComponentsLatestParameters;
    private          Task?                                  _ongoingQuiescenceTask;

    private int         _nextComponentId;
    private bool        _isBatchInProgress;
    private ulong       _lastEventHandlerId;
    private List<Task>? _pendingTasks;
    private Task?       _disposeTask;
    private bool        _rendererIsDisposed;

    private bool _hotReloadInitialized;

    public event UnhandledExceptionEventHandler UnhandledSynchronizationException
    {
        add => Dispatcher.UnhandledException += value;
        remove => Dispatcher.UnhandledException -= value;
    }

    public Renderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        : this(serviceProvider, loggerFactory, GetComponentActivatorOrDefault(serviceProvider))
    {
        // This overload is provided for back-compatibility
    }

    public Renderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IComponentActivator componentActivator)
    {
        if (serviceProvider is null)
            throw new ArgumentNullException(nameof(serviceProvider));

        if (loggerFactory is null)
            throw new ArgumentNullException(nameof(loggerFactory));

        if (componentActivator is null)
            throw new ArgumentNullException(nameof(componentActivator));

        _serviceProvider  = serviceProvider;
        _logger           = loggerFactory.CreateLogger<Renderer>();
        _componentFactory = new ComponentFactory(componentActivator);
    }

    internal HotReloadManager HotReloadManager { get; set; } = HotReloadManager.Default;

    private static IComponentActivator GetComponentActivatorOrDefault(IServiceProvider serviceProvider)
        => serviceProvider.GetService<IComponentActivator>()
           ?? DefaultComponentActivator.Instance;

    public abstract Dispatcher Dispatcher { get; }

    protected internal ElementReferenceContext? ElementReferenceContext { get; protected set; }

    internal bool IsRenderingOnMetadataUpdate { get; private set; }

    internal bool Disposed => _rendererIsDisposed;

    private async void RenderRootComponentsOnHotReload()
    {
        ComponentFactory.ClearCache();
        ComponentProperties.ClearCache();
        Routing.QueryParameterValueSupplier.ClearCache();

        await Dispatcher.InvokeAsync(() =>
        {
            if (_rootComponentsLatestParameters is null)
                return;

            IsRenderingOnMetadataUpdate = true;
            try
            {
                foreach (var (componentId, parameters) in _rootComponentsLatestParameters)
                {
                    var componentState = GetRequiredComponentState(componentId);
                    componentState.SetDirectParameters(parameters);
                }
            }
            finally
            {
                IsRenderingOnMetadataUpdate = false;
            }
        });
    }

    protected IComponent InstantiateComponent([DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
        => _componentFactory.InstantiateComponent(_serviceProvider, componentType);

    protected internal int AssignRootComponentId(IComponent component)
    {
        if (!_hotReloadInitialized)
        {
            _hotReloadInitialized = true;
            if (HotReloadManager.MetadataUpdateSupported)
                HotReloadManager.OnDeltaApplied += RenderRootComponentsOnHotReload;
        }

        return AttachAndInitComponent(component, -1).ComponentId;
    }

    protected ArrayRange<RenderTreeFrame> GetCurrentRenderTreeFrames(int componentId) => GetRequiredComponentState(componentId).CurrentRenderTree.GetFrames();

    protected Task RenderRootComponentAsync(int componentId)
        => RenderRootComponentAsync(componentId, ParameterView.Empty);

    protected internal async Task RenderRootComponentAsync(int componentId, ParameterView initialParameters)
    {
        Dispatcher.AssertAccess();

        _pendingTasks ??= new();

        var componentState = GetRequiredRootComponentState(componentId);
        if (HotReloadManager.MetadataUpdateSupported)
        {
            _rootComponentsLatestParameters              ??= new();
            _rootComponentsLatestParameters[componentId] =   initialParameters.Clone();
        }

        componentState.SetDirectParameters(initialParameters);

        await WaitForQuiescence();
        Debug.Assert(_pendingTasks == null);
    }

    protected internal void RemoveRootComponent(int componentId)
    {
        Dispatcher.AssertAccess();

        _ = GetRequiredRootComponentState(componentId);

        _batchBuilder.ComponentDisposalQueue.Enqueue(componentId);
        if (HotReloadManager.MetadataUpdateSupported)
            _rootComponentsLatestParameters?.Remove(componentId);

        ProcessRenderQueue();
    }

    internal Type GetRootComponentType(int componentId)
        => GetRequiredRootComponentState(componentId).Component.GetType();

    protected abstract void HandleException(Exception exception);

    private async Task WaitForQuiescence()
    {
        if (_ongoingQuiescenceTask is not null)
        {
            await _ongoingQuiescenceTask;
            return;
        }

        try
        {
            _ongoingQuiescenceTask = ProcessAsynchronousWork();
            await _ongoingQuiescenceTask;
        }
        finally
        {
            Debug.Assert(_pendingTasks is null || _pendingTasks.Count == 0);
            _pendingTasks          = null;
            _ongoingQuiescenceTask = null;
        }

        async Task ProcessAsynchronousWork()
        {
            while (_pendingTasks?.Count > 0)
            {
                var pendingWork = Task.WhenAll(_pendingTasks);

                _pendingTasks.Clear();

                await pendingWork;
            }
        }
    }

    private ComponentState AttachAndInitComponent(IComponent component, int parentComponentId)
    {
        var componentId          = _nextComponentId++;
        var parentComponentState = GetOptionalComponentState(parentComponentId);
        var componentState       = new ComponentState(this, componentId, component, parentComponentState);
        //Log.InitializingComponent(_logger, componentState, parentComponentState);
        _componentStateById.Add(componentId, componentState);
        _componentStateByComponent.Add(component, componentState);
        component.Attach(new RenderHandle(this, componentId));
        return componentState;
    }
    
    protected abstract Task UpdateDisplayAsync(in RenderBatch renderBatch);
    
    public virtual Task DispatchEventAsync(ulong eventHandlerId, EventFieldInfo? fieldInfo, EventArgs eventArgs)
    {
        Dispatcher.AssertAccess();

        var callback = GetRequiredEventCallback(eventHandlerId);
        //Log.HandlingEvent(_logger, eventHandlerId, eventArgs);
        
        ComponentState? receiverComponentState = null;
        if (callback.Receiver is IComponent receiverComponent) // The receiver might be null or not an IComponent
            _componentStateByComponent.TryGetValue(receiverComponent, out receiverComponentState);

        if (fieldInfo != null)
        {
            var latestEquivalentEventHandlerId = FindLatestEventHandlerIdInChain(eventHandlerId);
            UpdateRenderTreeToMatchClientState(latestEquivalentEventHandlerId, fieldInfo);
        }

        Task? task = null;
        try
        {
            _isBatchInProgress = true;

            task = callback.InvokeAsync(eventArgs);
        }
        catch (Exception e)
        {
            HandleExceptionViaErrorBoundary(e, receiverComponentState);
            return Task.CompletedTask;
        }
        finally
        {
            _isBatchInProgress = false;

            ProcessPendingRender();
        }
        
        var result = GetErrorHandledTask(task, receiverComponentState);
        return result;
    }
    
    public Type GetEventArgsType(ulong eventHandlerId)
    {
        var methodInfo = GetRequiredEventCallback(eventHandlerId).Delegate?.Method;
        
        return methodInfo == null
                   ? typeof(EventArgs)
                   : EventArgsTypeCache.GetEventArgsType(methodInfo);
    }

    internal void InstantiateChildComponentOnFrame(ref RenderTreeFrame frame, int parentComponentId)
    {
        if (frame.FrameTypeField != RenderTreeFrameType.Component)
            throw new ArgumentException($"The frame's {nameof(RenderTreeFrame.FrameType)} property must equal {RenderTreeFrameType.Component}", nameof(frame));

        if (frame.ComponentStateField != null)
            throw new ArgumentException($"The frame already has a non-null component instance", nameof(frame));

        var newComponent      = InstantiateComponent(frame.ComponentTypeField);
        var newComponentState = AttachAndInitComponent(newComponent, parentComponentId);
        frame.ComponentStateField = newComponentState;
        frame.ComponentIdField    = newComponentState.ComponentId;
    }

    internal void AddToPendingTasks(Task task, ComponentState? owningComponentState)
    {
        switch (task == null ? TaskStatus.RanToCompletion : task.Status)
        {
            case TaskStatus.RanToCompletion:
            case TaskStatus.Canceled:
                break;
            case TaskStatus.Faulted:
                var baseException = task.Exception.GetBaseException();
                HandleExceptionViaErrorBoundary(baseException, owningComponentState);
                break;
            default:
                var handledErrorTask = GetErrorHandledTask(task, owningComponentState);

                _pendingTasks?.Add(handledErrorTask);

                break;
        }
    }

    internal void AssignEventHandlerId(ref RenderTreeFrame frame)
    {
        var id = ++_lastEventHandlerId;

        if (frame.AttributeValueField is EventCallback callback)
            _eventBindings.Add(id, callback);
        else if (frame.AttributeValueField is MulticastDelegate @delegate) 
            _eventBindings.Add(id, new EventCallback(@delegate.Target as IHandleEvent, @delegate));
        
        frame.AttributeEventHandlerIdField = id;
    }
    
    internal void AddToRenderQueue(int componentId, RenderFragment renderFragment)
    {
        Dispatcher.AssertAccess();

        var componentState = GetOptionalComponentState(componentId);
        if (componentState == null)
            return;

        _batchBuilder.ComponentRenderQueue.Enqueue(
            new RenderQueueEntry(componentState, renderFragment));

        if (!_isBatchInProgress) 
            ProcessPendingRender();
    }

    internal void TrackReplacedEventHandlerId(ulong oldEventHandlerId, ulong newEventHandlerId)
    {
        _eventHandlerIdReplacements.Add(oldEventHandlerId, newEventHandlerId);
    }

    private EventCallback GetRequiredEventCallback(ulong eventHandlerId)
    {
        if (!_eventBindings.TryGetValue(eventHandlerId, out var callback))
        {
            throw new ArgumentException($"There is no event handler associated with this event. EventId: '{eventHandlerId}'.", nameof(eventHandlerId));
        }

        return callback;
    }

    private ulong FindLatestEventHandlerIdInChain(ulong eventHandlerId)
    {
        while (_eventHandlerIdReplacements.TryGetValue(eventHandlerId, out var replacementEventHandlerId))
        {
            eventHandlerId = replacementEventHandlerId;
        }

        return eventHandlerId;
    }

    private ComponentState GetRequiredComponentState(int componentId)
        => _componentStateById.TryGetValue(componentId, out var componentState)
               ? componentState
               : throw new ArgumentException($"The renderer does not have a component with ID {componentId}.");

    private ComponentState GetOptionalComponentState(int componentId)
        => _componentStateById.TryGetValue(componentId, out var componentState)
               ? componentState
               : null;

    private ComponentState GetRequiredRootComponentState(int componentId)
    {
        var componentState = GetRequiredComponentState(componentId);
        if (componentState.ParentComponentState is not null)
        {
            throw new InvalidOperationException("The specified component is not a root component");
        }

        return componentState;
    }

    protected virtual void ProcessPendingRender()
    {
        if (_rendererIsDisposed)
            return;

        ProcessRenderQueue();
    }

    private void ProcessRenderQueue()
    {
        Dispatcher.AssertAccess();

        if (_isBatchInProgress)
            throw new InvalidOperationException("Cannot start a batch when one is already in progress.");

        _isBatchInProgress = true;
        var updateDisplayTask = Task.CompletedTask;

        try
        {
            if (_batchBuilder.ComponentRenderQueue.Count == 0)
                if (_batchBuilder.ComponentDisposalQueue.Count == 0)
                    return;
                else
                    ProcessDisposalQueueInExistingBatch();
            
            while (_batchBuilder.ComponentRenderQueue.Count > 0)
            {
                var nextToRender = _batchBuilder.ComponentRenderQueue.Dequeue();
                RenderInExistingBatch(nextToRender);
            }

            var batch = _batchBuilder.ToBatch();
            updateDisplayTask = UpdateDisplayAsync(batch);
            
            _ = InvokeRenderCompletedCalls(batch.UpdatedComponents, updateDisplayTask);
        }
        catch (Exception e)
        {
            HandleException(e);
            return;
        }
        finally
        {
            RemoveEventHandlerIds(_batchBuilder.DisposedEventHandlerIds.ToRange(), updateDisplayTask);
            _batchBuilder.ClearStateForCurrentBatch();
            _isBatchInProgress = false;
        }

        if (_batchBuilder.ComponentRenderQueue.Count > 0)
            ProcessRenderQueue();
    }

    private Task InvokeRenderCompletedCalls(ArrayRange<RenderTreeDiff> updatedComponents, Task updateDisplayTask)
    {
        if (updateDisplayTask.IsCanceled)
            return Task.CompletedTask;

        if (updateDisplayTask.IsFaulted)
        {
            // The display update failed so we don't care any more about running on render completed
            // fallbacks as the entire rendering process is going to be torn down.
            HandleException(updateDisplayTask.Exception);
            return Task.CompletedTask;
        }

        if (!updateDisplayTask.IsCompleted)
        {
            var updatedComponentsId    = new int[updatedComponents.Count];
            var updatedComponentsArray = updatedComponents.Array;
            for (int i = 0; i < updatedComponentsId.Length; i++)
                updatedComponentsId[i] = updatedComponentsArray[i].ComponentId;

            return InvokeRenderCompletedCallsAfterUpdateDisplayTask(updateDisplayTask, updatedComponentsId);
        }

        List<Task> batch = null;
        var        array = updatedComponents.Array;
        for (var i = 0; i < updatedComponents.Count; i++)
        {
            var componentState = GetOptionalComponentState(array[i].ComponentId);
            if (componentState != null)
                NotifyRenderCompleted(componentState, ref batch);
        }

        return batch != null ? Task.WhenAll(batch) : Task.CompletedTask;
    }

    private async Task InvokeRenderCompletedCallsAfterUpdateDisplayTask(
        Task  updateDisplayTask,
        int[] updatedComponents)
    {
        try
        {
            await updateDisplayTask;
        }
        catch // avoiding exception filters for AOT runtimes
        {
            if (updateDisplayTask.IsCanceled)
                return;

            HandleException(updateDisplayTask.Exception);
            return;
        }

        List<Task> batch = null;
        var        array = updatedComponents;
        for (var i = 0; i < updatedComponents.Length; i++)
        {
            var componentState = GetOptionalComponentState(array[i]);
            if (componentState != null)
                NotifyRenderCompleted(componentState, ref batch);
        }

        var result = batch != null ? Task.WhenAll(batch) : Task.CompletedTask;

        await result;
    }

    private void NotifyRenderCompleted(ComponentState state, ref List<Task> batch)
    {
        var task = state.NotifyRenderCompletedAsync();

        if (task.IsCompleted)
        {
            if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Canceled)
                return;
            else if (task.Status == TaskStatus.Faulted)
            {
                HandleExceptionViaErrorBoundary(task.Exception, state);
                return;
            }
        }

        batch = batch ?? new List<Task>();
        batch.Add(GetErrorHandledTask(task, state));
    }

    private void RenderInExistingBatch(RenderQueueEntry renderQueueEntry)
    {
        var componentState = renderQueueEntry.ComponentState;
        //Log.RenderingComponent(_logger, componentState);
        componentState.RenderIntoBatch(_batchBuilder, renderQueueEntry.RenderFragment, out var renderFragmentException);
        if (renderFragmentException != null)
            HandleExceptionViaErrorBoundary(renderFragmentException, componentState);

        ProcessDisposalQueueInExistingBatch();
    }

    private void ProcessDisposalQueueInExistingBatch()
    {
        List<Exception> exceptions = null;
        while (_batchBuilder.ComponentDisposalQueue.Count > 0)
        {
            var disposeComponentId    = _batchBuilder.ComponentDisposalQueue.Dequeue();
            var disposeComponentState = GetRequiredComponentState(disposeComponentId);
            //Log.DisposingComponent(_logger, disposeComponentState);
            if (!(disposeComponentState.Component is IAsyncDisposable))
            {
                if (!disposeComponentState.TryDisposeInBatch(_batchBuilder, out var exception))
                {
                    exceptions ??= new List<Exception>();
                    exceptions.Add(exception);
                }
            }
            else
            {
                var result = disposeComponentState.DisposeInBatchAsync(_batchBuilder);
                if (result.IsCompleted)
                {
                    if (!result.IsCompletedSuccessfully)
                    {
                        exceptions ??= new List<Exception>();
                        exceptions.Add(result.Exception);
                    }
                }
                else
                {
                    AddToPendingTasks(GetHandledAsynchronousDisposalErrorsTask(result), owningComponentState: null);

                    async Task GetHandledAsynchronousDisposalErrorsTask(Task result)
                    {
                        try
                        {
                            await result;
                        }
                        catch (Exception e)
                        {
                            HandleException(e);
                        }
                    }
                }
            }

            _componentStateById.Remove(disposeComponentId);
            _componentStateByComponent.Remove(disposeComponentState.Component);
            _batchBuilder.DisposedComponentIds.Append(disposeComponentId);
        }

        if (exceptions?.Count > 1)
            HandleException(new AggregateException("Exceptions were encountered while disposing components.", exceptions));
        else if (exceptions?.Count == 1)
            HandleException(exceptions[0]);
    }

    private void RemoveEventHandlerIds(ArrayRange<ulong> eventHandlerIds, Task afterTaskIgnoreErrors)
    {
        if (eventHandlerIds.Count == 0)
            return;

        if (afterTaskIgnoreErrors.IsCompleted)
        {
            var array = eventHandlerIds.Array;
            var count = eventHandlerIds.Count;
            for (var i = 0; i < count; i++)
            {
                var eventHandlerIdToRemove = array[i];
                _eventBindings.Remove(eventHandlerIdToRemove);
                _eventHandlerIdReplacements.Remove(eventHandlerIdToRemove);
            }
        }
        else
        {
            _ = ContinueAfterTask(eventHandlerIds, afterTaskIgnoreErrors);
        }

        async Task ContinueAfterTask(ArrayRange<ulong> eventHandlerIds, Task afterTaskIgnoreErrors)
        {
            var eventHandlerIdsClone = eventHandlerIds.Clone();

            try
            {
                await afterTaskIgnoreErrors;
            }
            catch (Exception)
            {
                // As per method contract, we're not error-handling the task.
                // That remains the caller's business.
            }

            RemoveEventHandlerIds(eventHandlerIdsClone, Task.CompletedTask);
        }
    }

    private async Task GetErrorHandledTask(Task taskToHandle, ComponentState? owningComponentState)
    {
        try
        {
            await taskToHandle;
        }
        catch (Exception ex)
        {
            if (!taskToHandle.IsCanceled)
                HandleExceptionViaErrorBoundary(ex, owningComponentState);
        }
    }

    private void UpdateRenderTreeToMatchClientState(ulong eventHandlerId, EventFieldInfo fieldInfo)
    {
        var componentState = GetOptionalComponentState(fieldInfo.ComponentId);
        if (componentState != null)
            RenderTreeUpdater.UpdateToMatchClientState(
                componentState.CurrentRenderTree,
                eventHandlerId,
                fieldInfo.FieldValue);
    }

    private void HandleExceptionViaErrorBoundary(Exception error, ComponentState? errorSourceOrNull)
    {
        Dispatcher.AssertAccess();

        var candidate = errorSourceOrNull;
        while (candidate is not null)
        {
            if (candidate.Component is IErrorBoundary errorBoundary)
            {
                AddToRenderQueue(candidate.ComponentId, builder => { });

                try
                {
                    errorBoundary.HandleException(error);
                }
                catch (Exception errorBoundaryException)
                {
                    HandleException(errorBoundaryException);
                }

                return; // Handled successfully
            }

            candidate = candidate.ParentComponentState;
        }

        HandleException(error);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!Dispatcher.CheckAccess())
        {
            Dispatcher.InvokeAsync(() => Dispose(disposing)).Wait();
            return;
        }

        _rendererIsDisposed = true;

        if (_hotReloadInitialized && HotReloadManager.MetadataUpdateSupported) 
            HotReloadManager.OnDeltaApplied -= RenderRootComponentsOnHotReload;

        List<Exception> exceptions       = null;
        List<Task>      asyncDisposables = null;
        foreach (var componentState in _componentStateById.Values)
        {
            //Log.DisposingComponent(_logger, componentState);

            if (componentState.Component is IAsyncDisposable asyncDisposable)
            {
                try
                {
                    var task = asyncDisposable.DisposeAsync();
                    if (!task.IsCompletedSuccessfully)
                    {
                        asyncDisposables ??= new();
                        asyncDisposables.Add(task.AsTask());
                    }
                }
                catch (Exception exception)
                {
                    exceptions ??= new List<Exception>();
                    exceptions.Add(exception);
                }
            }
            else if (componentState.Component is IDisposable disposable)
            {
                try
                {
                    componentState.Dispose();
                }
                catch (Exception exception)
                {
                    exceptions ??= new List<Exception>();
                    exceptions.Add(exception);
                }
            }
        }

        _componentStateById.Clear(); // So we know they were all disposed
        _componentStateByComponent.Clear();
        _batchBuilder.Dispose();

        NotifyExceptions(exceptions);

        if (asyncDisposables?.Count >= 1) 
            _disposeTask = HandleAsyncExceptions(asyncDisposables);

        async Task HandleAsyncExceptions(List<Task> tasks)
        {
            List<Exception> asyncExceptions = null;
            foreach (var task in tasks)
            {
                try
                {
                    await task;
                }
                catch (Exception exception)
                {
                    asyncExceptions ??= new List<Exception>();
                    asyncExceptions.Add(exception);
                }
            }

            NotifyExceptions(asyncExceptions);
        }

        void NotifyExceptions(List<Exception> exceptions)
        {
            if (exceptions?.Count > 1)
                HandleException(new AggregateException("Exceptions were encountered while disposing components.", exceptions));
            else if (exceptions?.Count == 1)
                HandleException(exceptions[0]);
        }
    }


    public void Dispose() 
        => Dispose(disposing: true);

    public async ValueTask DisposeAsync()
    {
        if (_rendererIsDisposed)
            return;

        if (_disposeTask != null)
            await _disposeTask;
        else
        {
            await Dispatcher.InvokeAsync(Dispose);

            if (_disposeTask != null)
                await _disposeTask;
            else
                await default(ValueTask);
        }
    }
}