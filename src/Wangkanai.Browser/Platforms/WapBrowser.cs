// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser.Platforms
{
    internal class WapBrowser : DeviceBrowser
    {
        public override bool IsValid(HttpRequest request)
        {
            // accept-header base detection
            if (request.Headers["Accept"].All(accept => accept.ToLowerInvariant() != "wap")) return false;

            DeviceInfo = DeviceBuilder.Mobile();
            return true;
        }
    }
}