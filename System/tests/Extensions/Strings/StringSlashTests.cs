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
	string end   = "end";

	// Ensure leading slash
	
	[Fact]
	public void EnsureLeadingHasSlash()
	{
		string value = slash + start;
		Assert.Equal("/start", value.EnsureLeadingSlash());
	}

	[Fact]
	public void EnsureLeadingNoSlash()
	{
		Assert.Equal("/start", start.EnsureLeadingSlash());
	}

	[Fact]
	public void EnsureLeadingWhenNullDoNothing()
	{
		Assert.Null(_null.EnsureLeadingSlash());
		Assert.Empty(_empty.EnsureLeadingSlash());
		Assert.Equal(_space, _space.EnsureLeadingSlash());
	}
	
	// Ensure trailing slash
	
	[Fact]
	public void EnsureTrailingHasSlash()
	{
		string value = end + slash;
		Assert.Equal("end/", value.EnsureTrailingSlash());
	}

	[Fact]
	public void EnsureTrailingNoSlash()
	{
		Assert.Equal("end/", end.EnsureTrailingSlash());
	}

	[Fact]
	public void EnsureLTrailingWhenNullDoNothing()
	{
		Assert.Null(_null.EnsureTrailingSlash());
		Assert.Empty(_empty.EnsureTrailingSlash());
		Assert.Equal(_space, _space.EnsureTrailingSlash());
	}
}