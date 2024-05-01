// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

[Flags]
public enum Device
{
	Unknown = 1 << 0,
	Desktop = 1 << 1, // Windows, Mac, Linux
	Tablet  = 1 << 2, // iPad, Android
	Mobile  = 1 << 3, // iPhone, Android
	Watch   = 1 << 4, // Smart Watch
	Tv      = 1 << 5, // Samsung, LG
	Console = 1 << 6, // XBox, Play Station
	Car     = 1 << 7, // Ford, Toyota
	IoT     = 1 << 8  // Raspberry Pi
}
