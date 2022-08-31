// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Tabler.Components;

public class SvgIconComponent : BaseTablerComponent
{
    [Parameter]
    public string Name { get; set; } = string.Empty;
}