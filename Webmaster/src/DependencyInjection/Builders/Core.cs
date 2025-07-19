// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Webmaster;
using Wangkanai.Webmaster.Services;

namespace Microsoft.Extensions.DependencyInjection;

internal static class WebmasterCoreCollectionBuilderExtensions
{
	internal static IWebmasterBuilder AddRequiredServices(this IWebmasterBuilder builder)
	{
		builder.ThrowIfNull();

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<WebmasterOptions>>().Value);

		builder.Services.AddDetection();

		return builder;
	}

	internal static IWebmasterBuilder AddCoreServices(this IWebmasterBuilder builder)
	{
		// Add Basic core to services
		builder.Services.TryAddScoped<IWebmasterService, WebmasterService>();

		return builder;
	}

	internal static IWebmasterBuilder AddMarkerService(this IWebmasterBuilder builder)
	{
		builder.Services.TryAddSingleton<WebmasterMarkerService>();

		return builder;
	}
}
