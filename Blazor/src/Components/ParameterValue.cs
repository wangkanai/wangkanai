// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components;

public readonly struct ParameterValue
{
	public string Name      { get; }
	public object Value     { get; }
	public bool   Cascading { get; }

	internal ParameterValue(string name, object value, bool cascading)
	{
		Name      = name;
		Value     = value;
		Cascading = cascading;
	}
}