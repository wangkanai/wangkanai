// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Blazor.Components.Sections;

internal sealed class SectionOutlet : ISectionContentSubscriber,IComponent, IDisposable
{
    private static readonly RenderFragment _emptyRenderFragment = _ => { };

    private string?         _subscribedName;
    private RenderHandle    _renderHandle;
    private SectionRegistry _registry = default!;
    private RenderFragment? _content;
    
    [Parameter] public string Name { get; set; } = default!;

    void IComponent.Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
        _registry     = _renderHandle.Dispatcher.Registry;
    }

    Task IComponent.SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (string.IsNullOrEmpty(Name))
            throw new InvalidOperationException($"{GetType()} requires a non-empty string parameter '{nameof(Name)}'.");

        if (Name != _subscribedName)
        {
            if (_subscribedName is not null) 
                _registry.Unsubscribe(_subscribedName);

            _registry.Subscribe(Name, this);
            _subscribedName = Name;
        }

        RenderContent();

        return Task.CompletedTask;
    }

    void ISectionContentSubscriber.ContentChanged(RenderFragment? content)
    {
        _content = content;
        RenderContent();
    }

    private void RenderContent()
    {
        if (_renderHandle.IsRendererDisposed)
            return;

        _renderHandle.Render(_content ?? _emptyRenderFragment);
    }
    
    public void Dispose()
    {
        if (_subscribedName is not null) 
            _registry.Unsubscribe(_subscribedName);
    }
}