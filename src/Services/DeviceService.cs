using System;

using Wangkanai.Extensions;
using Wangkanai.Detection.Collections;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUserAgentService _userAgentService;
        
        private Device? _type;
        public Device Type => _type ??= DeviceFromUserAgent();

        public DeviceService(IUserAgentService userAgentService)
        {
            _userAgentService = userAgentService;
        }

        private Device DeviceFromUserAgent()
        {
            var agent = _userAgentService.UserAgent.ToLower();
            
            if (IsTablet(agent))
                return Device.Tablet;
            if (IsTV(agent))
                return Device.Tv;
            if (IsMobile(agent))
                return Device.Mobile;
            if (agent.Contains(Device.Watch))
                return Device.Watch;
            if (agent.Contains(Device.Console))
                return Device.Console;
            if (agent.Contains(Device.Car))
                return Device.Car;
            if (agent.Contains(Device.IoT))
                return Device.IoT;

            return Device.Desktop;
        }

        private static bool IsTablet(string agent)
            => agent.SearchContains(TabletCollection.KeywordsSearchTree);

        private static bool IsMobile(string agent)
            => agent.Length >= 4 && agent.SearchStartsWith(MobileCollection.PrefixesSearchTree) ||
               agent.SearchContains(MobileCollection.KeywordsSearchTree);

        private static bool IsTV(string agent)
            => agent.Contains(Device.Tv) || agent.Contains("bravia", StringComparison.Ordinal);
    }
}