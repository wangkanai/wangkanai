// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions.Strings;

#nullable enable

public class StringSlashTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";

	string slash = "/";
	string start = "start";

	[Fact]
	public void EnsureLeadingHasSlash()
	{
		string expected = "/start";
		string value    = slash + start;
		Assert.Equal(expected, value.EnsureLeadingSlash());
	}

	[Fact]
	public void EnsureLeadingNoSlash()
	{
		string expected = "/start";
		Assert.Equal(expected, start.EnsureLeadingSlash());
	}

	[Fact]
	public void EnsureLeadingWhenNullDoNothing()
	{
		Assert.Null(_null.EnsureLeadingSlash());
		Assert.Empty(_empty.EnsureLeadingSlash());
		Assert.Equal(_space, _space.EnsureLeadingSlash());
	}
}