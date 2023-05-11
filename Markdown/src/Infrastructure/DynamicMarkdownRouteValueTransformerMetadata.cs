// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownRouteValueTransformerMetadata : IDynamicEndpointMetadata
{
	public bool    IsDynamic    => true;
	public object? State        { get; }
	public Type    SelectorType { get; }

	public DynamicMarkdownRouteValueTransformerMetadata(Type selectorType, object? state)
	{
		selectorType.ThrowIfNull();

		if (!typeof(DynamicRouteValueTransformer).IsAssignableFrom(selectorType))
			throw new ArgumentException(
				$"The provided type must be a subclass of {typeof(DynamicRouteValueTransformer)}",
				nameof(selectorType)
			);

		SelectorType = selectorType;
		State        = state;
	}
}