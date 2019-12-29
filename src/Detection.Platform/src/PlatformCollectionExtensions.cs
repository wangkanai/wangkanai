// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class PlatformCollectionExtensions
    {
        // Concept idea on adding platform detection to client service
        public static IDetectionCoreBuilder AddPlatform(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<IPlatformResolver, PlatformResolver>();

            return builder;
        }
    }
}
