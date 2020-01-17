// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension method to <see cref="IServiceCollection"/> for configuring client services.
    /// </summary>
    public static class CoreBuilderExtensions
    {
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

            services.AddOptions();
            // Add Basic core to services
            services.TryAddTransient<IUserAgentService, DefaultUserAgentService>();
            services.TryAddTransient<IDeviceService, DefaultDeviceService>();
            services.TryAddTransient<IEngineService, DefaultEngineService>();
            services.TryAddTransient<IPlatformService, DefaultPlatformService>();
            services.TryAddTransient<IBrowserService, DefaultBrowserService>();
            services.TryAddTransient<ICrawlerService, DefaultCrawlerService>();
            services.TryAddTransient<IDetectionService, DefaultDetectionService>();

            // Add Advance featuures to service
            services.TryAddTransient<IResponsiveService, DefaultResponsiveService>();

            // Completed adding services
            services.TryAddSingleton<DetectionMarkerService, DetectionMarkerService>();

            return new DetectionCoreBuilder(services);
        }
    }
}
