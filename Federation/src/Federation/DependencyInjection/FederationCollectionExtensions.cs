// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods to <see cref="IServiceCollection" /> for configuring client services.
/// </summary>
public static class FederationCollectionExtensions
{
	/// <summary>
	/// Add Federation service to the services container
	/// </summary>
	/// <param name="services">The services available in the application</param>
	/// <returns>An <see cref="IFederationBuilder" /> so that additional calls can be chained</returns>
	public static IFederationBuilder AddFederation(this IServiceCollection services)
	{
		var builder = services.AddFederationBuilder();
		
		builder.AddRequiredServices()
		       .AddCoreServices()
		       .AddDefaultEndpoints()
		       .AddRunnableServices()
		       .AddResponseMakers()
		       .AddMarkerService();

		return builder;
	}

	/// <summary>
	/// Add Federation service to the services container with configuration
	/// </summary>
	/// <param name="services">The services available in the application</param>
	/// <param name="setAction">The setup action</param>
	/// <returns></returns>
	public static IFederationBuilder AddFederation(this IServiceCollection services, Action<FederationOptions> setAction)
	{
		return services.Configure(setAction)
		               .AddFederation();
	}

	private static IFederationBuilder AddFederationBuilder(this IServiceCollection services)
	{
		return new FederationBuilder(services);
	}
}