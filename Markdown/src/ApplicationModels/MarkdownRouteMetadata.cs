// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
