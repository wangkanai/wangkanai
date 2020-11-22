// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Collections;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class DeviceService : IDeviceService
    {
        public Device Type { get; }

        public DeviceService(IUserAgentService userAgentService)
        {
            var useragent = userAgentService.UserAgent;
            var request   = userAgentService.Context?.Request?? new DefaultHttpContext().Request;

            Type = DeviceFromUserAgent(useragent, request);
        }

        private static Device DeviceFromUserAgent(UserAgent agent, HttpRequest request)
        {
            // Tablet
            if (IsTablet(agent))
                return Device.Tablet;
            // Tv
            if (IsTV(agent))
                return Device.Tv;
            // Mobile
            if (IsMobile(agent, request))
                return Device.Mobile;
            // Watch
            if (agent.Contains(Device.Watch))
                return Device.Watch;
            // Console
            if (agent.Contains(Device.Console))
                return Device.Console;
            // Car
            if (agent.Contains(Device.Car))
                return Device.Car;
            // IoT
            if (agent.Contains(Device.IoT))
                return Device.IoT;
            // Desktop
            return Device.Desktop;
        }

        private static bool IsTablet(UserAgent agent)
            => agent.Contains(TabletCollection.Keywords)
               || agent.Contains(TabletCollection.Prefixes);

        private static bool IsMobile(UserAgent agent, HttpRequest request)
            => agent.Contains(MobileCollection.Keywords)
               || agent.StartsWith(MobileCollection.Prefixes, 4)
               || IsOperaMini(request)
               || IsUserAgentWap(request)
               || IsAcceptHeaderWap(request);

        #region Mobile

        private static bool IsOperaMini(HttpRequest request)
            => request.Headers.Any(header => header.Value.Any(IsOperaMini));

        private static bool IsOperaMini(string value)
            => value?.Contains("OperaMini") ?? false;

        private static bool IsUserAgentWap(HttpRequest request)
            => request.Headers.ContainsKey("x-wap-profile")
               || request.Headers.ContainsKey("Profile");

        private static bool IsAcceptHeaderWap(HttpRequest request)
            => request.Headers["Accept"].Any(accept => accept.ToLower() == "wap");

        #endregion

        private static bool IsTV(UserAgent agent)
            => agent.Contains(Device.Tv) || agent.Contains("BRAVIA");
    }
}
