// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringSubstring
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";
	string? text   = "abcde";

	[Fact]
	public void NormalStart()
	{
		Assert.Equal("abcde", text.SubstringSafe(0));
		Assert.Equal("bcde", text.SubstringSafe(1));
		Assert.Equal("cde", text.SubstringSafe(2));
		Assert.Equal("de", text.SubstringSafe(3));
		Assert.Equal("e", text.SubstringSafe(4));
		Assert.Equal("", text.SubstringSafe(5));
	}

	[Fact]
	public void NormalStartLength()
	{
		Assert.Equal("abcde", text.SubstringSafe(0, 5));
		Assert.Equal("bcde", text.SubstringSafe(1, 4));
		Assert.Equal("cde", text.SubstringSafe(2, 3));
		Assert.Equal("de", text.SubstringSafe(3, 2));
		Assert.Equal("e", text.SubstringSafe(4, 1));
		Assert.Equal("", text.SubstringSafe(5, 0));
	}

	[Fact]
	public void Null()
	{
		Assert.Null(_null.SubstringSafe(0));
	}
}