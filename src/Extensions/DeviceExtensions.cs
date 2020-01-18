// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class DeviceExtensions
    {
        public static bool IsTablet(this UserAgent agent)
            => TabletCollection.Keywords.Any(keyword => agent.Contains(keyword)
);
        public static bool IsMobileKeyword(this UserAgent agent)
            => MobileCollection.Keywords.Any(keyword => agent.Contains(keyword));

        public static bool IsMobilePrefix(this UserAgent agent)
            => agent.Length() >= 4
            && MobileCollection.Prefixes.Any(prefix => agent.StartsWith(prefix));

        public static bool IsOperaMini(this HttpRequest request)
            => request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini")));

        public static bool IsUserAgentWAP(this HttpRequest request)
            => request.Headers.ContainsKey("x-wap-profile")
            || request.Headers.ContainsKey("Profile");

        public static bool IsAcceptHeaderWAP(this HttpRequest request)
            => request.Headers["Accept"].Any(accept => accept.ToLowerInvariant() == "wap");

        private static int Length(this UserAgent agent)
            => agent.ToString().Length;

        private static bool Contains(this UserAgent agent, string keyword)
            => agent.ToString().ToLower().ToLowerInvariant().Contains(keyword);

        private static bool StartsWith(this UserAgent agent, string prefix)
            => agent.ToString().StartsWith(prefix);
    }
}
