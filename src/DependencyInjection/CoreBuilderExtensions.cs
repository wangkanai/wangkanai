// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.Detection.DependencyInjection.Options;
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
            // Hosting doesn't add IHttpContextAccessor by default
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add Detection Options
            builder.Services.AddOptions();
            builder.Services.TryAddSingleton(
                resolver => resolver.GetRequiredService<IOptions<DetectionOptions>>().Value);

            return builder;
        }

        public static IDetectionBuilder AddCoreServices(this IDetectionBuilder builder)
        {
            // Add Basic core to services
            builder.Services.TryAddTransient<IUserAgentService, DefaultUserAgentService>();
            builder.Services.TryAddTransient<IDeviceService, DefaultDeviceService>();
            builder.Services.TryAddTransient<IEngineService, DefaultEngineService>();
            builder.Services.TryAddTransient<IPlatformService, DefaultPlatformService>();
            builder.Services.TryAddTransient<IBrowserService, DefaultBrowserService>();
            builder.Services.TryAddTransient<ICrawlerService, DefaultCrawlerService>();
            builder.Services.TryAddTransient<IDetectionService, DefaultDetectionService>();

            return builder;
        }

        public static IDetectionBuilder AddMarkerService(this IDetectionBuilder builder)
        {
            builder.Services.TryAddSingleton<DetectionMarkerService, DetectionMarkerService>();

            return builder;
        }

        /// <summary>
        /// Adds the default client service to the services container.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IDetectionCoreBuilder AddDetectionCore(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add Basic core to services
            services.TryAddTransient<IUserAgentService, DefaultUserAgentService>();

            // Completed adding services
            services.TryAddSingleton<DetectionMarkerService, DetectionMarkerService>();

            return new DetectionCoreBuilder(services);
        }
    }
}
