// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.Rendering;
using Wangkanai.Blazor.Components.RenderTree;

namespace Wangkanai.Blazor.Components;

internal readonly struct ParameterViewBuilder
{
	private const    string            GeneratedParameterViewElementName = "__ARTIFICIAL_PARAMETER_VIEW";
	private readonly RenderTreeFrame[] _frames;

	public ParameterViewBuilder(int maxCapacity)
	{
		_frames = new RenderTreeFrame[maxCapacity + 1];
		_frames[0] = RenderTreeFrame
		             .Element(0, GeneratedParameterViewElementName)
		             .WithElementSubtreeLength(1);
	}

	public void Add(string name, object? value)
	{
		var nextIndex = _frames[0].ElementSubtreeLengthField++;
		_frames[nextIndex] = RenderTreeFrame.Attribute(0, name, value);
	}

	public ParameterView ToParameterView()
	{
		return new ParameterView(ParameterViewLifetime.Unbound, _frames, 0);
	}
}