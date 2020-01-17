// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.DependencyInjection.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension method to <see cref="IServiceCollection"/> for configuring client services.
    /// </summary>
    public static class DetectionCollectionExtensions
    {
        /// <summary>
        /// Adds the default client service to the services container.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IDetectionBuilder AddDetection(
            this IServiceCollection services)
        {
            return services.AddDetection(options => { });
        }

        public static IDetectionBuilder AddDetection(
            this IServiceCollection services,
            Action<DetectionOptions> setAction            )
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.Configure<DetectionOptions>(setAction);

            services
                .AddDetectionCore()
                .AddDevice()
                .AddBrowser()
                .AddPlatform()
                .AddEngine()
                .AddCrawler()
                .AddResponsive();

            return new DetectionBuilder(services);
        }
    }
}
