// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Web;

public class SectionOutlet : IComponent, IDisposable
{
    private static RenderFragment         EmptyRenderFragment = builder => { };
    private        string                 _subscribledName;
    private        SectionRegistry           _registry = default!;
    private        Action<RenderFragment> _onChangeCallback;


    public void Attach(RenderHandle renderHandle)
    {
        _onChangeCallback = content => renderHandle.Render(content ?? EmptyRenderFragment);
        _registry         = SectionRegistry.GetRegistry(renderHandle);
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        var suppliedName = parameters.GetValueOrDefault<string>("Name");

        if (suppliedName != _subscribledName)
        {
            _registry.Unsubscribe(_subscribledName, _onChangeCallback);
            _registry.Subscribe(suppliedName, _onChangeCallback);
            _subscribledName = suppliedName;
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _registry.Unsubscribe(_subscribledName, _onChangeCallback);
    }
}