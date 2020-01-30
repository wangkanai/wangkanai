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
        Watch   = 1 << 2, // Smart Watchs
        Tv      = 1 << 3, // Samsung, LG
        Console = 1 << 4, // XBox, Play Station
        Car     = 1 << 5, // Ford, Toyota
        IoT     = 1 << 6  // Raspberry Pi
    }
}
