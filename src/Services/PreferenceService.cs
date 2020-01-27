using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PreferenceService : IPreferenceService
    {
        public Device Preferred { get; private set; }
        public bool   IsSet     { get; private set; }

        public PreferenceService()
            => Clear();

        public void Set(Device preferred)
        {
            IsSet     = true;
            Preferred = preferred;
        }

        public void Clear()
        {
            IsSet     = false;
            Preferred = Device.Desktop;
        }
    }
}