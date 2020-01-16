// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;
using Wangkanai.Detection.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CrawlerBuilderExtensions
    {
        public static IDetectionCoreBuilder AddCrawlerService(this IDetectionCoreBuilder builder)
        {
            builder.Services.AddTransient<ICrawlerService, DefaultCrawlerService>();
            // waiting to retire
            builder.Services.AddTransient<ICrawlerResolver, CrawlerResolver>();

            return builder;
        }
    }
}
