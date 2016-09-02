// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser.Depreciated
{
    internal class DesktopBrowser : DeviceBrowser
    {
        public override bool IsValid(HttpRequest request)
        {
            DeviceInfoDepreciated = new DeviceInfoDepreciated(DeviceTypes.Desktop);
            return base.IsValid(request);
        }
    }
}