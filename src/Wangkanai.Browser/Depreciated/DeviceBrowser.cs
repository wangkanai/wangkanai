// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser.Depreciated
{
    internal abstract class DeviceBrowser : IDeviceBrowser
    {
        public DeviceInfoDepreciated DeviceInfoDepreciated { get; protected set; }
        public virtual bool IsValid(HttpRequest request)
        {
            DeviceInfoDepreciated = new DeviceInfoDepreciated(DeviceTypes.Other);
            return false;
        }
    }
}
