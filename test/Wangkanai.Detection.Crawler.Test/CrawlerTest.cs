// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class CrawlerTest
    {
        [Theory]
        [InlineData("Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)")]
        [InlineData("Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)")]
        [InlineData("Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)")]
        [InlineData("Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)")]
        public void Keyword(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);
            // assert
            Assert.NotNull(resolver.Crawler);
        }

        private IDetectionService CreateService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IDetectionService>();
            service.Setup(f => f.Context).Returns(context);
            service.Setup(f => f.UserAgent).Returns(new UserAgent(agent));
            return service.Object;
        }

        private HttpContext CreateContext(string value)
        {
            var context = new DefaultHttpContext();
            var header = "User-Agent";
            context.Request.Headers.Add(header, new[] { value });
            return context;
        }
    }
}