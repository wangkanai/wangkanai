// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Wangkanai.Detection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DeviceBuilderExtensions
    {
        /// <summary>
        /// Adds the DeviceResolver service the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="builder">The <see cref="IDetectionBuilder" /> to add services to</param>
        /// <returns>An <see cref="IDetectionBuilder"/> that can be used to further configure the Detection services.</returns>
        public static IDetectionBuilder AddDevice(this IDetectionBuilder builder)
        {
            builder.Services.AddTransient<IDeviceResolver, DeviceResolver>();

            return builder;
        }
    }
}
