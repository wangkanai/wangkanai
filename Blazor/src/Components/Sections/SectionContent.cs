// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Blazor.Components.Sections;

internal sealed class SectionContent : ISectionContentProvider, IComponent, IDisposable
{
    private string?         _registeredName;
    private SectionRegistry _registry = default!;

    [Parameter] public string          Name             { get; set; } = default!;
    [Parameter] public bool            IsDefaultContent { get; set; }
    [Parameter] public RenderFragment? ChildContent     { get; set; }

    RenderFragment? ISectionContentProvider.Content
        => ChildContent;

    void IComponent.Attach(RenderHandle renderHandle)
        => _registry = renderHandle.Dispatcher.Registry;

    Task IComponent.SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (string.IsNullOrEmpty(Name))
            throw new InvalidOperationException($"{GetType()} requires a non-empty string parameter '{nameof(Name)}'.");

        if (Name != _registeredName)
        {
            if (_registeredName is not null) 
                _registry.RemoveProvider(_registeredName, this);

            _registry.AddProvider(Name, this, IsDefaultContent);
            _registeredName = Name;
        }

        _registry.NotifyContentChanged(Name, this);

        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        if (_registeredName is not null) 
            _registry.RemoveProvider(_registeredName, this);
    }
}