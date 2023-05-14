// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection.Metadata;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Detection;
using Wangkanai.Markdown;
using Wangkanai.Markdown.DependencyInjection.Options;
using Wangkanai.Markdown.Infrastructure;
using Wangkanai.Markdown.Services;
using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Routing;

namespace Microsoft.Extensions.DependencyInjection;

internal static class MarkdownCoreBuilderExtensions
{
	internal static IMarkdownBuilder AddRequiredServices(this IMarkdownBuilder builder)
	{
		// Hosting doesn't add IHttpContextAccessor by default
		builder.Services.AddHttpContextAccessor();

		// Add DetectionMvc Options
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<DetectionOptions>>().Value);

		return builder;
	}

	internal static IMarkdownBuilder AddCoreServices(this IMarkdownBuilder builder)
	{
		if (MetadataUpdater.IsSupported)
			builder.Services.TryAddSingleton<MarkdownHotReload>();

		// Options
		builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MarkdownViewEngineOptions>, MarkdownViewEngineOptionsSetup>());
		builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MarkdownPagesOptions>, MarkdownPagesOptionsSetup>());

		// Routing
		builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<MatcherPolicy, DynamicMarkdownEndpointMatcherPolicy>());
		// builder.Services.TryAddSingleton<DynamicMarkdownEndpointSelectorCache>();
		builder.Services.TryAddSingleton<OrderedEndpointsSequenceProviderCache>();
		builder.Services.TryAddSingleton<ActionEndpointFactory>();
		builder.Services.TryAddSingleton<MarkdownActionEndpointDataSourceIdProvider>();

		// Action description and invocation
		builder.Services.TryAddSingleton<MarkdownActionEndpointDataSourceFactory>();

		// Page and Page model create and activation

		// Action executors

		return builder;
	}

	internal static IMarkdownBuilder AddMarkerService(this IMarkdownBuilder builder)
	{
		builder.Services.TryAddSingleton<MarkdownMarkerService>();

		return builder;
	}
}