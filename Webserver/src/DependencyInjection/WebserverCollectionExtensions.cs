// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Microsoft.Extensions.DependencyInjection;

public static class WebserverCollectionExtensions
{
	public static IWebserverBuilder AddWebserver(this IServiceCollection services, Action<WebserverOptions> setAction)
		=> services.Configure(setAction)
				   .AddWebserver();

	public static IWebserverBuilder AddWebserver(this IServiceCollection services)
		=> services.AddWebserverBuilder()
				   .AddCoreServices()
				   .AddMarkerService();

	private static IWebserverBuilder AddWebserverBuilder(this IServiceCollection services)
		=> new WebserverBuilder(services);
}
