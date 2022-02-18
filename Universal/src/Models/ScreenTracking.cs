using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal
{
    public class ScreenTracking : Send
    {
        private ScreenTrackingOption option { get; set; }
        public ScreenTracking(string name)
        {
            option = new ScreenTrackingOption();
            option.screenName = name;
        }
        public override string ToString()
        {
            return "ga('send','screenview'," + option.ToString() + "});";
        }
    }
}
