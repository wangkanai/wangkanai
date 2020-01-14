// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BrowserCollectionExtensions
    {
        /// <summary>
        /// Adds the BrowserResolver service to the specific <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="builder">The <see cref="IDetectionCoreBuilder"/> to add services to</param>
        /// <returns>An <see cref="IDetectionCoreBuilder"/> that can be used to further configure the Detection services.</returns>
        public static IDetectionCoreBuilder AddBrowserService(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<IBrowserResolver, BrowserResolver>();

            return builder;
        }
    }
}
