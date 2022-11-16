// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.ComponentModel;

namespace Wangkanai.Blazor.Components;

public sealed class EventCallbackFactory
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public EventCallback Create(object receiver, EventCallback callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return callback;
    }

    public EventCallback Create(object receiver, Action callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore(receiver, callback);
    }

    public EventCallback Create(object receiver, Action<object> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore(receiver, callback);
    }

    public EventCallback Create(object receiver, Func<Task> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore(receiver, callback);
    }

    public EventCallback Create(object receiver, Func<object, Task> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore(receiver, callback);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public EventCallback<TValue> Create<TValue>(object receiver, EventCallback callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return new EventCallback<TValue>(callback.Receiver, callback.Delegate);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public EventCallback<TValue> Create<TValue>(object receiver, EventCallback<TValue> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return callback;
    }

    public EventCallback<TValue> Create<TValue>(object receiver, Action callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore<TValue>(receiver, callback);
    }

    public EventCallback<TValue> Create<TValue>(object receiver, Action<TValue> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore<TValue>(receiver, callback);
    }

    public EventCallback<TValue> Create<TValue>(object receiver, Func<Task> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore<TValue>(receiver, callback);
    }

    public EventCallback<TValue> Create<TValue>(object receiver, Func<TValue, Task> callback)
    {
        if (receiver == null)
            throw new ArgumentNullException(nameof(receiver));

        return CreateCore<TValue>(receiver, callback);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public EventCallback<TValue> CreateInferred<TValue>(object receiver, Action<TValue> callback, TValue value)
    {
        return Create(receiver, callback);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public EventCallback<TValue> CreateInferred<TValue>(object receiver, Func<TValue, Task> callback, TValue value)
    {
        return Create(receiver, callback);
    }

    private static EventCallback CreateCore(object receiver, MulticastDelegate callback)
    {
        return new(callback?.Target as IHandleEvent ?? receiver as IHandleEvent, callback);
    }

    private static EventCallback<TValue> CreateCore<TValue>(object receiver, MulticastDelegate callback)
    {
        return new(callback?.Target as IHandleEvent ?? receiver as IHandleEvent, callback);
    }
}