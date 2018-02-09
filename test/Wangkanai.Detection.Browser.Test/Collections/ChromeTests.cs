// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Collections
{
    public class ChromeTests
    {
        [Fact]
        public void First()
        {
            // arrange
            var agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.3";
            // act
            var chrome = new Chrome(agent);
            // assert
            Assert.Equal(BrowserType.Chrome, chrome.Type);
            Assert.Equal("51.0.2704.103", chrome.Version.ToString());
        }

        [Fact]
        public void Invalid()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0";
            // act
            var chrome = new Chrome(agent);
            // assert
            Assert.NotEqual(BrowserType.Chrome, chrome.Type);
            Assert.Null(chrome.Version);
        }
    }
}
