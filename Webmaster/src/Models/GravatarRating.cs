// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
