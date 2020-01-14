using System;
using System.Collections.Generic;

namespace Wangkanai.Detection
{
    public class Client
    {
        public Device Device { get; set; }
        public Browser Browser { get; set; }
        //public Platform Platform { get; set; }
        public Engine Engine { get; set; }
        public string? Agent { get; set; }
    }
}
