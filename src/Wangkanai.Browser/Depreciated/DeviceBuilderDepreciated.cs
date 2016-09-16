// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Wangkanai.Browser.Depreciated;

namespace Wangkanai.Browser
{
    //[Obsolete("depreciated", false)]
    public class DeviceBuilderDepreciated
    {
        internal static DeviceInfoDepreciated Desktop() => new DeviceInfoDepreciated(DeviceTypes.Desktop);
        internal static DeviceInfoDepreciated Tablet() => new DeviceInfoDepreciated(DeviceTypes.Tablet);
        internal static DeviceInfoDepreciated Mobile() => new DeviceInfoDepreciated(DeviceTypes.Mobile);
        internal static DeviceInfoDepreciated Crawler() => new DeviceInfoDepreciated(DeviceTypes.Crawler);
    }
}