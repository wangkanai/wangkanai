// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Analytics.Services;

namespace Microsoft.Extensions.DependencyInjection;

internal static class CoreBuilderExtensions
{
	public static IAnalyticsBuilder AddRequiredServices(this IAnalyticsBuilder builder)
	{
		builder.ThrowIfNull();

		// Add Analytics options
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<AnalyticsOptions>>().Value);

		return builder;
	}

	public static IAnalyticsBuilder AddCoreServices(this IAnalyticsBuilder builder)
	{
		// Add basic core to services
		builder.Services.TryAddScoped<IAnalyticsService, AnalyticsService>();

		return builder;
	}

	public static IAnalyticsBuilder AddMarkerService(this IAnalyticsBuilder builder)
	{
		builder.Services.TryAddSingleton<AnalyticsMarkerService, AnalyticsMarkerService>();

		return builder;
	}
}