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

        public bool IsSet
        {
            get => _userAgentService.Context.GetMark();
            private set => _userAgentService.Context.SetMark(value);
        }
        
        public PreferenceService(IUserAgentService userAgentService)
        {
            _userAgentService = userAgentService;
        }

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