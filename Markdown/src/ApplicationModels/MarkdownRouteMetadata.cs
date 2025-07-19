// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown.ApplicationModels;

public sealed class MarkdownRouteMetadata
{
	public string MarkdownRoute { get; }
	public string? RouteTemplate { get; }

	public MarkdownRouteMetadata(string markdownRoute, string? routeTemplate)
	{
		MarkdownRoute = markdownRoute;
		RouteTemplate = routeTemplate;
	}
}
