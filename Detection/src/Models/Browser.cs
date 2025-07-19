// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

[Flags]
public enum Browser
{
	Unknown = 1 << 0,
	Chrome = 1 << 1, // Google Chrome
	InternetExplorer = 1 << 2, // Microsoft Internet Explorer
	Safari = 1 << 3, // Apple Safari
	Firefox = 1 << 4, // Firefox
	Edge = 1 << 5, // Microsoft Edge
	Opera = 1 << 6, // Opera
	GoogleSearchApp = 1 << 7, // Google Search App
	Samsung = 1 << 8, // Samsung Internet Browser
	Others = 1 << 9  // Others
}
