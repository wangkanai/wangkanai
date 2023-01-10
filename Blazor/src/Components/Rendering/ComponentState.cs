// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Wangkanai.Blazor.Components.RenderTree;

namespace Wangkanai.Blazor.Components.Rendering;

internal sealed class ComponentState : IDisposable
{
	private readonly IReadOnlyList<CascadingParameterState> _cascadingParameters;
	private readonly bool                                   _hasAnyCascadingParameterSubscriptions;
	private readonly bool                                   _hasCascadingParameters;
	private readonly Renderer                               _renderer;
	private          bool                                   _componentWasDisposed;
	private          ArrayBuilder<RenderTreeFrame>?         _latestDirectParametersSnapshot; // Lazily instantiated
	private          RenderTreeBuilder                      _nextRenderTree;

	public ComponentState(Renderer renderer, int componentId, IComponent component, ComponentState parentComponentState)
	{
		ComponentId          = componentId;
		ParentComponentState = parentComponentState;
		Component            = component ?? throw new ArgumentNullException(nameof(component));
		_renderer            = renderer ?? throw new ArgumentNullException(nameof(renderer));
		_cascadingParameters = CascadingParameterState.FindCascadingParameters(this);
		CurrentRenderTree    = new RenderTreeBuilder();
		_nextRenderTree      = new RenderTreeBuilder();

		if (_cascadingParameters.Count != 0)
		{
			_hasCascadingParameters                = true;
			_hasAnyCascadingParameterSubscriptions = AddCascadingParameterSubscriptions();
		}
	}

	public int               ComponentId          { get; }
	public IComponent        Component            { get; }
	public ComponentState    ParentComponentState { get; }
	public RenderTreeBuilder CurrentRenderTree    { get; private set; }

	public void Dispose()
	{
		DisposeBuffers();

		if (Component is IDisposable disposable)
			disposable.Dispose();
	}

	public void RenderIntoBatch(
		RenderBatchBuilder batchBuilder,
		RenderFragment     renderFragment,
		out Exception?     renderFragmentException)
	{
		renderFragmentException = null;

		if (_componentWasDisposed)
			return;

		_nextRenderTree.Clear();

		try
		{
			renderFragment(_nextRenderTree);
		}
		catch (Exception ex)
		{
			renderFragmentException = ex;
			return;
		}

		_nextRenderTree.AssertTreeIsValid(Component);

		(CurrentRenderTree, _nextRenderTree) = (_nextRenderTree, CurrentRenderTree);

		var diff = RenderTreeDiffBuilder.ComputeDiff(
			_renderer,
			batchBuilder,
			ComponentId,
			_nextRenderTree.GetFrames(),
			CurrentRenderTree.GetFrames());
		batchBuilder.UpdatedComponentDiffs.Append(diff);
		batchBuilder.InvalidateParameterViews();
	}

	public bool TryDisposeInBatch(RenderBatchBuilder batchBuilder, [NotNullWhen(false)] out Exception? exception)
	{
		_componentWasDisposed = true;
		exception             = null;

		try
		{
			if (Component is IDisposable disposable)
				disposable.Dispose();
		}
		catch (Exception ex)
		{
			exception = ex;
		}

		CleanupComponentStateResources(batchBuilder);

		return exception == null;
	}

	private void CleanupComponentStateResources(RenderBatchBuilder batchBuilder)
	{
		RenderTreeDiffBuilder.DisposeFrames(batchBuilder, CurrentRenderTree.GetFrames());

		if (_hasAnyCascadingParameterSubscriptions)
			RemoveCascadingParameterSubscriptions();

		DisposeBuffers();
	}

	public Task NotifyRenderCompletedAsync()
	{
		if (Component is IHandleAfterRender handlerAfterRender)
		{
			try
			{
				return handlerAfterRender.OnAfterRenderAsync();
			}
			catch (OperationCanceledException cex)
			{
				return Task.FromCanceled(cex.CancellationToken);
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
		}

		return Task.CompletedTask;
	}

	public void SetDirectParameters(ParameterView parameters)
	{
		// Note: We should be careful to ensure that the framework never calls
		// IComponent.SetParametersAsync directly elsewhere. We should only call it
		// via ComponentState.SetDirectParameters (or NotifyCascadingValueChanged below).
		// If we bypass this, the component won't receive the cascading parameters nor
		// will it update its snapshot of direct parameters.

		if (_hasAnyCascadingParameterSubscriptions)
		{
			// We may need to replay these direct parameters later (in NotifyCascadingValueChanged),
			// but we can't guarantee that the original underlying data won't have mutated in the
			// meantime, since it's just an index into the parent's RenderTreeFrames buffer.
			if (_latestDirectParametersSnapshot == null) _latestDirectParametersSnapshot = new ArrayBuilder<RenderTreeFrame>();

			parameters.CaptureSnapshot(_latestDirectParametersSnapshot);
		}

		if (_hasCascadingParameters) parameters = parameters.WithCascadingParameters(_cascadingParameters);

		SupplyCombinedParameters(parameters);
	}

	public void NotifyCascadingValueChanged(in ParameterViewLifetime lifetime)
	{
		var directParams = _latestDirectParametersSnapshot != null
			                   ? new ParameterView(lifetime, _latestDirectParametersSnapshot.Buffer, 0)
			                   : ParameterView.Empty;
		var allParams = directParams.WithCascadingParameters(_cascadingParameters!);
		SupplyCombinedParameters(allParams);
	}

	// This should not be called from anywhere except SetDirectParameters or NotifyCascadingValueChanged.
	// Those two methods know how to correctly combine both cascading and non-cascading parameters to supply
	// a consistent set to the recipient.
	private void SupplyCombinedParameters(ParameterView directAndCascadingParameters)
	{
		// Normalise sync and async exceptions into a Task
		Task setParametersAsyncTask;
		try
		{
			setParametersAsyncTask = Component.SetParametersAsync(directAndCascadingParameters);
		}
		catch (Exception ex)
		{
			setParametersAsyncTask = Task.FromException(ex);
		}

		_renderer.AddToPendingTasks(setParametersAsyncTask, this);
	}

	private bool AddCascadingParameterSubscriptions()
	{
		var hasSubscription        = false;
		var numCascadingParameters = _cascadingParameters!.Count;

		for (var i = 0; i < numCascadingParameters; i++)
		{
			var valueSupplier = _cascadingParameters[i].ValueSupplier;
			if (!valueSupplier.CurrentValueIsFixed)
			{
				valueSupplier.Subscribe(this);
				hasSubscription = true;
			}
		}

		return hasSubscription;
	}

	private void RemoveCascadingParameterSubscriptions()
	{
		var numCascadingParameters = _cascadingParameters!.Count;
		for (var i = 0; i < numCascadingParameters; i++)
		{
			var supplier = _cascadingParameters[i].ValueSupplier;
			if (!supplier.CurrentValueIsFixed)
				supplier.Unsubscribe(this);
		}
	}

	private void DisposeBuffers()
	{
		_nextRenderTree.Dispose();
		CurrentRenderTree.Dispose();
		_latestDirectParametersSnapshot?.Dispose();
	}

	public Task DisposeInBatchAsync(RenderBatchBuilder batchBuilder)
	{
		_componentWasDisposed = true;

		CleanupComponentStateResources(batchBuilder);

		try
		{
			var result = ((IAsyncDisposable)Component).DisposeAsync();
			if (result.IsCompletedSuccessfully)
			{
				// If it's a IValueTaskSource backed ValueTask,
				// inform it its result has been read so it can reset
				result.GetAwaiter().GetResult();
				return Task.CompletedTask;
			}

			// We know we are dealing with an exception that happened asynchronously, so return a task
			// to the caller so that he can unwrap it.
			return result.AsTask();
		}
		catch (Exception e)
		{
			return Task.FromException(e);
		}
	}
}