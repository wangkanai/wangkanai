// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

namespace Wangkanai.Blazor.Components;

public readonly struct EventCallbackWorkItem
{
    public static readonly EventCallbackWorkItem Empty = new EventCallbackWorkItem(null);

    private readonly MulticastDelegate? _delegate;

    public EventCallbackWorkItem(MulticastDelegate? @delegate)
    {
        _delegate = @delegate;
    }

    public Task InvokeAsync(object? arg)
    {
        return InvokeAsync<object?>(_delegate, arg);
    }

    internal static Task InvokeAsync<T>(MulticastDelegate? @delegate, T arg)
    {
        switch (@delegate)
        {
            case null:
                return Task.CompletedTask;

            case Action action:
                action.Invoke();
                return Task.CompletedTask;

            case Action<T> actionEventArgs:
                actionEventArgs.Invoke(arg);
                return Task.CompletedTask;

            case Func<Task> func:
                return func.Invoke();

            case Func<T, Task> funcEventArgs:
                return funcEventArgs.Invoke(arg);

            default:
            {
                try
                {
                    return @delegate.DynamicInvoke(arg) as Task ?? Task.CompletedTask;
                }
                catch (TargetInvocationException e)
                {
                    return Task.FromException(e.InnerException!);
                }
            }
        }
    }
}