// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    [Flags]
    public enum Device
    {
        Unknown = 0,
        Desktop = 1 << 0, // Windows, Mac, Linux
        Tablet  = 1 << 1, // iPad, Android
        Mobile  = 1 << 2, // iPhone, Android
        Watch   = 1 << 3, // Smart Watchs
        Tv      = 1 << 4, // Samsung, LG
        Console = 1 << 5, // XBox, Play Station
        Car     = 1 << 6, // Ford, Toyota
        IoT     = 1 << 7  // Raspberry Pi
    }
}