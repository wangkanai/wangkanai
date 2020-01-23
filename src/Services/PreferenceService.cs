using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PreferenceService : IPreferenceService
    {
        public Device Preferred { get; }

        public PreferenceService()
        {
            Preferred = Device.Desktop;
        }
    }
}
