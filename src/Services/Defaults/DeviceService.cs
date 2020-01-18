// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class DeviceService : IDeviceService
    {
        public Device Type { get; }

        private readonly string _useragent;
        private readonly HttpRequest _request;
        private readonly ResponsiveOptions _options;

        public DeviceService(IUserAgentService userAgentService, DetectionOptions options)
        {
            _useragent = userAgentService.UserAgent.ToString().ToLower().ToLowerInvariant();
            _request = userAgentService.Context.Request;
            _options = options.Responsive;

            Type = DeviceFromUserAgent(_useragent, _request, _options);
        }

        private static Device DeviceFromUserAgent(
            string agent,
            HttpRequest request,
            ResponsiveOptions options)
        {
            // fail fast and return default desktop
            if (agent.IsNullOrEmpty())
                return options.DefaultDesktop;
            // tablet user agent keyword detection
            if (TabletCollection.Keywords.Any(keyword => agent.Contains(keyword)))
                return options.DefaultTablet;
            // mobile user agent keyword detection
            if (MobileCollection.Keywords.Any(keyword => agent.Contains(keyword)))
                return options.DefaultMobile;
            // mobile user agent prefix detection
            if (agent?.Length >= 4 && MobileCollection.Prefixes.Any(prefix => agent.StartsWith(prefix)))
                return options.DefaultMobile;
            // mobile opera mini special case
            if (request.IsOperaMini())
                return options.DefaultMobile;
            // mobile user agent prof detection
            if (request.IsUserAgentWAP())
                return options.DefaultMobile;
            // mobile accept-header base detection
            if (request.IsAcceptHeaderWAP())
                return options.DefaultMobile;

            return options.DefaultDesktop;
        }
    }
}
