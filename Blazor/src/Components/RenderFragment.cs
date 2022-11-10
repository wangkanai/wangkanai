// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components.Rendering;

namespace Wangkanai.Blazor.Components;

/// <summary>
/// Represents a segment of UI content, implemented as a delegate that
/// writes the content to a <see cref="RenderTreeBuilder"/>.
/// </summary>
/// <param name="builder">The <see cref="RenderTreeBuilder"/> to which the content should be written.</param>
public delegate void RenderFragment(RenderTreeBuilder builder);

/// <summary>
/// Represents a segment of UI content for an object of type <typeparamref name="TValue"/>, implemented as
/// a function that returns a <see cref="RenderFragment"/>.
/// </summary>
/// <typeparam name="TValue">The type of object.</typeparam>
/// <param name="value">The value used to build the content.</param>
public delegate RenderFragment RenderFragment<TValue>(TValue value);
