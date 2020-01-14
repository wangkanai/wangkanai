// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

using Moq;

using Wangkanai.Detection.Services;

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
        [InlineData("Mozilla/5.0 (compatible; SemrushBot/3~bl; +http://www.semrush.com/bot.html)")]
        [InlineData("Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106")]
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
        public void CrawlerAgentNull()
        {
            var service = CreateService(null);
            var resolver = new CrawlerResolver(service);

            Assert.NotNull(resolver.Crawler);
            Assert.Null(resolver.Crawler.Name);
        }

        [Fact]
        public void CrawlerAgentEmpty()
        {
            var service = CreateService("");
            var resolver = new CrawlerResolver(service);

            Assert.NotNull(resolver.Crawler);
            Assert.Null(resolver.Crawler.Name);
        }

        [Fact]
        public void Facebot_Bot()
        {
            var agent = "Facebot";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("Facebot", resolver.Crawler.Name);
            Assert.Equal(Crawler.Facebook, resolver.Crawler.Type);
        }

        [Fact]
        public void Facebookbot_Bot()
        {
            var agent = "facebookexternalhit/1.1 (+http://www.facebook.com/externalhit_uatext.php)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("facebookexternalhit", resolver.Crawler.Name);
            Assert.Equal(Crawler.Facebook, resolver.Crawler.Type);
        }

        [Fact]
        public void Twitterbot_Bot()
        {
            var agent = "Twitterbot/1.0";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("Twitterbot", resolver.Crawler.Name);
            Assert.Equal(Crawler.Twitter, resolver.Crawler.Type);
        }

        [Fact]
        public void LinkedInBot_Bot()
        {
            var agent = "LinkedInBot/1.0 (compatible; Mozilla/5.0; Jakarta Commons-HttpClient/3.1 +http://www.linkedin.com)";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("LinkedInBot", resolver.Crawler.Name);
            Assert.Equal(Crawler.LinkedIn, resolver.Crawler.Type);
        }

        [Fact]
        public void Skype_Messenger()
        {
            var agent = "Mozilla/5.0 (Windows NT 6.1; WOW64) SkypeUriPreview Preview/0.5";
            var service = CreateService(agent);
            // act
            var resolver = new CrawlerResolver(service);

            Assert.Equal("SkypeUriPreview", resolver.Crawler.Name);
            Assert.Equal(Crawler.Skype, resolver.Crawler.Type);
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
            Assert.Equal(Crawler.Google, resolver.Crawler.Type);
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
            Assert.Equal(Crawler.Bing, resolver.Crawler.Type);
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
            Assert.Equal(Crawler.Yahoo, resolver.Crawler.Type);
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
            Assert.Equal(Crawler.Baidu, resolver.Crawler.Type);
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
