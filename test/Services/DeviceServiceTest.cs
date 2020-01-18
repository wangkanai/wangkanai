// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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
    }
}
