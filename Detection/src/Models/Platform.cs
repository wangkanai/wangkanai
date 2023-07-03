// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

[Flags]
public enum Platform
{
	Unknown  = 0,
	Windows  = 1 << 0, // Microsoft Windows
	Mac      = 1 << 1, // Apple MacOS
	iOS      = 1 << 2, // Apple iOS
	iPadOS   = 1 << 3, // Apple iPadOS
	Linux    = 1 << 4, // Linux Distribution (Red Hat, Mint, Ubuntu)
	Android  = 1 << 5, // Google Android
	ChromeOS = 1 << 6, // Google ChromeOS
	Others   = 1 << 7  // Others
}