// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Collections.Generic;

using Microsoft.AspNetCore.Components;

using Wangkanai.Blazor.Helpers;

namespace Wangkanai.Blazor;

public class DomBlazorComponentBase : BlazorComponentBase
{
	public DomBlazorComponentBase()
	{
		ClassMapper.Get(() => Class);
		StyleMapper.Get(() => Style);
	}

	[Parameter]
	public string Id { get; set; } = IdGeneratorHelper.Generate("blazor_id_");

	[Parameter(CaptureUnmatchedValues = true)]
	public Dictionary<string, object> Attributes { get; set; }

	[Parameter]
	public string Class { get; set; }

	[Parameter]
	public string Style { get; set; }

	protected ClassMapper ClassMapper { get; } = new();
	protected StyleMapper StyleMapper { get; } = new();
}
