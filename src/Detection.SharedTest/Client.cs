using System;
using System.Collections.Generic;

namespace Wangkanai.Detection
{
    public class Client
    {
        public DeviceType Device { get; set; }
        public BrowserType Browser { get; set; }
        public PlatformType Platform { get; set; }
        public EngineType Engine { get; set; }
        public string Agent { get; set; }
    }
}