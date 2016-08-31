// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Browser;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension method to <see cref="IServiceCollection"/> for configuring client services.
    /// </summary>
    public static class ClientServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the default client service to the services container.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IClientBuilder"/> for creating and configuring the browser system.</returns>
        public static IClientBuilder AddBrowser(this IServiceCollection services)
        {
            if(services == null) throw new ArgumentNullException(nameof(services));

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Client Services            
            services.TryAddScoped<IClientService, ClientService>();

            return new ClientBuilder(services);
        }
    }
}
