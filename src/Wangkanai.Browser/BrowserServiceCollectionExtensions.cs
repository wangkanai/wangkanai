using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Browser;
using Wangkanai.Browser.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class BrowserServiceCollectionExtensions
    {
        public static IServiceCollection AddBrowserDetector(this IServiceCollection services)
        {
            if(services == null) throw new ArgumentNullException(nameof(services));

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Browser Services            
            services.TryAddTransient<IBrowserDetector, BrowserDetector>();

            return services;
        }
    }
}
