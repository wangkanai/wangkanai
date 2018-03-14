// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wangkanai.Detection
{
    public sealed class DeviceResolver : BaseResolver, IDeviceResolver
    {
        /// <summary>
        /// Get device result of device result
        /// </summary>
        public IDevice Device => _device;

        private readonly IDevice _device;

        public DeviceResolver(IUserAgentService service) : base(service)
        {
            _device = new Device(GetDeviceType());
        }

        private DeviceType GetDeviceType()
        {
            var agent = GetUserAgent();
            var request = Context.Request;

            // tablet user agent keyword detection
            if (agent != null && TabletCollection.Keywords.Any(keyword => agent.Contains(keyword)))
                return DeviceType.Tablet;
            // mobile user agent keyword detection
            if (agent != null && MobileCollection.Keywords.Any(keyword => agent.Contains(keyword)))
                return DeviceType.Mobile;
            // mobile user agent prefix detection
            if (agent?.Length >= 4 && MobileCollection.Prefixes.Any(prefix => agent.StartsWith(prefix)))
                return DeviceType.Mobile;
            // mobile opera mini special case
            if (request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini"))))
                return DeviceType.Mobile;
            // mobile user agent prof detection
            if (request.Headers.ContainsKey("x-wap-profile") || request.Headers.ContainsKey("Profile"))
                return DeviceType.Mobile;
            // mobile accept-header base detection
            if (request.Headers["Accept"].Any(accept => accept.ToLowerInvariant() == "wap"))
                return DeviceType.Mobile;

            return DeviceType.Desktop;
        }
    }
}