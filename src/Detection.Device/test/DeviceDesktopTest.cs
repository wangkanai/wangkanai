// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DeviceDesktopTest : DeviceTestAbstract
    {
        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT x.y; WOW64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36")]
        [InlineData("Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Macintosh; PPC Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (X11; Linux i686; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (X11; Linux x86_64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        public void Keyword(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new DeviceResolver(service);
            // assert
            Assert.Equal(Device.Desktop, resolver.Device.Type);
        }
    }
}
