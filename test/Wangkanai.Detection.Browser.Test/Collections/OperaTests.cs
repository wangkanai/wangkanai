// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Collections
{
    public class OperaTests
    {
        [Fact]
        public void First_version_15_above()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.73 Safari/537.36 OPR/34.0.2036.42";
            // act
            var opera = new Opera(agent);
            // assert
            Assert.Equal(BrowserType.Opera, opera.Type);
            Assert.Equal("34.0.2036.42", opera.Version.ToString());
        }

        [Fact]
        public void Second_version_12()
        {
            // arrange
            var agent = "Opera/9.80 (X11; Linux i686; Ubuntu/14.10) Presto/2.12.388 Version/12.16";
            // act
            var opera = new Opera(agent);
            // assert
            Assert.Equal(BrowserType.Opera, opera.Type);
            Assert.Equal("12.16", opera.Version.ToString());
        }
        [Fact]
        public void Invalid()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0";
            // act
            var opera = new Opera(agent);
            // assert
            Assert.NotEqual(BrowserType.Opera, opera.Type);
            Assert.Null(opera.Version);
        }
    }
}
