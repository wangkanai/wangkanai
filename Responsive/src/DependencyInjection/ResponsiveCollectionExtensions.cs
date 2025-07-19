// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Responsive;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extensions method to <see cref="IServiceCollection" /> for configuring client services</summary>
public static class ResponsiveCollectionExtensions
{
	/// <summary>Add Responsive service to the services container</summary>
	/// <param name="services">The services available in the application</param>
	/// <returns>An <see cref="IResponsiveBuilder" /> so that additional calls can be chained</returns>
	public static IResponsiveBuilder AddResponsive(this IServiceCollection services)
		=> services.AddResponsiveBuilder()
				   .AddRequiredServices()
				   .AddCoreServices()
				   .AddSessionServices()
				   .AddResponsiveService()
				   .AddMarkerService();

	/// <summary>Add Responsive service to the service container</summary>
	/// <param name="services">The services available in the application</param>
	/// <param name="configure">An <see cref="Action{ResposnsiveOptions}" /> to configure the provided<see cref="ResponsiveOptions" /></param>
	/// <returns>An <see cref="IResponsiveBuilder" /> so that additional calls can be chained</returns>
	public static IResponsiveBuilder AddResponsive(this IServiceCollection services, Action<ResponsiveOptions> configure)
		=> services.Configure(configure)
				   .AddResponsive();

	// For internal unit tests
	internal static IResponsiveBuilder AddResponsiveBuilder(this IServiceCollection services)
		=> new ResponsiveBuilder(services);
}
