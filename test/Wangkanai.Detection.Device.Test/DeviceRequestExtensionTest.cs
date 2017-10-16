// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

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
