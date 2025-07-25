// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Webmaster;
using Wangkanai.Webmaster.Builders;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension method to <see cref="IServiceCollection" /> for configuring client services.
/// </summary>
public static class WebmasterCollectionExtensions
{
	/// <summary>Add Webmaster Service to the services container. </summary>
	/// <param name="services">The services available in the application.</param>
	/// <returns>An <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
	public static IWebmasterBuilder AddWebmaster(this IServiceCollection services)
		=> services.AddWebmasterBuilder()
				   .AddCoreServices()
				   .AddMarkerService();

	/// <summary>Add Webmaster Service to the services container.</summary>
	/// <param name="services">The services available in the application.</param>
	/// <param name="setAction">An <see cref="Action{WebmasterOptions}"/> to configure the provided <see cref="WebmasterOptions"/>.</param>
	/// <returns>An <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
	public static IWebmasterBuilder AddWebmaster(this IServiceCollection services, Action<WebmasterOptions> setAction)
		=> services.Configure(setAction)
				   .AddWebmaster();

	internal static IWebmasterBuilder AddWebmasterBuilder(this IServiceCollection services)
		=> new WebmasterBuilder(services);
}
