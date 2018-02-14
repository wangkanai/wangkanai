// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Collections
{
    public class SafariTests
    {
        [Fact]
        public void First()
        {
            // arrange
            var agent = "Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1";
            // act
            var safari = new Safari(agent);
            // assert
            Assert.Equal(BrowserType.Safari, safari.Type);
            Assert.Equal("601.1", safari.Version.ToString());
        }

        [Fact]
        public void Invalid()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0";
            // act
            var safari = new Safari(agent);
            // assert
            Assert.NotEqual(BrowserType.Safari, safari.Type);
            Assert.Null(safari.Version);
        }
    }
}
