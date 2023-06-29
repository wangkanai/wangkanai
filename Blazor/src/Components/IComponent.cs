// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components;

public interface IComponent
{
	void Attach(RenderHandle renderHandle);

	Task SetParametersAsync(ParameterView parameters);
}