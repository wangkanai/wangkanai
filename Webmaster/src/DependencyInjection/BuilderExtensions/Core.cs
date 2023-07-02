// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Webmaster.DependencyInjection.Options;
using Wangkanai.Webmaster.Services;

namespace Microsoft.Extensions.DependencyInjection;

internal static class CoreCollectionExtensions
{
	internal static IWebmasterBuilder AddRequiredServices(this IWebmasterBuilder builder)
	{
		builder.ThrowIfNull();

		// Hosting doesn't add IHttpContextAccessor by default
		builder.Services.AddHttpContextAccessor();

		// Add Webmaster options
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider=> provider.GetRequiredService<IOptions<WebmasterOptions>>().Value);
		
		return builder;
	}

	internal static IWebmasterBuilder AddCoreServices(this IWebmasterBuilder builder)
	{
		// Add Basic core to services
		builder.Services.TryAddSingleton<IWebmasterService, WebmasterService>();

		return builder;
	}

	internal static IWebmasterBuilder AddMarkerService(this IWebmasterBuilder builder)
	{
		builder.Services.TryAddSingleton<WebmasterMarkerService>();

		return builder;
	}
}