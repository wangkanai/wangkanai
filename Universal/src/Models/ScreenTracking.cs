using Wangkanai.Universal.Options;

namespace Wangkanai.Universal.Models
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
            return "ga('send','screenview'," + option + "});";
        }
    }
}
