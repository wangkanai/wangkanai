// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    internal class CrawlerBrowser : DeviceBrowser
    {
        private readonly string[] _keywords = {
            "bot","slurp", "spider"
        };

        public override bool IsValid(HttpRequest request)
        {
            var agent = request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();

            if (agent == null) return false;
            if (!_keywords.Any(keyword => agent.Contains(keyword))) return false;

            DeviceInfo = DeviceBuilder.Crawler();
            return true;            
        }
    }
}