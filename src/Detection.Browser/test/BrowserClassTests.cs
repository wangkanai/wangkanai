// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Xunit;

namespace Wangkanai.Detection.Test
{
    public class BrowserClassTests
    {
        [Fact]
        public void cast_valid_type()
        {
            BrowserFactory browser = null;
            try
            {
                browser = new BrowserFactory("chrome");
            }
            catch (BrowserNotFoundException) { }

            Assert.Equal(Browser.Chrome, browser.Type);
        }

        [Fact]
        public void cast_somthing_not_valid()
        {
            Type actual = null;
            BrowserFactory browser = null;
            try
            {
                browser = new BrowserFactory("xxx");
            }
            catch (BrowserNotFoundException e)
            {
                actual = e.GetType();
            }
            Assert.Equal(typeof(BrowserNotFoundException), actual);
        }
    }
}
