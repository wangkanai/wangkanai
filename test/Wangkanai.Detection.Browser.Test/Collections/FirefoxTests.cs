// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Wangkanai.Detection.Collections
{
    public class FirefoxTests
    {
        [Fact]
        public void First()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0";
            // act
            var firefox = new Firefox(agent);
            // assert
            Assert.Equal(BrowserType.Firefox, firefox.Type);
            Assert.Equal("10.0", firefox.Version.ToString());
        }

        [Fact]
        public void Invalid()
        {
            // arrange
            var agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            // act
            var firefox = new Firefox(agent);
            // assert
            Assert.NotEqual(BrowserType.Firefox, firefox.Type);
            Assert.Null(firefox.Version);
        }
    }
}
