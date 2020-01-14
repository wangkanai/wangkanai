// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DeviceTest
    {
        [Fact]
        public void cast_valid_type()
        {
            DeviceFactory device = null;
            try
            {
                device = new DeviceFactory("mobile");
            }
            catch (DeviceNotFoundException)
            {
            }
            Assert.Equal(Device.Mobile, device.Type);
        }

        [Fact]
        public void cast_something_not_valid()
        {
            Type actual = null;
            DeviceFactory device = null;
            try
            {
                device = new DeviceFactory("xxx");
            }
            catch (DeviceNotFoundException e)
            {
                actual = e.GetType();
            }
            Assert.Equal(typeof(DeviceNotFoundException), actual);

            Assert.Throws<DeviceNotFoundException>(() =>
            {
                new DeviceFactory("xxx");
            });
        }
    }
}
