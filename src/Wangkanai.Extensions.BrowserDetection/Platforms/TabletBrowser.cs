// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    internal class TabletBrowser : DeviceBrowser
    {
        private readonly string[] _keywords = {
            "tablet", "ipad", "playbook", "hp-tablet", "kindle"
        };

        public override bool IsValid(HttpRequest request)
        {
            var agent = request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();

            if (agent == null) return false;
            if (!_keywords.Any(k => agent.Contains(k))) return false;
            if (!agent.Contains("ipad") && agent.Contains("mobile")) return false;

            return true;
        }
    }
}