// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension method to <see cref="IServiceCollection"/> for configuring client services</summary>
public static class AnalyticsCollectionExtensions
{
	/// <summary> Add Analytics service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <returns>An <see cref="IAnalyticsBuilder" /> so that additional calls can be chained</returns>
	public static IAnalyticsBuilder AddAnalytics(this IServiceCollection services)
		=> services.AddAnalyticsBuilder()
		           .AddRequiredServices()
		           .AddCoreServices()
		           .AddMarkerService();

	/// <summary>Add Analytics service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <param name="setAction">An <see cref="Action{AnalyticsOptions}" /> to configure the provided <see cref="AnalyticsOptions" /></param>
	/// <returns>An <see cref="IAnalyticsBuilder" /> so that additional calls can be chained</returns>
	public static IAnalyticsBuilder AddAnalytics(this IServiceCollection services, Action<AnalyticsOptions> setAction)
		=> services.Configure(setAction)
		           .AddAnalytics();

	// For internal unit tests
	internal static IAnalyticsBuilder AddAnalyticsBuilder(this IServiceCollection services)
		=> new AnalyticsBuilder(services);
}