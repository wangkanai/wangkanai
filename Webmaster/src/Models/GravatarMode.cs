// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Webmaster.Models;

public enum GravatarMode
{
	[Display(Name = "404")]
	NotFound,

	[Display(Name = "Mp")]
	Mp,

	[Display(Name = "Identicon")]
	Identicon,

	[Display(Name = "Monsterid")]
	Monsterid,

	[Display(Name = "Wavatar")]
	Wavatar,

	[Display(Name = "Retro")]
	Retro,

	[Display(Name = "Blank")]
	Blank,

	[System.ComponentModel.Browsable(false)]
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	Default
}

public static class GravatarModeExtensions
{
	public static string Value(this GravatarMode mode)
		=> mode == GravatarMode.NotFound
			   ? "404"
			   : mode.ToString().ToLower();
}