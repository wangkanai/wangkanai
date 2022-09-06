// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Blazor;

public class SvgInlineComponent : ComponentBase
{
    [Parameter]
    public string Src { get; set; }
}