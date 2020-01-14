// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    public class DeviceFactory : IDeviceFactory
    {
        public Device Type { get; set; }
        public bool Crawler { get; set; }

        public DeviceFactory()
        {
        }

        public DeviceFactory(Device deviceType)
            => Type = deviceType;

        public DeviceFactory(Device deviceType, bool isCrawler)
            : this(deviceType)
            => Crawler = isCrawler;

        public DeviceFactory(string name)
        {
            Device deviceType;

            if (!Enum.TryParse(name, true, out deviceType))
                throw new DeviceNotFoundException(name, "not found");

            Type = deviceType;
        }
    }
}
