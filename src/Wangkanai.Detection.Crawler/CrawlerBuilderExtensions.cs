using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CrawlerBuilderExtensions
    {
        public static IDetectionBuilder AddCrawler(this IDetectionBuilder builder)
        {
            builder.Services.AddTransient<ICrawlerResolver, CrawlerResolver>();

            return builder;
        }
    }
}
