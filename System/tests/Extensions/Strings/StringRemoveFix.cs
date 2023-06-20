// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using System.Globalization;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringRemoveFix
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";
	string? abcde  = "abcde";

	[Fact]
	public void RemovePreFixTest()
	{
		Assert.Equal("cde", abcde.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixNoMatchTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix("abx"));
	}

	[Fact]
	public void RemovePreFixNoMatchIgnoreCaseTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix("ABx"));
	}

	[Fact]
	public void RemovePreFixIgnoreCaseTest()
	{
		Assert.Equal("cde", abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
}