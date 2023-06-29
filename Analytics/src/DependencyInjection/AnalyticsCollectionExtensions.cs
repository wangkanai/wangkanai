// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

public static class AnalyticsCollectionExtensions
{
	public static IAnalyticsBuilder AddAnalytics(this IServiceCollection services)
	{
		return services.AddAnalyticsBuilder()
		               .AddRequiredServices()
		               .AddCoreServices()
		               .AddMarkerService();
	}

	public static IAnalyticsBuilder AddAnalytics(this IServiceCollection services, Action<AnalyticsOptions> setAction)
	{
		return services.Configure(setAction)
		               .AddAnalytics();
	}

	private static IAnalyticsBuilder AddAnalyticsBuilder(this IServiceCollection services)
	{
		return new AnalyticsBuilder(services);
	}
}