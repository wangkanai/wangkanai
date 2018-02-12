// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class BrowserClassTests
    {
        [Fact]
        public void cast_valid_type()
        {
            Browser browser = null;
            try
            {
                browser = new Browser("chrome");
            }
            catch (BrowserNotFoundException) { }

            Assert.Equal(BrowserType.Chrome, browser.Type);
        }

        [Fact]
        public void cast_somthing_not_valid()
        {
            Type actual = null;
            Browser browser = null;
            try {
                browser = new Browser("xxx");
            }
            catch (BrowserNotFoundException e) {
                actual = e.GetType();
            }
            Assert.Equal(typeof(BrowserNotFoundException), actual);
        }
    }
}
