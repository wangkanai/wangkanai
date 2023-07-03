// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Detection;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Contains extension method to <see cref="IServiceCollection" /> for configuring client services.
/// </summary>
internal static class DetectionCoreBuilderExtensions
{
	internal static IDetectionBuilder AddRequiredServices(this IDetectionBuilder builder)
	{
		builder.ThrowIfNull();

		// Hosting doesn't add IHttpContextAccessor by default
		builder.Services.AddHttpContextAccessor();

		// Add Detection Options
		builder.Services.AddOptions();
		builder.Services.TryAddSingleton(provider => provider.GetRequiredService<IOptions<DetectionOptions>>().Value);

		return builder;
	}

	internal static IDetectionBuilder AddCoreServices(this IDetectionBuilder builder)
	{
		// Add Basic core to services
		builder.Services.TryAddScoped<IHttpContextService, HttpContextService>();
		builder.Services.TryAddScoped<IUserAgentService, UserAgentService>();
		builder.Services.TryAddScoped<IDeviceService, DeviceService>();
		builder.Services.TryAddScoped<IEngineService, EngineService>();
		builder.Services.TryAddScoped<IPlatformService, PlatformService>();
		builder.Services.TryAddScoped<IBrowserService, BrowserService>();
		builder.Services.TryAddScoped<ICrawlerService, CrawlerService>();
		builder.Services.TryAddScoped<IDetectionService, DetectionService>();

		return builder;
	}

	internal static IDetectionBuilder AddMarkerService(this IDetectionBuilder builder)
	{
		builder.Services.TryAddSingleton<DetectionMarkerService>();

		return builder;
	}
}