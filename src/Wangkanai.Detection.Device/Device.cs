// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    public class Device :IDevice
    {
        public DeviceType Type { get; set; }
        public bool IsCrawler { get; set; }

        public Device(){}

        public Device(DeviceType deviceType, bool isCrawler)
        {
            Type = deviceType;
            IsCrawler = isCrawler;
        }

        public Device(string name)
        {        
            DeviceType deviceType;
            if (!Enum.TryParse(name, true, out deviceType))
                throw new DeviceNotFoundException(name, "not found");
            Type = deviceType;            
        }
    }
}