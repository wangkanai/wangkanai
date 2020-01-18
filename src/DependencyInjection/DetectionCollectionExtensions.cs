// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.Extensions.Configuration;

using Wangkanai.Detection.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension method to <see cref="IServiceCollection"/> for configuring client services.
    /// </summary>
    public static class DetectionCollectionExtensions
    {
        public static IDetectionBuilder AddDetectionBuilder(this IServiceCollection services)
        {
            return new DetectionBuilder(services);
        }

        /// <summary>
        /// Adds the default client service to the services container.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IDetectionBuilder AddDetection(this IServiceCollection services)
        {
            var builder = services.AddDetectionBuilder();

            builder.AddRequiredPlatformServices();
            builder.AddCoreServices();
            // Will evaluate if need to add more advance services
            //builder.AddResponsive();
            builder.AddMarkerService();

            builder.Services.AddDetectionCore()
                .AddDevice()
                .AddBrowser()
                .AddResponsive();

            return builder;
        }

        public static IDetectionBuilder AddDetection(this IServiceCollection services, Action<DetectionOptions> setAction)
        {
            services.Configure(setAction);
            return services.AddDetection();
        }

        public static IDetectionBuilder AddDetection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DetectionOptions>(configuration);
            return services.AddDetection();
        }
    }
}
