// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Device : IDevice
    {
        public DeviceType Type { get; set; }
        public IVersion Version { get; set; }
        public bool Crawler { get; set; }

        public Device() { }
        public Device(DeviceType deviceType)
            => Type = deviceType;
        public Device(DeviceType deviceType, bool isCrawler)
            : this(deviceType)
            => Crawler = isCrawler;
        public Device(DeviceType deviceType, IVersion version, bool isCrawler)
            : this(deviceType, isCrawler)
            => Version = version;

        public Device(string name)
        {
            DeviceType deviceType;

            if (!Enum.TryParse(name, true, out deviceType))
                throw new DeviceNotFoundException(name, "not found");

            Type = deviceType;
        }
    }
}