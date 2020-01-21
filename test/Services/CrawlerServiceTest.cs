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
        public void UserAgentIsNull()
        {
            // arrange
            var service = MockService.CreateService(null);
            // act
            var crawler = new CrawlerService(service, null);

            Assert.NotNull(crawler);
        }

        [Fact]
        public void GoogleBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Google, resolver.Type);
            Assert.Equal(new Version(2, 1), resolver.Version);
        }

        [Fact]
        public void FacebookBot()
        {
            // arrange
            var userAgent = "facebookexternalhit/1.1 (+http://www.facebook.com/externalhit_uatext.php)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Facebook, resolver.Type);
            Assert.Equal(new Version(1, 1), resolver.Version);
        }

        [Fact]
        public void BingBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Bing, resolver.Type);
            Assert.Equal(new Version(2, 0), resolver.Version);
        }

        [Fact]
        public void TwitterBot()
        {
            // arrange
            var userAgent = "Twitterbot/1.0";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Twitter, resolver.Type);
            Assert.Equal(new Version(1, 0), resolver.Version);
        }

        [Fact]
        public void YahooBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Yahoo, resolver.Type);
            Assert.Equal(new Version(), resolver.Version);
        }

        [Fact]
        public void BaiduBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Baidu, resolver.Type);
            Assert.Equal(new Version(2, 0), resolver.Version);
        }

        [Fact]
        public void LinkedInBot()
        {
            // arrange
            var userAgent = "LinkedInBot/1.0 (compatible; Mozilla/5.0; Jakarta Commons-HttpClient/3.1 +http://www.linkedin.com)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.LinkedIn, resolver.Type);
            Assert.Equal(new Version(1, 0), resolver.Version);
        }

        [Fact]
        public void SkypeBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) SkypeUriPreview Preview/0.5";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Skype, resolver.Type);
        }

        [Fact]
        public void WhatsAppBot()
        {
            // arrange
            var userAgent = "WhatsApp/2.18.61 i";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.WhatsApp, resolver.Type);
            Assert.Equal(new Version(2, 18, 61), resolver.Version);
        }

        [Fact]
        public void OthersBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (compatible; SemrushBot/3~bl; +http://www.semrush.com/bot.html)";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Others, resolver.Type);
        }

        [Fact]
        public void UnknownBot()
        {
            // arrange
            var userAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:10.0) Gecko/20100101 Firefox/10.0";
            var service   = MockService.CreateService(userAgent);

            // act
            var resolver = new CrawlerService(service, null);

            // assert
            Assert.False(resolver.IsCrawler);
            Assert.Equal(Crawler.Unknown, resolver.Type);
        }

        [Fact]
        public void OptionCrawlerForOthers()
        {
            // arrange
            var userAgent = "starnic";
            var service   = MockService.CreateService(userAgent);
            var options   = new DetectionOptions();
            options.Crawler.Others.Add("starnic");
            // act
            var resolver = new CrawlerService(service, options);

            // assert
            Assert.True(resolver.IsCrawler);
            Assert.Equal(Crawler.Others, resolver.Type);
        }
    }
}
