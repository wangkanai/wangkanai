// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.DeviceTest
{
    public class DeviceTabletTest : DeviceTestAbstract
    {
        [Theory]
        [InlineData("Mozilla/5.0 (Android 4.4; Tablet; rv:41.0) Gecko/41.0 Firefox/41.0")]
        [InlineData("Mozilla/5.0 (Tablet; rv:26.0) Gecko/26.0 Firefox/26.0")]
        [InlineData("Mozilla/5.0 (iPad; U; CPU OS 4_3_5 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8L1 Safari/6533.18.5")]
        [InlineData("Mozilla/5.0 (Linux; Android 7.0; SM-T585 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36")]
        [InlineData("Mozilla/5.0 (Linux; Android 4.4.4; SM-T561 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.98 Safari/537.36")]
        [InlineData("Mozilla/5.0 (Linux; Android 5.1.1; KFAUWI) AppleWebKit/537.36 (KHTML, like Gecko) Silk/77.2.19 like Chrome/77.0.3865.92 Safari/537.36")]
        public void Keyword(string agent)
        {
            // arrange
            var service = CreateService(agent);
            // act
            var resolver = new DeviceResolver(service);
            // assert
            Assert.Equal(Device.Tablet, resolver.Device.Type);
        }
    }
}
