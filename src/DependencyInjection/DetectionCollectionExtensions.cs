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
    /// Contains extension method to <see cref="IServiceCollection"/> for configuring client services.
    /// </summary>
    public static class DetectionCollectionExtensions
    {
        public static IDetectionBuilder AddDetectionBuilder(this IServiceCollection services)
        {
            return new DetectionBuilder(services);
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

        #region deprecated
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
            services.TryAddTransient<IUserAgentService, UserAgentService>();

            // Completed adding services
            services.TryAddSingleton<MarkerService, MarkerService>();

            return new DetectionCoreBuilder(services);
        }

        /// <summary>
        /// Adds the DeviceResolver service the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="builder">The <see cref="IDetectionCoreBuilder" /> to add services to</param>
        /// <returns>An <see cref="IDetectionCoreBuilder"/> that can be used to further configure the Detection services.</returns>
        public static IDetectionCoreBuilder AddDevice(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<IDeviceResolver, DeviceResolver>();

            return builder;
        }

        /// <summary>
        /// Adds the BrowserResolver service to the specific <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="builder">The <see cref="IDetectionCoreBuilder"/> to add services to</param>
        /// <returns>An <see cref="IDetectionCoreBuilder"/> that can be used to further configure the Detection services.</returns>
        public static IDetectionCoreBuilder AddBrowser(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<IBrowserResolver, BrowserResolver>();

            return builder;
        }

        /// <summary>
        /// Detection Core builder Interface
        /// </summary>
        [Obsolete]
        public interface IDetectionCoreBuilder
        {
            /// <summary>
            /// Gets the services.
            /// </summary>
            /// <value>
            /// The services.
            /// </value>
            IServiceCollection Services { get; }
        }

        [Obsolete]
        public class DetectionCoreBuilder : IDetectionCoreBuilder
        {
            /// <summary>
            /// Creates a new instance of <see cref="DetectionCoreBuilder"/>.
            /// </summary>
            /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
            public DetectionCoreBuilder(IServiceCollection services)
            {
                if (services is null)
                    throw new ArgumentNullException(nameof(services));

                Services = services;
            }

            /// <summary>
            /// Gets the <see cref="IServiceCollection"/> services are attached to.
            /// </summary>
            /// <value>
            /// The <see cref="IServiceCollection"/> services are attached to.
            /// </value>
            public IServiceCollection Services { get; private set; }
        }
        #endregion
    }
}
