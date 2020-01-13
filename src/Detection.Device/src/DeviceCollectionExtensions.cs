// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DeviceCollectionExtensions
    {
        /// <summary>
        /// Adds the DeviceResolver service the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="builder">The <see cref="IDetectionCoreBuilder" /> to add services to</param>
        /// <returns>An <see cref="IDetectionCoreBuilder"/> that can be used to further configure the Detection services.</returns>
        public static IDetectionCoreBuilder AddDevice(
            this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<IDeviceResolver, DeviceResolver>();

            return builder;
        }
    }
}
