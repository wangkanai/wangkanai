// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Wangkanai.Exceptions;

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
	}

	[Fact]
	public void NormalStartLengthOverflow()
	{
		Assert.Equal("abcde", text.SubstringSafe(0, 10));
		Assert.Equal("bcde", text.SubstringSafe(1, 10));
		Assert.Equal("cde", text.SubstringSafe(2, 10));
		Assert.Equal("de", text.SubstringSafe(3, 10));
		Assert.Equal("e", text.SubstringSafe(4, 10));
		Assert.Equal("", text.SubstringSafe(5, 10));
	}

	[Fact]
	public void NullStartZero()
	{
		Assert.Throws<ArgumentNullException>(() => _null.SubstringSafe());
	}

	[Fact]
	public void NullStartOne()
	{
		Assert.Throws<ArgumentNullException>(() => _null.SubstringSafe(1));
	}

	[Fact]
	public void NullStartLength()
	{
		Assert.Throws<ArgumentNullException>(() => _null.SubstringSafe(0, 1));
	}

	[Fact]
	public void EmptyStartZero()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.SubstringSafe());
	}

	[Fact]
	public void EmptyStartOne()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.SubstringSafe(1));
	}

	[Fact]
	public void EmptyStartLength()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.SubstringSafe(0, 1));
	}

	[Fact]
	public void SpaceStartZero()
	{
		Assert.Equal(_space, _space.SubstringSafe());
	}

	[Fact]
	public void SpaceStartOne()
	{
		Assert.Empty(_space.SubstringSafe(1));
	}

	[Fact]
	public void SpaceStartLength()
	{
		Assert.Equal(_space, _space.SubstringSafe(0, 1));
	}

	[Fact]
	public void SpaceStartLengthOverflow()
	{
		Assert.Equal(_space, _space.SubstringSafe(0, 10));
	}

	[Fact]
	public void TextStartZero()
	{
		Assert.Equal(text, text.SubstringSafe());
	}
	
	[Fact]
	public void TextStartOne()
	{
		Assert.Equal("bcde", text.SubstringSafe(1));
	}
	
	[Fact]
	public void TextStartLength()
	{
		Assert.Equal("bcde", text.SubstringSafe(1, 4));
	}
	
	[Fact]
	public void TextStartLengthOverflow()
	{
		Assert.Equal("bcde", text.SubstringSafe(1, 10));
	}

}