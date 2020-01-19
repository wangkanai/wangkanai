// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Extensions
{
    internal static class DeviceExtensions
    {
        public static bool IsOperaMini(this HttpRequest request)
        {
            return request.Headers.Any(header
                => header.Value.Any(value
                    => value?.Contains("OperaMini") ?? false));
        }

        public static bool IsUserAgentWap(this HttpRequest request)
        {
            return request.Headers.ContainsKey("x-wap-profile")
                   || request.Headers.ContainsKey("Profile");
        }

        public static bool IsAcceptHeaderWap(this HttpRequest request)
        {
            return request.Headers["Accept"].Any(accept => accept.ToLower() == "wap");
        }
    }
}
