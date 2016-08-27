// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    internal abstract class DeviceBrowser : IDeviceBrowser
    {
        public virtual DeviceInfo DeviceInfo { get; protected set; }
        public virtual bool IsValid(HttpRequest request)
        {
            DeviceInfo = new DeviceInfo(DeviceTypes.Other);
            return false;
        }
    }
}
