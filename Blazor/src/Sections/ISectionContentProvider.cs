// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.AspNetCore.Components.Web;

internal interface ISectionContentProvider
{
    RenderFragment? Content { get; }
}