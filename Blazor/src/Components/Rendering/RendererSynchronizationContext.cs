// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Wangkanai.Blazor.Components.Rendering;

[DebuggerDisplay("{_state,nq}")]
internal sealed class RendererSynchronizationContext : SynchronizationContext
{
    private readonly State _state;

    public RendererSynchronizationContext()
        : this(new State())
    {
    }

    private RendererSynchronizationContext(State state)
        => _state = state;

    private static readonly ContextCallback ExecutionContextThunk = (object state) =>
    {
        var item = (WorkItem)state;
        item.SynchronizationContext.ExecuteSynchronously(null, item.Callback, item.State);
    };

    private static readonly Action<Task, object> BackgroundWorkThunk = (Task task, object state) =>
    {
        var item = (WorkItem)state;
        item.SynchronizationContext.ExecuteBackground(item);
    };

    public event UnhandledExceptionEventHandler UnhandledException;

    public Task InvokeAsync(Action action)
    {
        var completion = new RendererSynchronizationTaskCompletionSource<Action, object>(action);
        ExecuteSynchronouslyIfPossible((state) =>
        {
            var completion = (RendererSynchronizationTaskCompletionSource<Action, object>)state;
            try
            {
                completion.Callback();
                completion.SetResult(null);
            }
            catch (OperationCanceledException)
            {
                completion.SetCanceled();
            }
            catch (Exception exception)
            {
                completion.SetException(exception);
            }
        }, completion);

        return completion.Task;
    }

    public Task InvokeAsync(Func<Task> asyncAction)
    {
        var completion = new RendererSynchronizationTaskCompletionSource<Func<Task>, object>(asyncAction);
        ExecuteSynchronouslyIfPossible(async (state) =>
        {
            var completion = (RendererSynchronizationTaskCompletionSource<Func<Task>, object>)state;
            try
            {
                await completion.Callback();
                completion.SetResult(null);
            }
            catch (OperationCanceledException)
            {
                completion.SetCanceled();
            }
            catch (Exception exception)
            {
                completion.SetException(exception);
            }
        }, completion);

        return completion.Task;
    }

    public Task<TResult> InvokeAsync<TResult>(Func<TResult> function)
    {
        var completion = new RendererSynchronizationTaskCompletionSource<Func<TResult>, TResult>(function);
        ExecuteSynchronouslyIfPossible((state) =>
        {
            var completion = (RendererSynchronizationTaskCompletionSource<Func<TResult>, TResult>)state;
            try
            {
                var result = completion.Callback();
                completion.SetResult(result);
            }
            catch (OperationCanceledException)
            {
                completion.SetCanceled();
            }
            catch (Exception exception)
            {
                completion.SetException(exception);
            }
        }, completion);

        return completion.Task;
    }

    public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> asyncFunction)
    {
        var completion = new RendererSynchronizationTaskCompletionSource<Func<Task<TResult>>, TResult>(asyncFunction);
        ExecuteSynchronouslyIfPossible(async (state) =>
        {
            var completion = (RendererSynchronizationTaskCompletionSource<Func<Task<TResult>>, TResult>)state;
            try
            {
                var result = await completion.Callback();
                completion.SetResult(result);
            }
            catch (OperationCanceledException)
            {
                completion.SetCanceled();
            }
            catch (Exception exception)
            {
                completion.SetException(exception);
            }
        }, completion);

        return completion.Task;
    }
    
    public override void Post(SendOrPostCallback d, object state)
    {
        lock (_state.Lock) 
            _state.Task = Enqueue(_state.Task, d, state, forceAsync: true);
    }

    // synchronously runs the callback
    public override void Send(SendOrPostCallback d, object state)
    {
        Task antecedent;
        var  completion = new TaskCompletionSource();

        lock (_state.Lock)
        {
            antecedent  = _state.Task;
            _state.Task = completion.Task;
        }
        
        antecedent.Wait();

        ExecuteSynchronously(completion, d, state);
    }

    // shallow copy
    public override SynchronizationContext CreateCopy() 
        => new RendererSynchronizationContext(_state);

    private void ExecuteSynchronouslyIfPossible(SendOrPostCallback d, object state)
    {
        TaskCompletionSource completion;
        lock (_state.Lock)
        {
            if (!_state.Task.IsCompleted)
            {
                _state.Task = Enqueue(_state.Task, d, state);
                return;
            }
            
            completion  = new TaskCompletionSource();
            _state.Task = completion.Task;
        }

        ExecuteSynchronously(completion, d, state);
    }

    private Task Enqueue(Task antecedent, SendOrPostCallback d, object state, bool forceAsync = false)
    {
        ExecutionContext executionContext = null;
        if (!ExecutionContext.IsFlowSuppressed())
            executionContext = ExecutionContext.Capture();

        var flags = forceAsync ? TaskContinuationOptions.RunContinuationsAsynchronously : TaskContinuationOptions.None;
        return antecedent.ContinueWith(BackgroundWorkThunk, new WorkItem()
        {
            SynchronizationContext = this,
            ExecutionContext       = executionContext,
            Callback               = d,
            State                  = state,
        }, CancellationToken.None, flags, TaskScheduler.Current);
    }

    private void ExecuteSynchronously(
        TaskCompletionSource completion,
        SendOrPostCallback   d,
        object               state)
    {
        var original = Current;
        try
        {
            SetSynchronizationContext(this);
            _state.IsBusy = true;

            d(state);
        }
        finally
        {
            _state.IsBusy = false;
            SetSynchronizationContext(original);

            completion?.SetResult();
        }
    }

    private void ExecuteBackground(WorkItem item)
    {
        if (item.ExecutionContext == null)
        {
            try
            {
                ExecuteSynchronously(null, item.Callback, item.State);
            }
            catch (Exception ex)
            {
                DispatchException(ex);
            }

            return;
        }

        // Perf - using a static thunk here to avoid a delegate allocation.
        try
        {
            ExecutionContext.Run(item.ExecutionContext, ExecutionContextThunk, item);
        }
        catch (Exception ex)
        {
            DispatchException(ex);
        }
    }

    private void DispatchException(Exception ex)
    {
        var handler = UnhandledException;
        if (handler != null)
        {
            handler(this, new UnhandledExceptionEventArgs(ex, isTerminating: false));
        }
    }

    private sealed class State
    {
        public bool   IsBusy; // Just for debugging
        public object Lock = new object();
        public Task   Task = Task.CompletedTask;

        public override string ToString()
            => $"{{ Busy: {IsBusy}, Pending Task: {Task} }}";
    }

    private sealed class WorkItem
    {
        public RendererSynchronizationContext SynchronizationContext;
        public ExecutionContext               ExecutionContext;
        public SendOrPostCallback             Callback;
        public object                         State;
    }
    
    private sealed class RendererSynchronizationTaskCompletionSource<TCallback, TResult> : TaskCompletionSource<TResult>
    {
        public RendererSynchronizationTaskCompletionSource(TCallback callback)
        {
            Callback = callback;
        }

        public TCallback Callback { get; }
    }
}