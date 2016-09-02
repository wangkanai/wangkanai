// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Xunit;

namespace Wangkanai.Browser.Test
{
    public class DeviveInfoTest
    {
        [Fact]
        public void browser_mobile()
        {
            var deviceinfo = new DeviceInfoDepreciated("mobile");
            Assert.Equal("mobile", deviceinfo.Name);
        }

        [Fact]
        public void browser_tablet()
        {
            var deviceinfo = new DeviceInfoDepreciated("tablet");
            Assert.Equal("tablet", deviceinfo.Name);
        }

        [Fact]
        public void broswer_desktop()
        {
            var deviceinfo = new DeviceInfoDepreciated("desktop");
            Assert.Equal("desktop", deviceinfo.Name);
        }
        [Fact]
        public void Device_not_found()
        {            
            Exception ex = Assert.Throws<DeviceNotFoundException>(()=> new DeviceInfoDepreciated("fake"));
            Assert.Equal("Device Not Found\r\nParameter name: name\r\nfake", ex.Message);
        }
    }
}
