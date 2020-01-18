// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class DeviceServiceTest
    {
        [Fact]
        public void UserAgentIsNull()
        {
            // arrange
            var service = MockService.CreateService(null);
            // act
            var device = new DeviceService(service, null);
            // assert
            Assert.NotNull(device);
        }


        #region Tablet
        [Theory]
        [InlineData("Mozilla/5.0 (Android 4.4; Tablet; rv:41.0) Gecko/41.0 Firefox/41.0")]
        [InlineData("Mozilla/5.0 (Tablet; rv:26.0) Gecko/26.0 Firefox/26.0")]
        [InlineData("Mozilla/5.0 (iPad; U; CPU OS 4_3_5 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8L1 Safari/6533.18.5")]
        [InlineData("Mozilla/5.0 (Linux; Android 7.0; SM-T585 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36")]
        [InlineData("Mozilla/5.0 (Linux; Android 4.4.4; SM-T561 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.98 Safari/537.36")]
        [InlineData("Mozilla/5.0 (Linux; Android 5.1.1; KFAUWI) AppleWebKit/537.36 (KHTML, like Gecko) Silk/77.2.19 like Chrome/77.0.3865.92 Safari/537.36")]
        public void Tablet(string agent)
        {
            // arrange
            var service = MockService.CreateService(agent);
            // act
            var resolver = new DeviceService(service, null);
            // assert
            Assert.Equal(Device.Tablet, resolver.Type);
        }
        #endregion

        #region Mobile
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
        public void MobileKeywords(string agent)
        {
            // arrange
            var service = MockService.CreateService(agent);
            // act
            var resolver = new DeviceService(service, null);
            // assert
            Assert.Equal(Device.Mobile, resolver.Type);
        }

        [Theory]
        [InlineData("EricssonT68/R101")]
        [InlineData("Nokia9210/2.0 Symbian-Crystal/6.1 Nokia/2.1")]
        [InlineData("SAMSUNG-SGH-R220/1.0 UP/4.1.19k")]
        [InlineData("SonyEricssonT68/R201A")]
        [InlineData("WinWAP 3.0 PRO")]
        public void MobilePrefix(string agent)
        {
            // Arrange
            var service = MockService.CreateService(agent);
            // Act
            var resolver = new DeviceService(service, null);
            // Assert
            Assert.Equal(Device.Mobile, resolver.Type);
        }

        [Theory]
        [InlineData("x-wap-profile")]
        [InlineData("Profile")]
        public void MobileUAProf(string header)
        {
            // Arrange
            var service = MockService.CreateService("<doc></doc>", header);
            // Act
            var resolver = new DeviceService(service, null);
            // Assert
            Assert.Equal(Device.Mobile, resolver.Type);
        }

        [Fact]
        public void MobileWap()
        {
            // Arrange
            var service = MockService.CreateService("wap", "Accept");
            // Act
            var resolver = new DeviceService(service, null);
            // Assert
            Assert.Equal(Device.Mobile, resolver.Type);
        }
        #endregion

        #region Desktop
        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT x.y; WOW64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36")]
        [InlineData("Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (Macintosh; PPC Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (X11; Linux i686; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData("Mozilla/5.0 (X11; Linux x86_64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        public void Desktop(string agent)
        {
            // arrange
            var service = MockService.CreateService(agent);
            // act
            var resolver = new DeviceService(service, null);
            // assert
            Assert.Equal(Device.Desktop, resolver.Type);
        }
        #endregion
    }
}
