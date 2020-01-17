// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CrawlerBuilderExtensions
    {
        [Obsolete("3.0-Alpha02 Will merge this into the adding core services DI.")]
        public static IDetectionCoreBuilder AddCrawler(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<ICrawlerResolver, CrawlerResolver>();

            return builder;
        }
    }
}
