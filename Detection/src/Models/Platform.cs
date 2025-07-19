// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

[Flags]
public enum Platform
{
	Unknown = 1 << 0,
	Windows = 1 << 1, // Microsoft Windows
	Mac = 1 << 2, // Apple MacOS
	iOS = 1 << 3, // Apple iOS
	iPadOS = 1 << 4, // Apple iPadOS
	Linux = 1 << 5, // Linux Distribution (Red Hat, Mint, Ubuntu)
	Android = 1 << 6, // Google Android
	ChromeOS = 1 << 7, // Google ChromeOS
	Others = 1 << 8  // Others
}
