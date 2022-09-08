// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.AspNetCore.Components.Web;

public class BodyClass : ComponentBase
{
    [Parameter] public string Default { get; set; } = default!;
    [Parameter] public string Add     { get; set; } = default!;
    [Parameter] public string Remove  { get; set; } = default!;
}