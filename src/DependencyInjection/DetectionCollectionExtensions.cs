// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Detection;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Contains extension method to <see cref="IServiceCollection" /> for configuring client services.
    /// </summary>
    public static class DetectionCollectionExtensions
    {
        public static IDetectionBuilder AddDetectionBuilder(this IServiceCollection services)
        {
            return new DetectionBuilder(services);
        }

        public static IDetectionBuilder AddDetection(this IServiceCollection services,
            Action<DetectionOptions> setAction)
        {
            services.Configure(setAction);
            return services.AddDetection();
        }

        public static IDetectionBuilder AddDetection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DetectionOptions>(configuration);
            return services.AddDetection();
        }

        /// <summary>
        ///     Adds the default client service to the services container.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IDetectionBuilder AddDetection(this IServiceCollection services)
        {
            var builder = services.AddDetectionBuilder();

            builder.AddRequiredPlatformServices();
            builder.AddCoreServices();
            builder.AddResponsive();
            builder.AddMarkerService();

            builder.Services.AddDetectionCore().AddBrowser();

            return builder;
        }

        #region deprecated

        private static IDetectionCoreBuilder AddDetectionCore(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add Basic core to services
            services.TryAddTransient<IUserAgentService, UserAgentService>();

            // Completed adding services
            services.TryAddSingleton<MarkerService, MarkerService>();

            return new DetectionCoreBuilder(services);
        }

        private static IDetectionCoreBuilder AddBrowser(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<IBrowserResolver, BrowserResolver>();

            return builder;
        }

        [Obsolete]
        private interface IDetectionCoreBuilder
        {
            IServiceCollection Services { get; }
        }

        [Obsolete]
        private class DetectionCoreBuilder : IDetectionCoreBuilder
        {
            public DetectionCoreBuilder(IServiceCollection services)
            {
                Services = services ?? throw new ArgumentNullException(nameof(services));
            }

            public IServiceCollection Services { get; }
        }

        #endregion
    }
}
