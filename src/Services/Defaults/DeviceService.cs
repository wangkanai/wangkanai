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

        private readonly UserAgent _useragent;
        private readonly HttpRequest _request;
        private readonly ResponsiveOptions _options;

        public DeviceService(IUserAgentService userAgentService, DetectionOptions options)
        {
            _useragent = userAgentService.UserAgent;
            _request = userAgentService.Context.Request;
            _options = options?.Responsive ?? new ResponsiveOptions();

            Type = DeviceFromUserAgent(_useragent, _request, _options);
        }

        private static Device DeviceFromUserAgent(
            UserAgent agent,
            HttpRequest request,
            ResponsiveOptions options)
        {
            // tablet user agent keyword detection
            if (agent.Contains(TabletCollection.Keywords))
                return options.DefaultTablet;
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
            if (request.IsUserAgentWAP())
                return options.DefaultMobile;
            // mobile accept-header base detection
            if (request.IsAcceptHeaderWAP())
                return options.DefaultMobile;

            return options.DefaultDesktop;
        }
    }
}
