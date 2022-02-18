using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal
{
    internal class AppTrackingOption : ScreenTrackingOption
    {
        public string appName { get; set; }
        public string appId { get; set; }
        public string appVersion { get; set; }
        public string appInstallerId { get; set; }
    }
}
