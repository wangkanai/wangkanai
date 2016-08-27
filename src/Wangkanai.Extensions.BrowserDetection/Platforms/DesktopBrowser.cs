// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    internal class DesktopBrowser : DeviceBrowser
    {
        public override bool IsValid(HttpRequest request)
        {
            DeviceInfo = new DeviceInfo(DeviceTypes.Desktop);
            return base.IsValid(request);
        }
    }
}