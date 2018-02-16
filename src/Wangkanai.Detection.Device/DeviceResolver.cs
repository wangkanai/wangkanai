// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wangkanai.Detection
{
    public sealed class DeviceResolver : IDeviceResolver
    {
        /// <summary>
        /// Get device result of device result
        /// </summary>
        public IDevice Device => _device;

        /// <summary>
        /// Get user agnet of the request client
        /// </summary>

        public IUserAgent UserAgent => _service.UserAgent;
        /// <summary>
        /// Get HttpContext of the application service
        /// </summary>
        private HttpContext _context => _service.Context;

        private readonly Device _device;
        private readonly IUserAgentService _service;

        public DeviceResolver(IUserAgentService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;

            // testing failed because no default Httpcontext
            //if (_context == null) throw new ArgumentNullException(nameof(_context));
            _device = new Device(GetDeviceType());
        }

        private DeviceType GetDeviceType()
        {
            var agent = GetUserAgent();
            var request = _context.Request;

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

        private string GetUserAgent()
        {
            if (_context == null) return "";
            if (!_context.Request.Headers["User-Agent"].Any()) return "";
            return new UserAgent(_context.Request.Headers["User-Agent"].FirstOrDefault()).ToString().ToLowerInvariant();
        }
    }
}