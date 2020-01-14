// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Linq;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public sealed class DeviceResolver : BaseResolver, IDeviceResolver
    {
        /// <summary>
        /// Get device result of device result
        /// </summary>
        public IDeviceFactory Device { get; }

        public DeviceResolver(IUserAgentService service) : base(service)
        {
            Device = new DeviceFactory(GetDeviceType());
        }

        private Device GetDeviceType()
        {
            var agent = _service.UserAgent.ToString().ToLower().ToLowerInvariant();
            var request = _service.Context.Request;

            // tablet user agent keyword detection
            if (agent != null && TabletCollection.Keywords.Any(keyword => agent.Contains(keyword)))
                return Models.Device.Tablet;
            // mobile user agent keyword detection
            if (agent != null && MobileCollection.Keywords.Any(keyword => agent.Contains(keyword)))
                return Models.Device.Mobile;
            // mobile user agent prefix detection
            if (agent?.Length >= 4 && MobileCollection.Prefixes.Any(prefix => agent.StartsWith(prefix)))
                return Models.Device.Mobile;
            // mobile opera mini special case
            if (request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini"))))
                return Models.Device.Mobile;
            // mobile user agent prof detection
            if (request.Headers.ContainsKey("x-wap-profile") || request.Headers.ContainsKey("Profile"))
                return Models.Device.Mobile;
            // mobile accept-header base detection
            if (request.Headers["Accept"].Any(accept => accept.ToLowerInvariant() == "wap"))
                return Models.Device.Mobile;

            return Models.Device.Desktop;
        }
    }
}
