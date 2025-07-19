// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Blazor;

public class TableBlazorComponentBase : DomBlazorComponentBase
{
	[Parameter]
	public RenderFragment ChildContent { get; set; }
}
