using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    public static class CrawlerHttpRequestExtensions
    {
        public static ICrawler Crawler(this HttpRequest request)
        {
            var service = new UserAgentService(request.HttpContext);
            var resolver = new CrawlerResolver(service);
            return resolver.Crawler;
        }
    }
}
