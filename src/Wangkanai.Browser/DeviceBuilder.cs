// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Browser
{
    public class DeviceBuilder
    {
        internal static DeviceInfo Desktop() => new DeviceInfo(DeviceTypes.Desktop);
        internal static DeviceInfo Tablet() => new DeviceInfo(DeviceTypes.Tablet);
        internal static DeviceInfo Mobile() => new DeviceInfo(DeviceTypes.Mobile);
        internal static DeviceInfo Crawler() => new DeviceInfo(DeviceTypes.Crawler);
    }
}