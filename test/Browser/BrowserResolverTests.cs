// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

using Moq;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

using Xunit;

namespace Wangkanai.Detection.BrowserTest
{
    public class BrowserResolverTests
    {
        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.73 Safari/537.36 OPR/34.0.2036.42")]
        [InlineData("Opera / 9.80(X11; Linux i686; Ubuntu / 14.10) Presto/2.12.388 Version/12.16")]
        public void Resolve_Opera(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Opera, resolver.Browser.Type);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393")]
        public void Resolve_Edge(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Edge, resolver.Browser.Type);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)")]
        [InlineData("Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko")]
        [InlineData("Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko")]
        public void Resolve_IE(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.IE, resolver.Browser.Type);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1")]
        public void Resolve_Safari(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Safari, resolver.Browser.Type);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        public void Resolve_FireFox(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Firefox, resolver.Browser.Type);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)")]
        public void Resolve_FireFox_via_GoogleProxy(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Firefox, resolver.Browser.Type);
            Assert.Equal(11, resolver.Browser.Version.Major);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0-Beta")]
        public void Resolve_FireFox_InvalidVersion(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Firefox, resolver.Browser.Type);
            Assert.Equal(11, resolver.Browser.Version.Major);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (BB10; Touch) AppleWebKit/537.10+ (KHTML, like Gecko) Version/10.0.9.2372 Mobile Safari/537.10+")]
        public void Resolve_Blackberry_InvalidVersion(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Safari, resolver.Browser.Type);
            Assert.Equal(537, resolver.Browser.Version.Major);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106")]
        [InlineData("Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots) AppleWebKit/537.36 (KHTML, like Gecko) Chrome")]
        [InlineData("Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/NoIdea")]
        public void Resolve_Bot_As_Chrome_Browser(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(Browser.Chrome, resolver.Browser.Type);
        }

        [Fact]
        public void Resolve_NullAgent()
        {
            // arrange
            var service = CreateService(null);
            // act
            var resolver = new BrowserResolver(service);
            // assert
        }

        private IUserAgentService CreateService(string agent)
        {
            var context = CreateContext(agent);
            var service = new Mock<IUserAgentService>();
            // this can be left out, due to the resolver doesn't require HttpContext
            //service.Setup(f => f.Context).Returns(context);
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
