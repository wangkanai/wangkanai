// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Collections
{
    public class InternetExplorerTests
    {
        [Fact]
        public void Before_IE11()
        {
            // arrange
            var agent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            // act
            var ie = new InternetExplorer(agent);
            // assert
            Assert.Equal(BrowserType.IE, ie.Type);
            Assert.Equal("10.0", ie.Version.ToString());
        }

        [Fact]
        public void On_IE11()
        {
            // arrange
            var agent = "Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko";
            // act
            var ie = new InternetExplorer(agent);
            // assert
            Assert.Equal(BrowserType.IE, ie.Type);
            Assert.Equal("11.0", ie.Version.ToString());
        }

        [Fact]
        public void NotIE()
        {
            // arrange
            var agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            // act
            var ie = new InternetExplorer(agent);
            // assert
            Assert.NotEqual(BrowserType.IE, ie.Type);
            Assert.Null(ie.Version);
        }
    }
}
