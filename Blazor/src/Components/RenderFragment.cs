// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components;

public delegate void RenderFragment(RenderTreeBuilder builder);

public delegate RenderFragment RenderFragment<TValue>(TValue value);