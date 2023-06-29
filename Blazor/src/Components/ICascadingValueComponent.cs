// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components;

internal interface ICascadingValueComponent
{
	object? CurrentValue { get; }

	bool CurrentValueIsFixed { get; }
	bool CanSupplyValue(Type valueType, string? valueName);

	void Subscribe(ComponentState subscriber);

	void Unsubscribe(ComponentState subscriber);
}