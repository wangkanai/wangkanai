// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Markdown.Builder;
using Wangkanai.Markdown.Infrastructure;
using Wangkanai.Mvc.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class MarkdownEndpointRouteBuilderExtensions
{
	public static MarkdownActionEndpointConventionBuilder MapMarkdownPages(this IEndpointRouteBuilder endpoints)
	{
		endpoints.ThrowIfNull(nameof(endpoints));

		EnsureMarkdownPagesServices(endpoints);

		return GetOrCreateDataSource(endpoints).DefaultBuilder;
	}

	private static void EnsureMarkdownPagesServices(IEndpointRouteBuilder endpoints)
	{
		var marker = endpoints.ServiceProvider.GetService<MarkdownActionEndpointDataSourceFactory>();
	}

	private static MarkdownActionEndpointDataSource GetOrCreateDataSource(IEndpointRouteBuilder endpoints)
	{
		var dataSource = endpoints.DataSources.OfType<MarkdownActionEndpointDataSource>().FirstOrDefault();
		if (dataSource == null)
		{
			var orderProviderCache = endpoints.ServiceProvider.GetRequiredService<OrderedEndpointsSequenceProviderCache>();
			var factory            = endpoints.ServiceProvider.GetRequiredService<MarkdownActionEndpointDataSourceFactory>();
			dataSource = factory.Create(orderProviderCache.GetOrCreateOrderedEndpointsSequenceProvider(endpoints));
			endpoints.DataSources.Add(dataSource);
		}

		return dataSource;
	}
}