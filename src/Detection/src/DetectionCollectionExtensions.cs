// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;
using Wangkanai.Detection.Builders;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        public static IDetectionBuilder AddDetection(this IServiceCollection services)
        {
            if (services == null) throw new AddDetectionArgumentNullException(nameof(services));

            services.AddDetectionCore()
                .AddDevice()
                .AddBrowser()
                .AddPlatform()
                .AddEngine()
                .AddCrawler();

            services.TryAddTransient<IDetection, Detection>();

            return new DetectionBuilder(services);
        }
    }
}
