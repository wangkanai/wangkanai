// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Webmaster.Models;

public enum GravatarRating
{
	g,
	pg,
	r,
	x
}

public static class GravatarRatingExtensions
{
	public static string Value(this GravatarRating rating)
		=> rating.ToString().ToLower();
}
