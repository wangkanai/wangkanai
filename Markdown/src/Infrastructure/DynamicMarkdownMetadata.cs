// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownMetadata : IDynamicEndpointMetadata
{
	public RouteValueDictionary Values    { get; }
	public bool                 IsDynamic => true;

	public DynamicMarkdownMetadata(RouteValueDictionary values)
	{
		values.ThrowIfNull();
		Values = values;
	}
}