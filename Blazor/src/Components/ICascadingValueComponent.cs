// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components;

internal interface ICascadingValueComponent
{
    bool CanSupplyValue(Type valueType, string? valueName);

    object? CurrentValue { get; }

    bool CurrentValueIsFixed { get; }

    void Subscribe(ComponentState subscriber);

    void Unsubscribe(ComponentState subscriber);
}