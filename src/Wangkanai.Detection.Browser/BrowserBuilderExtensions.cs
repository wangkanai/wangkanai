// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Wangkanai.Detection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class BrowserBuilderExtensions
    {
        /// <summary>
        /// Adds the BrowserResolver service to the specific <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="builder">The <see cref="IDetectionBuilder"/> to add services to</param>
        /// <returns>An <see cref="IDetectionBuilder"/> that can be used to further configure the Detection services.</returns>
        public static IDetectionBuilder AddBrowser(this IDetectionBuilder builder)
        {
            builder.Services.AddTransient<IBrowserResolver, BrowserResolver>();

            return builder;
        }
    }
}
