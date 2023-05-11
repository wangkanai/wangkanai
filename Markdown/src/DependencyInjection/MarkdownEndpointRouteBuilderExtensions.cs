// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;

using Wangkanai.Markdown.Builder;
using Wangkanai.Markdown.Infrastructure;
using Wangkanai.Markdown.Models;
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

	public static IEndpointConventionBuilder MapFallbackToPage(this IEndpointRouteBuilder endpoints, string page)
	{
		endpoints.ThrowIfNull();
		page.ThrowIfNull();

		MarkdownConventionCollection.EnsureValidPageName(page, nameof(page));

		EnsureMarkdownPagesServices(endpoints);

		var pageDataSource = GetOrCreateDataSource(endpoints);
		pageDataSource.CreateInertEndpoints = true;
		RegisterInCache(endpoints.ServiceProvider, pageDataSource);

		var builder = endpoints.MapFallback(context => Task.CompletedTask);
		builder.Add(b => {
			b.Metadata.Add(CreateDynamicMarkdownMetadata(page, null));
			b.Metadata.Add(new MarkdownEndpointDataSourceIdMetadata(pageDataSource.DataSourceId));
		});

		return builder;
	}

	private static DynamicMarkdownMetadata CreateDynamicMarkdownMetadata(string page, string? area)
		=> new DynamicMarkdownMetadata(new RouteValueDictionary()
		{
			{ "page", page },
			{ "area", area }
		});

	private static void EnsureMarkdownPagesServices(IEndpointRouteBuilder endpoints)
	{
		var marker = endpoints.ServiceProvider.GetService<MarkdownActionEndpointDataSourceFactory>();
		marker.ThrowIfNull<InvalidOperationException>(
			$"Unable to find the required services. Please add all the required services by calling '{nameof(IServiceCollection)}.AddMarkdownPages' inside the call to 'ConfigureService(...)' in the application startup code.");
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

	private static void RegisterInCache(IServiceProvider serviceProvider, MarkdownActionEndpointDataSource dataSource)
	{
		var cache = serviceProvider.GetRequiredService<DynamicMarkdownEndpointDataSelectorCache>();
		cache.AddDataSource(dataSource);
	}
}