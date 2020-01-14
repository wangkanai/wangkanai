// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Responsive.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ASP.NET Core middleware for routing to specific area base client request device
    /// Extension method for setting up Universal in an <see cref="IServiceCollection" />
    /// </summary>
    public static class ResponsiveCoreCollectionExtensions
    {
        /// <summary>
        /// Adds services required for application Responsive.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IResponsiveCoreBuilder AddResponsiveCore(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddDetection();

            services.AddOptions();
            services.TryAddSingleton<ResponsiveMarkerService, ResponsiveMarkerService>();

            return new ResponsiveCoreBuilder(services);
        }
    }
}
