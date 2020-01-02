using System;
using System.Collections.Generic;

namespace Wangkanai.Detection
{
    public class Client
    {
        public Device Device { get; set; }
        public Browser Browser { get; set; }
        //public PlatformType Platform { get; set; }
        public EngineType Engine { get; set; }
        public string? Agent { get; set; }
    }
}
