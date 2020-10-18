// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Wangkanai.Detection.DependencyInjection;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension method to <see cref="IServiceCollection"/> for configuring client services.
    /// </summary>
    public static class CoreBuilderExtensions
    {
        public static IDetectionBuilder AddRequiredPlatformServices(this IDetectionBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            
            // Hosting doesn't add IHttpContextAccessor by default
            builder.Services.AddHttpContextAccessor();

            // Add Detection Options
            builder.Services.AddOptions();
            builder.Services.TryAddSingleton(
                provider => provider.GetRequiredService<IOptions<DetectionOptions>>().Value);

            return builder;
        }

        public static IDetectionBuilder AddCoreServices(this IDetectionBuilder builder)
        {
            // Add Basic core to services
            builder.Services.TryAddScoped<IUserAgentService, UserAgentService>();
            builder.Services.TryAddScoped<IDeviceService, DeviceService>();
            builder.Services.TryAddScoped<IEngineService, EngineService>();
            builder.Services.TryAddScoped<IPlatformService, PlatformService>();
            builder.Services.TryAddScoped<IBrowserService, BrowserService>();
            builder.Services.TryAddScoped<ICrawlerService, CrawlerService>();
            builder.Services.TryAddScoped<IDetectionService, DetectionService>();

            return builder;
        }

        public static IDetectionBuilder AddMarkerService(this IDetectionBuilder builder)
        {
            builder.Services.TryAddSingleton<DetectionMarkerService, DetectionMarkerService>();

            return builder;
        }
    }
}