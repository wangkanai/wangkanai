using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        public Device Preferred { get; }

        public UserPreferenceService()
        {
            Preferred = Device.Desktop;
        }
    }
}
