// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Rendering;

internal sealed class RendererSynchronizationContextDispatcher : Dispatcher
{
    private readonly RendererSynchronizationContext _context;

    public RendererSynchronizationContextDispatcher()
    {
        _context                    =  new RendererSynchronizationContext();
        _context.UnhandledException += (sender, e) => { OnUnhandledException(e); };
    }

    public override bool CheckAccess()
    {
        return SynchronizationContext.Current == _context;
    }

    public override Task InvokeAsync(Action workItem)
    {
        if (CheckAccess())
        {
            workItem();
            return Task.CompletedTask;
        }

        return _context.InvokeAsync(workItem);
    }

    public override Task InvokeAsync(Func<Task> workItem)
    {
        if (CheckAccess())
            return workItem();

        return _context.InvokeAsync(workItem);
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
    {
        if (CheckAccess())
            return Task.FromResult(workItem());

        return _context.InvokeAsync(workItem);
    }

    public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
    {
        if (CheckAccess())
            return workItem();

        return _context.InvokeAsync(workItem);
    }
}