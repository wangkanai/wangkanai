// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Wangkanai.Blazor.Components.HotReload;
using Wangkanai.Blazor.Components.RenderTree;

namespace Wangkanai.Blazor.Components;

public readonly struct RenderHandle
{
    private readonly Renderer? _renderer;
    private readonly int       _componentId;

    internal RenderHandle(Renderer renderer, int componentId)
    {
        _renderer    = renderer ?? throw new ArgumentNullException(nameof(renderer));
        _componentId = componentId;
    }

    public Dispatcher Dispatcher
    {
        get
        {
            if (_renderer == null)
                ThrowNotInitialized();

            return _renderer.Dispatcher;
        }
    }

    public bool IsInitialized
        => _renderer is not null;

    public bool IsRenderingOnMetadataUpdate
        => HotReloadManager.Default.MetadataUpdateSupported && (_renderer?.IsRenderingOnMetadataUpdate ?? false);

    internal bool IsRendererDisposed
        => _renderer?.Disposed
           ?? throw new InvalidOperationException("No renderer has been initialized.");

    /// <summary>
    ///     Notifies the renderer that the component should be rendered.
    /// </summary>
    /// <param name="renderFragment">The content that should be rendered.</param>
    public void Render(RenderFragment renderFragment)
    {
        if (_renderer == null)
            ThrowNotInitialized();

        _renderer.AddToRenderQueue(_componentId, renderFragment);
    }

    [DoesNotReturn]
    private static void ThrowNotInitialized()
    {
        throw new InvalidOperationException("The render handle is not yet assigned.");
    }
}