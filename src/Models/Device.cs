// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

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