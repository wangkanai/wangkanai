// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wangkanai.Detection
{
    public sealed class DeviceResolver : IDeviceResolver
    {
        public IDevice Device => _device;
        public IUserAgent UserAgent => _service.UserAgent;
        private HttpContext _context => _service.Context;
        private readonly Device _device;
        private readonly IDetectionService _service;
        public DeviceResolver(IDetectionService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;

            // testing failed because no default Httpcontext
            //if (_context == null) throw new ArgumentNullException(nameof(_context));
            _device = new Device(GetDeviceType(), GetCrawler());
        }

        private DeviceType GetDeviceType()
        {
            var agent = GetUserAgent();
            var request = _context.Request;

            // tablet user agent keyword detection       
            if (agent != null && _tabletKeywords.Any(keyword => agent.Contains(keyword)))
                return DeviceType.Tablet;
            // mobile user agent keyword detection
            if (agent != null && _mobileKeywords.Any(keyword => agent.Contains(keyword)))
                return DeviceType.Mobile;
            // mobile user agent prefix detection
            if (agent?.Length >= 4 && _mobilePrefixes.Any(prefix => agent.StartsWith(prefix)))
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
        private bool GetCrawler()
        {
            var agent = GetUserAgent();
            if (agent == null) return false;
            if (_crawlerKeywords.Any(keyword => agent.Contains(keyword))) return true;
            return false;
        }
        private string GetUserAgent()
        {
            if (_context == null) return "";
            if (!_context.Request.Headers["User-Agent"].Any()) return "";
            return new UserAgent(_context.Request.Headers["User-Agent"].FirstOrDefault()).ToString();
        }

        #region yaml    
        private readonly string[] _tabletKeywords = {
            "tablet",
            "ipad",
            "playbook",
            "hp-tablet",
            "kindle",
            "sm-t"
        };

        private readonly string[] _crawlerKeywords = {
            "bot",
            "slurp",
            "spider"
        };

        private readonly string[] _mobileKeywords = {
            "blackberry",
            "webos",
            "ipod",
            "lge vx",
            "midp",
            "maemo",
            "mmp",
            "mobile",
            "netfront",
            "hiptop",
            "nintendo DS",
            "novarra",
            "openweb",
            "opera mobi",
            "opera mini",
            "palm",
            "psp",
            "phone",
            "smartphone",
            "symbian",
            "up.browser",
            "up.link",
            "wap",
            "windows ce"
        };

        // reference 4 chare from http://www.webcab.de/wapua.htm
        private readonly string[] _mobilePrefixes = {
            "w3c ",
            "w3c-",
            "acs-",
            "alav",
            "alca",
            "amoi",
            "audi",
            "avan",
            "benq",
            "bird",
            "blac",
            "blaz",
            "brew",
            "cell",
            "cldc",
            "cmd-",
            "dang",
            "doco",
            "eric",
            "hipt",
            "htc_",
            "inno",
            "ipaq",
            "ipod",
            "jigs",
            "kddi",
            "keji",
            "leno",
            "lg-c",
            "lg-d",
            "lg-g",
            "lge-",
            "lg/u",
            "maui",
            "maxo",
            "midp",
            "mits",
            "mmef",
            "mobi",
            "mot-",
            "moto",
            "mwbp",
            "nec-",
            "newt",
            "noki",
            "palm",
            "pana",
            "pant",
            "phil",
            "play",
            "port",
            "prox",
            "qwap",
            "sage",
            "sams",
            "sany",
            "sch-",
            "sec-",
            "send",
            "seri",
            "sgh-",
            "shar",
            "sie-",
            "siem",
            "smal",
            "smar",
            "sony",
            "sph-",
            "symb",
            "t-mo",
            "teli",
            "tim-",
            "tosh",
            "tsm-",
            "upg1",
            "upsi",
            "vk-v",
            "voda",
            "wap-",
            "wapa",
            "wapi",
            "wapp",
            "wapr",
            "webc",
            "winw",
            "winw",
            "xda ",
            "xda-"
        };

        #endregion
    }
}