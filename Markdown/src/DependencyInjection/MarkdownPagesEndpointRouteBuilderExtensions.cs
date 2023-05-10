// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.DependencyInjection;

public static class MarkdownPagesEndpointRouteBuilderExtensions
{
	public static PageActionEndpointConventionBuilder MapMarkdownPages(this IEndpointRouteBuilder endpoints)
	{
		endpoints.ThrowIfNull(nameof(endpoints));

		EnsureMarkdownPagesServices(endpoints);

		return GetOrCreateDataSource(endpoints).DefaultBuilder;
	}

	private static void EnsureMarkdownPagesServices(IEndpointRouteBuilder endpoints)
	{
		var marker = endpoints.ServiceProvider.GetService<PageActionEndpointDataSourceFactory>();
	}

}