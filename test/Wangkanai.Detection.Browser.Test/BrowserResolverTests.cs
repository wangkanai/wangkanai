// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Moq;
using System;
using Xunit;

namespace Wangkanai.Detection.Test
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
            Assert.Equal(BrowserType.Opera, resolver.Browser.Type);
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
            Assert.Equal(BrowserType.Edge, resolver.Browser.Type);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)")]
        [InlineData("Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko")]
        public void Resolve_IE(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new BrowserResolver(service);
            // assert
            Assert.Equal(BrowserType.IE, resolver.Browser.Type);
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
            Assert.Equal(BrowserType.Safari, resolver.Browser.Type);
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
            Assert.Equal(BrowserType.Firefox, resolver.Browser.Type);
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
