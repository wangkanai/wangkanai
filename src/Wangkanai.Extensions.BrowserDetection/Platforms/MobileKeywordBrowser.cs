// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    internal class MobileKeywordBrowser : DeviceBrowser
    {
        private readonly string[] _keywords = {
            "blackberry", "webos", "ipod", "lge vx", "midp", "maemo", "mmp", "mobile",
            "netfront", "hiptop", "nintendo DS", "novarra", "openweb", "opera mobi",
            "opera mini", "palm", "psp", "phone", "smartphone", "symbian", "up.browser",
            "up.link", "wap", "windows ce"
        };

        public override bool IsValid(HttpRequest request)
        {
            var agent = request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();

            // user agent keyword detection
            if (agent == null) return false;
            if (!_keywords.Any(keyword => agent.Contains(keyword))) return false;

            DeviceInfo = DeviceBuilder.Mobile();
            return true;
        }
    }
}