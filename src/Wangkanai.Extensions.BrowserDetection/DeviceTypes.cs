// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Extensions.BrowserDetection
{
    [Flags]
    public enum DeviceTypes
    {
        Mobile,
        Tablet,
        Desktop,
        Crawler,
        Other
    }
}
