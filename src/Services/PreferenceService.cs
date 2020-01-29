using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IUserAgentService _userAgentService;
        public Device Preferred
        {
            get => _userAgentService.Context.GetPreference();
            private set => _userAgentService.Context.SetPreference(value);
        }

        public PreferenceService(IUserAgentService userAgentService)
        {
            _userAgentService = userAgentService;
        }

        public void Set(Device preferred) 
            => Preferred = preferred;

        public void Clear() 
            => Preferred = Device.Desktop;
    }
}