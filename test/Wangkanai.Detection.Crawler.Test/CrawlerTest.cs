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
        [InlineData("Facebot")]
        [InlineData("facebookexternalhit/1.1 (+http://www.facebook.com/externalhit_uatext.php)")]
        [InlineData("Mozilla/5.0 (Windows NT 6.1; WOW64) SkypeUriPreview Preview/0.5")]
        [InlineData("Twitterbot/1.0")]
        [InlineData("LinkedInBot/1.0 (compatible; Mozilla/5.0; Jakarta Commons-HttpClient/3.1 +http://www.linkedin.com)")]
        [InlineData("Mozilla/5.0 (compatible; Google-Structured-Data-Testing-Tool +https://search.google.com/structured-data/testing-tool)")]
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

        [Fact]
        public void Facebot_Bot() {
            var agent = "Facebot";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("Facebot", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Facebook, resolver.Crawler.Type);
        }

        [Fact]
        public void Facebookbot_Bot() {
            var agent = "facebookexternalhit/1.1 (+http://www.facebook.com/externalhit_uatext.php)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("facebookexternalhit", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Facebook, resolver.Crawler.Type);
        }

        [Fact]
        public void Twitterbot_Bot() {
            var agent = "Twitterbot/1.0";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("Twitterbot", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Twitter, resolver.Crawler.Type);
        }

        [Fact]
        public void LinkedInBot_Bot() {
            var agent = "LinkedInBot/1.0 (compatible; Mozilla/5.0; Jakarta Commons-HttpClient/3.1 +http://www.linkedin.com)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("LinkedInBot", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.LinkedIn, resolver.Crawler.Type);
        }

        [Fact]
        public void Skype_Messenger() {
            var agent = "Mozilla/5.0 (Windows NT 6.1; WOW64) SkypeUriPreview Preview/0.5";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("SkypeUriPreview", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Skype, resolver.Crawler.Type);
        }

        [Fact]
        public void Google_Bot()
        {
            // arrange
            var agent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);
            // assert
            Assert.Equal("Googlebot", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Google, resolver.Crawler.Type);
        }

        [Fact]
        public void Bing_Bot()
        {
            // arrange
            var agent = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);
            // assert
            Assert.Equal("bingbot", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Bing, resolver.Crawler.Type);
        }

        [Fact]
        public void Yahoo_Bot()
        {
            // arrange
            var agent = "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);
            // assert
            Assert.Equal("Yahoo!Slurp", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Yahoo, resolver.Crawler.Type);
        }

        [Fact]
        public void Baidu_Bot()
        {
            // arrange
            var agent = "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);
            // assert
            Assert.Equal("Baiduspider", resolver.Crawler.Name);
            Assert.Equal(CrawlerType.Baidu, resolver.Crawler.Type);
        }

        private IUserAgentService CreateService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IUserAgentService>();
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