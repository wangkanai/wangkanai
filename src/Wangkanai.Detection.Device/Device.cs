// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Device : IDevice
    {
        public DeviceType Type { get; set; }
        public bool Crawler { get; set; }

        public Device() { }
        public Device(DeviceType deviceType)
            => Type = deviceType;
        public Device(DeviceType deviceType, bool isCrawler)
            : this(deviceType)
            => Crawler = isCrawler;

        public Device(string name)
        {
            DeviceType deviceType;

            if (!Enum.TryParse(name, true, out deviceType))
                throw new DeviceNotFoundException(name, "not found");

            Type = deviceType;
        }
    }
}