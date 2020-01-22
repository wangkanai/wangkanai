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
            var request   = userAgentService.Context.Request;

            Type = DeviceFromUserAgent(useragent, request);
        }

        private static Device DeviceFromUserAgent(UserAgent agent, HttpRequest request)
        {
            // Tablet
            if (agent.Contains(TabletCollection.Keywords))
                return Device.Tablet;
            // Tv
            if (IsTV(agent))
                return Device.Tv;
            // Mobile
            if (IsMobile(agent, request))
                return Device.Mobile;
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


        private static bool IsMobile(UserAgent agent, HttpRequest request)
            => agent.Contains(MobileCollection.Keywords)
               || agent.StartsWith(MobileCollection.Prefixes, 4)
               || IsOperaMini(request)
               || IsUserAgentWap(request)
               || IsAcceptHeaderWap(request);

        #region Mobile

        public static bool IsOperaMini(HttpRequest request)
            => request.Headers.Any(header
                => header.Value.Any(value => value?.Contains("OperaMini") ?? false));

        public static bool IsUserAgentWap(HttpRequest request)
            => request.Headers.ContainsKey("x-wap-profile")
               || request.Headers.ContainsKey("Profile");

        public static bool IsAcceptHeaderWap(HttpRequest request)
            => request.Headers["Accept"].Any(accept => accept.ToLower() == "wap");

        #endregion

        private static bool IsTV(UserAgent agent)
            => agent.Contains(Device.Tv) || agent.Contains("BRAVIA");
    }
}
