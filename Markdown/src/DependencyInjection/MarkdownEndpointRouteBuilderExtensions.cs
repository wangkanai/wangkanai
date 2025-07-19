// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

using Wangkanai.Markdown.ApplicationModels;
using Wangkanai.Markdown.Builder;
using Wangkanai.Markdown.Infrastructure;
using Wangkanai.Mvc.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class MarkdownEndpointRouteBuilderExtensions
{
	public static MarkdownActionEndpointConventionBuilder MapMarkdownPages(this IEndpointRouteBuilder endpoints)
	{
		endpoints.ThrowIfNull();

		EnsureMarkdownPagesServices(endpoints);

		return GetOrCreateDataSource(endpoints).DefaultBuilder;
	}

	public static IEndpointConventionBuilder MapFallbackToMarkdown(
		this IEndpointRouteBuilder endpoints,
		string page)
	{
		endpoints.ThrowIfNull();
		page.ThrowIfNull();

		MarkdownConventionCollection.EnsureValidMarkdownName(page, nameof(page));

		EnsureMarkdownPagesServices(endpoints);

		var pageDataSource = GetOrCreateDataSource(endpoints);
		pageDataSource.CreateInertEndpoints = true;
		RegisterInCache(endpoints.ServiceProvider, pageDataSource);

		var builder = endpoints.MapFallback(context => Task.CompletedTask);
		builder.Add(b =>
		{
			b.Metadata.Add(CreateDynamicMarkdownMetadata(page, null));
			b.Metadata.Add(new MarkdownEndpointDataSourceIdMetadata(pageDataSource.DataSourceId));
		});

		return builder;
	}

	public static IEndpointConventionBuilder MapFallbackToMarkdown(
		this IEndpointRouteBuilder endpoints,
		string pattern,
		string page)
	{
		endpoints.ThrowIfNull();
		pattern.ThrowIfNull();
		page.ThrowIfNull();

		MarkdownConventionCollection.EnsureValidMarkdownName(page, nameof(page));

		EnsureMarkdownPagesServices(endpoints);

		var pageDataSource = GetOrCreateDataSource(endpoints);
		pageDataSource.CreateInertEndpoints = true;
		RegisterInCache(endpoints.ServiceProvider, pageDataSource);

		var builder = endpoints.MapFallback(pattern, context => Task.CompletedTask);
		builder.Add(b =>
		{
			b.Metadata.Add(CreateDynamicMarkdownMetadata(page, area: null));
			b.Metadata.Add(new MarkdownEndpointDataSourceIdMetadata(pageDataSource.DataSourceId));
		});
		return builder;
	}

	public static IEndpointConventionBuilder MapFallbackToAreaPage(
		this IEndpointRouteBuilder endpoints,
		string page,
		string area)
	{
		endpoints.ThrowIfNull();
		page.ThrowIfNull();

		MarkdownConventionCollection.EnsureValidMarkdownName(page, nameof(page));

		EnsureMarkdownPagesServices(endpoints);

		var pageDataSource = GetOrCreateDataSource(endpoints);
		pageDataSource.CreateInertEndpoints = true;
		RegisterInCache(endpoints.ServiceProvider, pageDataSource);

		var builder = endpoints.MapFallback(context => Task.CompletedTask);
		builder.Add(b =>
		{
			b.Metadata.Add(CreateDynamicMarkdownMetadata(page, area));
			b.Metadata.Add(new MarkdownEndpointDataSourceIdMetadata(pageDataSource.DataSourceId));
		});
		return builder;
	}

	public static IEndpointConventionBuilder MapFallbackToAreaPage(
		this IEndpointRouteBuilder endpoints,
		string pattern,
		string page,
		string area)
	{
		endpoints.ThrowIfNull();
		pattern.ThrowIfNull();
		page.ThrowIfNull();

		MarkdownConventionCollection.EnsureValidMarkdownName(page, nameof(page));

		EnsureMarkdownPagesServices(endpoints);

		var pageDataSource = GetOrCreateDataSource(endpoints);
		pageDataSource.CreateInertEndpoints = true;
		RegisterInCache(endpoints.ServiceProvider, pageDataSource);

		var builder = endpoints.MapFallback(context => Task.CompletedTask);
		builder.Add(b =>
		{
			b.Metadata.Add(CreateDynamicMarkdownMetadata(page, area));
			b.Metadata.Add(new MarkdownEndpointDataSourceIdMetadata(pageDataSource.DataSourceId));
		});
		return builder;
	}

	public static void MapDynamicMarkdownRoute<TTransformer>(this IEndpointRouteBuilder endpoints, string pattern)
		where TTransformer : DynamicRouteValueTransformer
	{
		MapDynamicMarkdownRoute<TTransformer>(endpoints, pattern, state: null);
	}

	public static void MapDynamicMarkdownRoute<TTransformer>(this IEndpointRouteBuilder endpoints, string pattern, object? state)
		where TTransformer : DynamicRouteValueTransformer
	{
		endpoints.ThrowIfNull();
		pattern.ThrowIfNull();

		EnsureMarkdownPagesServices(endpoints);

		var pageDataSource = GetOrCreateDataSource(endpoints);
		RegisterInCache(endpoints.ServiceProvider, pageDataSource);

		pageDataSource.AddDynamicMarkdownEndpoint(endpoints, pattern, typeof(TTransformer), state);
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
			string.Format(Wangkanai.Mvc.Resources.UnableToFindServices, nameof(IServiceCollection), "AddMarkdownPages", "ConfigureServices(...)")
		);
	}

	private static MarkdownActionEndpointDataSource GetOrCreateDataSource(IEndpointRouteBuilder endpoints)
	{
		var dataSource = endpoints.DataSources.OfType<MarkdownActionEndpointDataSource>().FirstOrDefault();
		if (dataSource == null)
		{
			var orderProviderCache = endpoints.ServiceProvider.GetRequiredService<OrderedEndpointsSequenceProviderCache>();
			var factory = endpoints.ServiceProvider.GetRequiredService<MarkdownActionEndpointDataSourceFactory>();
			dataSource = factory.Create(orderProviderCache.GetOrCreateOrderedEndpointsSequenceProvider(endpoints));
			endpoints.DataSources.Add(dataSource);
		}

		return dataSource;
	}

	private static void RegisterInCache(IServiceProvider serviceProvider, MarkdownActionEndpointDataSource dataSource)
	{
		var cache = serviceProvider.GetRequiredService<DynamicMarkdownEndpointSelectorCache>();
		cache.AddDataSource(dataSource);
	}
}
