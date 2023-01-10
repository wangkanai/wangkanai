// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


namespace Wangkanai.Blazor.Components;

public readonly struct EventCallback : IEventCallback
{
	public static readonly EventCallbackFactory Factory = new();
	public static readonly EventCallback        Empty   = new(null, (Action)(() => { }));

	internal readonly MulticastDelegate? Delegate;
	internal readonly IHandleEvent?      Receiver;

	public EventCallback(IHandleEvent? receiver, MulticastDelegate? @delegate)
	{
		Receiver = receiver;
		Delegate = @delegate;
	}

	public   bool HasDelegate              => Delegate != null;
	internal bool RequiresExplicitReceiver => Receiver != null && !ReferenceEquals(Receiver, Delegate?.Target);

	public Task InvokeAsync(object? arg)
	{
		if (Receiver == null)
			return EventCallbackWorkItem.InvokeAsync(Delegate, arg);

		return Receiver.HandleEventAsync(new EventCallbackWorkItem(Delegate), arg);
	}

	public Task InvokeAsync()
	{
		return InvokeAsync(null!);
	}

	object? IEventCallback.UnpackForRenderTree()
	{
		return RequiresExplicitReceiver ? this : Delegate;
	}
}

public readonly struct EventCallback<TValue> : IEventCallback
{
	public static readonly EventCallback<TValue> Empty = new(null, (Action)(() => { }));

	internal readonly MulticastDelegate? Delegate;
	internal readonly IHandleEvent?      Receiver;

	public EventCallback(IHandleEvent? receiver, MulticastDelegate? @delegate)
	{
		Receiver = receiver;
		Delegate = @delegate;
	}

	public   bool HasDelegate              => Delegate != null;
	internal bool RequiresExplicitReceiver => Receiver != null && !ReferenceEquals(Receiver, Delegate?.Target);

	public Task InvokeAsync(TValue? arg)
	{
		if (Receiver == null)
			return EventCallbackWorkItem.InvokeAsync(Delegate, arg);

		return Receiver.HandleEventAsync(new EventCallbackWorkItem(Delegate), arg);
	}

	public Task InvokeAsync()
	{
		return InvokeAsync(default!);
	}

	internal EventCallback AsUntyped()
	{
		return new EventCallback(Receiver ?? Delegate?.Target as IHandleEvent, Delegate);
	}

	object? IEventCallback.UnpackForRenderTree()
	{
		return RequiresExplicitReceiver ? AsUntyped() : Delegate;
	}
}