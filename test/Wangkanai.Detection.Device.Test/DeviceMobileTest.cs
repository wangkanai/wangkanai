// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DeviceMobileTest : DeviceTestAbstract
    {
        [Theory]
        [InlineData("Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12F69 Safari/600.1.4")]
        [InlineData("Mozilla/5.0 (Linux; Android 4.4.2); Nexus 5 Build/KOT49H) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Mobile Safari/537.36 OPR/20.0.1396.72047")]
        [InlineData("Mozilla/5.0 (iPod touch; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12F69 Safari/600.1.4")]
        [InlineData("Mozilla/5.0 (Android 4.4; Mobile; rv:41.0) Gecko/41.0 Firefox/41.0")]
        [InlineData("Mozilla/5.0 (Maemo; Linux armv7l; rv:10.0.1) Gecko/20100101 Firefox/10.0.1 Fennec/10.0.1")]
        [InlineData("Mozilla/5.0 (Mobile; rv:26.0) Gecko/26.0 Firefox/26.0")]
        [InlineData("Mozilla/5.0 (Linux; Android 4.0.4; Galaxy Nexus Build/IMM76B) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.133 Mobile Safari/535.19")]
        [InlineData("Mozilla/5.0 (iPhone; U; CPU iPhone OS 5_1_1 like Mac OS X; en) AppleWebKit/534.46.0 (KHTML, like Gecko) CriOS/19.0.1084.60 Mobile/9B206 Safari/7534.48.3")]
        [InlineData("Mozilla/5.0 (iPhone; U; CPU like Mac OS X; en) AppleWebKit/420+ (KHTML, like Gecko) Version/3.0 Mobile/1A543 Safari/419.3")]
        public void Keywords(string agents)
        {
            // arrange
            var service = CreateService(agents);
            // act
            var resolver = new DeviceResolver(service);
            // assert
            Assert.Equal(DeviceType.Mobile, resolver.Device.Type);
        }

        [Theory]
        [InlineData("EricssonT68/R101")]
        [InlineData("Nokia9210/2.0 Symbian-Crystal/6.1 Nokia/2.1")]
        [InlineData("SAMSUNG-SGH-R220/1.0 UP/4.1.19k")]
        [InlineData("SonyEricssonT68/R201A")]
        [InlineData("WinWAP 3.0 PRO")]
        public void Prefix(string agent)
        {
            // Arrange
            var service = CreateService(agent);
            // Act
            var device = new DeviceResolver(service).Device;
            // Assert
            Assert.Equal(DeviceType.Mobile, device.Type);
        }

        [Theory]
        [InlineData("x-wap-profile")]
        [InlineData("Profile")]
        public void UAProf(string agent)
        {
            // Arrange
            var service = CreateService(agent, "<xml><doc></doc>");
            // Act
            var device = new DeviceResolver(service).Device;
            // Assert
            Assert.Equal(DeviceType.Mobile, device.Type);
        }

        [Fact]
        public void Wap()
        {
            // Arrange
            var service = CreateService("Accept", "wap");
            // Act
            var device = new DeviceResolver(service).Device;
            // Assert
            Assert.Equal(DeviceType.Mobile, device.Type);
        }
    }
}
