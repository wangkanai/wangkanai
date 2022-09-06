// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNetCore.Components.Web;

internal sealed class HtmlRegistry
{
    private static ConditionalWeakTable<Dispatcher, HtmlRegistry>   _registries    = new();
    private        Dictionary<string, List<Action<RenderFragment>>> _subscriptions = new();

    public static HtmlRegistry GetRegistry(RenderHandle renderHandle)
        => _registries.GetOrCreateValue(renderHandle.Dispatcher);
    
    public void Subscribe(string name, Action<RenderFragment> callback)
    {
        if(!_subscriptions.TryGetValue(name, out var list))
        {
            list = new List<Action<RenderFragment>>();
            _subscriptions.Add(name, list);
        }
        
        list.Add(callback);
    }

    public void Unsubscribe(string name, Action<RenderFragment> callback)
    {
        if(name != null && _subscriptions.TryGetValue(name, out var list)) 
            list.Remove(callback);
    }

    public void SetConnect(string name, RenderFragment content)
    {
        if (!_subscriptions.TryGetValue(name, out var list)) 
            return;
        
        foreach(var callback in list) 
            callback(content);
    }
}