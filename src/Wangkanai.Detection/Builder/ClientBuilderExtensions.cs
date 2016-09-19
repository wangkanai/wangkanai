// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Wangkanai.Detection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{    
    public static class ClientBuilderExtensions
    {
        public static IClientBuilder AddDevice(this IClientBuilder builder)
        {
            builder.Services.AddTransient<IDeviceResolver, DeviceResolver>();

            return builder;
        }
        // concept idea on adding platform detection to client service
        public static IClientBuilder AddBrowser(this IClientBuilder builder)
        {
            return builder;
        }
        // Concept idea on adding platform detection to client service
        public static IClientBuilder AddPlatform(this IClientBuilder builder)
        {
            return builder;
        }

        // Concept idea on adding engine detection to client service
        public static IClientBuilder AddEngine(this IClientBuilder builder)
        {
            return builder;
        }
    }
}
