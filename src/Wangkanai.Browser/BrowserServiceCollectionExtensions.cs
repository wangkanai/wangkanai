using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class BrowserServiceCollectionExtensions
    {
        public static IServiceCollection AddBrowserDetector(this IServiceCollection services)
        {
            return services;
        }
    }
}
