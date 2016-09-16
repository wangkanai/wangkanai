// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Moq;
using Xunit;

namespace Wangkanai.Browser.Test
{
    public class DeviceTabletTest : DeviceTestAbstract
    {
        [Theory]
        [InlineData("Mozilla/5.0 (Android 4.4; Tablet; rv:41.0) Gecko/41.0 Firefox/41.0")]
        [InlineData("Mozilla/5.0 (Tablet; rv:26.0) Gecko/26.0 Firefox/26.0")]
        [InlineData("Mozilla/5.0 (iPad; U; CPU OS 4_3_5 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8L1 Safari/6533.18.5")]
        public void Keyword(string agent)
        {
            // arrange             
            var service = CreateService(agent);            
            // act
            var resolver = new DeviceResolver(service);
            // assert
            Assert.Equal(DeviceType.Tablet, resolver.Device.Type);
        }
    }
}