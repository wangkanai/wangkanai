// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Collections;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class DeviceService : IDeviceService
    {
        public Device Type { get; }

        public DeviceService(IUserAgentService userAgentService, DetectionOptions options)
        {
            var useragent         = userAgentService.UserAgent;
            var request           = userAgentService.Context.Request;
            var responsiveOptions = options?.Responsive ?? new ResponsiveOptions();

            Type = DeviceFromUserAgent(useragent, request, responsiveOptions);
        }

        private static Device DeviceFromUserAgent(UserAgent agent, HttpRequest request, ResponsiveOptions options)
        {
            // tablet user agent keyword detection
            if (agent.Contains(TabletCollection.Keywords))
                return options.DefaultTablet;
            // Tv     user agent keywork detection
            if (IsTV(agent))
                return Device.Tv;
            // mobile user agent keyword detection
            if (agent.Contains(MobileCollection.Keywords))
                return options.DefaultMobile;
            // mobile user agent prefix detection
            if (agent.StartsWith(MobileCollection.Prefixes, 4))
                return options.DefaultMobile;
            // mobile opera mini special case
            if (request.IsOperaMini())
                return options.DefaultMobile;
            // mobile user agent prof detection
            if (request.IsUserAgentWap())
                return options.DefaultMobile;
            // mobile accept-header base detection
            if (request.IsAcceptHeaderWap())
                return options.DefaultMobile;
            // console user agent keyword detection
            if (agent.Contains(Device.Console))
                return Device.Console;
            // car    user agent keyword detection
            if (agent.Contains(Device.Car))
                return Device.Car;
            // iot    user agent keyword detection
            if (agent.Contains(Device.IoT))
                return Device.IoT;

            return options.DefaultDesktop;
        }

        private static bool IsTV(UserAgent agent)
            => agent.Contains(Device.Tv) || agent.Contains("BRAVIA");
    }
}
