// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Detection.Models;

[Flags]
public enum Crawler
{
	Unknown = 1 << 0,
	Google = 1 << 1,
	Bing = 1 << 2,
	Yahoo = 1 << 3,
	Baidu = 1 << 4,
	Facebook = 1 << 5,
	Twitter = 1 << 6,
	LinkedIn = 1 << 7,
	WhatsApp = 1 << 8,
	Skype = 1 << 9,
	Others = 1 << 10
}
