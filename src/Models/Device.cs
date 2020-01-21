// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    [Flags]
    public enum Device
    {
        Desktop = 0,      // Windows, Mac, Linux
        Tablet  = 1 << 0, // iPad, Android
        Mobile  = 1 << 1, // iPhone, Android
        Tv      = 1 << 2, // Samsung, LG
        Console = 1 << 3, // XBox, Play Station
        Car     = 1 << 4, // Ford, Toyota
        IoT     = 1 << 5  // Raspberry Pi
    }
}
