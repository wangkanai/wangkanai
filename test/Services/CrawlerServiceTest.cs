// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Moq;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class CrawlerServiceTest
    {
        [Fact]
        public void Null()
        {
            var resolver = MockCrawlerService(null);
            Assert.NotNull(resolver);
            Assert.Equal(Crawler.Unknown, resolver.Type);
        }

        [Fact]
        public void Unknown()
        {
            var agent    = "Mozilla/5.0 (X11; Linux x86_64; rv:10.0) Gecko/20100101 Firefox/10.0";
            var resolver = MockCrawlerService(agent);
            Assert.False(resolver.IsCrawler);
            Assert.Equal(Crawler.Unknown, resolver.Type);
        }

        [Fact]
        public void Google()
        {
            var agent   = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
            var crawler = MockCrawlerService(agent);
            Assert.True(crawler.IsCrawler);
            Assert.Equal(Crawler.Google, crawler.Type);
            Assert.Equal(new Version(2, 1), crawler.Version);
        }

        [Fact]
        public void Facebook()
        {
            var agent   = "facebookexternalhit/1.1 (+http://www.facebook.com/externalhit_uatext.php)";
            var crawler = MockCrawlerService(agent);
            Assert.True(crawler.IsCrawler);
            Assert.Equal(Crawler.Facebook, crawler.Type);
            Assert.Equal(new Version(1, 1), crawler.Version);
        }

        [Fact]
        public void BingBot()
        {
            var agent    = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Bing, resolver.Type);
            Assert.Equal(new Version(2, 0), resolver.Version);
        }

        [Fact]
        public void Twitter()
        {
            var agent    = "Twitterbot/1.0";
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Twitter, resolver.Type);
            Assert.Equal(new Version(1, 0), resolver.Version);
        }

        [Fact]
        public void Yahoo()
        {
            var agent    = "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)";
            var resolver = MockCrawlerService(agent);
            Assert.Equal(Crawler.Yahoo, resolver.Type);
            Assert.Equal(new Version(), resolver.Version);
        }

        [Fact]
        public void Baidu()
        {
            var agent    = "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)";
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Baidu, resolver.Type);
            Assert.Equal(new Version(2, 0), resolver.Version);
        }

        [Fact]
        public void LinkedIn()
        {
            var agent    = "LinkedInBot/1.0 (compatible; Mozilla/5.0; Jakarta Commons-HttpClient/3.1 +http://www.linkedin.com)";
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.LinkedIn, resolver.Type);
            Assert.Equal(new Version(1, 0), resolver.Version);
        }

        [Fact]
        public void Skype()
        {
            var agent    = "Mozilla/5.0 (Windows NT 6.1; WOW64) SkypeUriPreview Preview/0.5";
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Skype, resolver.Type);
        }

        [Fact]
        public void WhatsApp()
        {
            var agent    = "WhatsApp/2.18.61 i";
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.WhatsApp, resolver.Type);
            Assert.Equal(new Version(2, 18, 61), resolver.Version);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (compatible; SemrushBot/3~bl; +http://www.semrush.com/bot.html)")]
        [InlineData("Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106")]
        public void Others(string agent)
        {
            var resolver = MockCrawlerService(agent);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Others, resolver.Type);
        }

        [Fact]
        public void OptionCrawlerForOthers()
        {
            var agent   = "starnic";
            var options = new DetectionOptions();
            options.Crawler.Others.Add("starnic");
            var resolver = MockCrawlerService(agent, options);
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Others, resolver.Type);
        }

        private static CrawlerService MockCrawlerService(string agent, DetectionOptions options = null)
        {
            var service  = MockService.CreateService(agent);
            var resolver = new CrawlerService(service, options);
            return resolver;
        }
    }
}
