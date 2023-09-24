// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Tabler.Services;

public class ThemeService
{
	public bool   DarkMode { get; private set; } = true;
	public string Theme    => DarkMode ? "dark" : "light";
	public void   Toggle() => DarkMode = !DarkMode;
}