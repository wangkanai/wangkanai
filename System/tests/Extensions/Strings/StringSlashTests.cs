// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringSlashTests
{
	string? _null  = null;
	string  _empty = string.Empty;
	string  _space = " ";

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
	public void EnsureLeadingDoNothingWhenNull()
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
	public void EnsureTrailingDoNothingWhenNull()
	{
		Assert.Null(_null.EnsureTrailingSlash());
		Assert.Empty(_empty.EnsureTrailingSlash());
		Assert.Equal(_space, _space.EnsureTrailingSlash());
	}

	// Remove leading slash

	[Fact]
	public void RemoveLeadingHasSlash()
	{
		var value = slash + start;
		Assert.Equal(start, value.RemoveLeadingSlash());
	}

	[Fact]
	public void RemoveLeadingNoSlash()
	{
		Assert.Equal(start, start.RemoveLeadingSlash());
	}

	[Fact]
	public void RemoveLeadingDoNothingWhenNull()
	{
		Assert.Null(_null.RemoveLeadingSlash());
		Assert.Empty(_empty.RemoveLeadingSlash());
		Assert.Equal(_space, _space.RemoveLeadingSlash());
	}

	// Remove trailing slash

	[Fact]
	public void RemoveTrailingHasSlash()
	{
		var value = end + slash;
		Assert.Equal(end, value.RemoveTrailingSlash());
	}

	[Fact]
	public void RemoveTrailingNoSlash()
	{
		Assert.Equal(end, end.RemoveTrailingSlash());
	}

	[Fact]
	public void RemoveTrailingDoNothingWhenNull()
	{
		Assert.Null(_null.RemoveTrailingSlash());
		Assert.Empty(_empty.RemoveTrailingSlash());
		Assert.Equal(_space, _space.RemoveTrailingSlash());
	}
}
