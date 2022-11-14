// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


namespace Wangkanai.Blazor.Components;

public readonly struct EventCallback : IEventCallback
{
    public static readonly EventCallbackFactory Factory = new EventCallbackFactory();
    public static readonly EventCallback        Empty   = new EventCallback(null, (Action)(() => { }));

    internal readonly MulticastDelegate? Delegate;
    internal readonly IHandleEvent?      Receiver;

    public EventCallback(IHandleEvent? receiver, MulticastDelegate? @delegate)
    {
        Receiver = receiver;
        Delegate = @delegate;
    }

    public   bool HasDelegate              => Delegate != null;
    internal bool RequiresExplicitReceiver => Receiver != null && !object.ReferenceEquals(Receiver, Delegate?.Target);

    public Task InvokeAsync(object? arg)
    {
        if (Receiver == null)
            return EventCallbackWorkItem.InvokeAsync(Delegate, arg);

        return Receiver.HandleEventAsync(new EventCallbackWorkItem(Delegate), arg);
    }

    public Task InvokeAsync()
        => InvokeAsync(null!);

    object? IEventCallback.UnpackForRenderTree()
        => RequiresExplicitReceiver ? (object)this : Delegate;
}

public readonly struct EventCallback<TValue> : IEventCallback
{
    public static readonly EventCallback<TValue> Empty = new EventCallback<TValue>(null, (Action)(() => { }));

    internal readonly MulticastDelegate? Delegate;
    internal readonly IHandleEvent?      Receiver;

    public EventCallback(IHandleEvent? receiver, MulticastDelegate? @delegate)
    {
        Receiver = receiver;
        Delegate = @delegate;
    }

    public   bool HasDelegate              => Delegate != null;
    internal bool RequiresExplicitReceiver => Receiver != null && !object.ReferenceEquals(Receiver, Delegate?.Target);

    public Task InvokeAsync(TValue? arg)
    {
        if (Receiver == null)
            return EventCallbackWorkItem.InvokeAsync<TValue?>(Delegate, arg);

        return Receiver.HandleEventAsync(new EventCallbackWorkItem(Delegate), arg);
    }

    public Task InvokeAsync() => InvokeAsync(default!);

    internal EventCallback AsUntyped()
    {
        return new EventCallback(Receiver ?? Delegate?.Target as IHandleEvent, Delegate);
    }

    object? IEventCallback.UnpackForRenderTree()
    {
        return RequiresExplicitReceiver ? (object)AsUntyped() : Delegate;
    }
}