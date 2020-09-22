// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;

using Xunit;

namespace Wangkanai.Detection.Services
{
    public class BrowserServiceTest
    {
        [Fact]
        public void Null()
        {
            var resolver = MockService.Browser(null);
            Assert.NotNull(resolver);
            Assert.Equal(Browser.Unknown, resolver.Name);
            Assert.Equal(new Version(0, 0), resolver.Version);
        }

        [Fact]
        public void Unknown()
        {
            var resolver = MockService.Browser("");
            Assert.NotNull(resolver);
            Assert.Equal(Browser.Unknown, resolver.Name);
            Assert.Equal(new Version(0, 0), resolver.Version);
        }

        [Theory]
        [InlineData("1.0.154.53", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.19 (KHTML, like Gecko) Chrome/1.0.154.53 Safari/525.19")]
        [InlineData("75.0.3770.90", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.90 Atom/4.0.0.141 Safari/537.36")]
        [InlineData("8.0.552.237", "Mozilla/5.0 (X11; U; Linux x86_64; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Ubuntu/10.10 Chromium/8.0.552.237 Chrome/8.0.552.237 Safari/534.10")]
        public void Chrome(string version, string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.Chrome, resolver.Name);
            Assert.Equal(new Version(version), resolver.Version);
        }

        [Theory]
        [InlineData("11.0", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko")]
        [InlineData("11.0", "Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko")]
        [InlineData("10.0", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)")]
        [InlineData("9.0", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)")]
        public void InternetExplorer(string version, string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.InternetExplorer, resolver.Name);
            Assert.Equal(new Version(version), resolver.Version);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0 Safari/605.1.15")]
        [InlineData("Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1")]
        [InlineData("Mozilla/5.0 (Macintosh; U; PPC Mac OS X; de-ch) AppleWebKit/85 (KHTML, like Gecko) Safari/85")]
        public void Safari(string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.Safari, resolver.Name);
        }

        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)")]
        [InlineData("Mozilla/5.0 (Linux arm) Gecko/20110318 Firefox/4.0b13pre Fennec/4.0")]
        public void Firefox(string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.Firefox, resolver.Name);
        }

        [Theory]
        [InlineData("13.9200", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.9200")]
        [InlineData("18.19041", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.19041")]
        [InlineData("40.15063.0", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; Xbox; Xbox One) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 Edge/40.15063.0")]
        [InlineData("40.15254.369", "Mozilla/5.0 (Windows Mobile 10; Android 8.0.0; Microsoft; Lumia 950XL) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.116 Mobile Safari/537.36 Edge/40.15254.369")]
        [InlineData("79.0.309.43", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.74 Safari/537.36 Edg/79.0.309.43")]
        [InlineData("85.0.564.51", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36 Edg/85.0.564.51")]
        public void Edge(string version, string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.Edge, resolver.Name);
            Assert.Equal(new Version(version), resolver.Version);
        }

        [Theory]
        [InlineData("Opera/9.80 (X11; Linux i686; Ubuntu/14.10) Presto/2.12.388 Version/12.16")]
        [InlineData("Opera/9.80 (Macintosh; Intel Mac OS X 10.14.1) Presto/2.12.388 Version/12.16")]
        [InlineData("Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14")]
        [InlineData("Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.0) Opera 12.14")]
        public void Opera(string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.Opera, resolver.Name);
        }

        [Theory]
        [InlineData("x-men")]
        public void Others(string agent)
        {
            var resolver = MockService.Browser(agent);
            Assert.Equal(Browser.Others, resolver.Name);
        }
    }
}
