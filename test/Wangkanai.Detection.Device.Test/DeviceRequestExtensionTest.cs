using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Test;
using Wangkanai.Detection;
using Moq;

namespace Wangkanai.Detection.Test
{
    public class DeviceRequestExtensionTest : DeviceTestAbstract
    {
        [Fact]
        public void CreateNewDeviceFromRequestExtension()
        {
            // arrange
            var context = CreateContext("test");
            // act
            var device = context.Request.Device();
            // assert
            Assert.Equal((new Device()).Type, device.Type);
        }
    }
}
