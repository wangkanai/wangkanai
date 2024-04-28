// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringSplitTests
{
	string? _null  = null;
	string  _empty = "";

	[Fact]
	public void SplitBySize()
	{
		var text     = "AAAAABBBBBCCCCC";
		var expected = new[] { "AAAAA", "BBBBB", "CCCCC" };
		var actual   = text.Split(5);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void SplitBySizeWithRemainder()
	{
		var text     = "AAAAABBBBBCCC";
		var expected = new[] { "AAAAA", "BBBBB" };
		var actual   = text.Split(5);
		Assert.Equal(expected, actual);
	}

	[Fact] public void SplitBySizeWithEmpty()      => Assert.Throws<ArgumentEmptyException>(() => _empty.Split(5));
	[Fact] public void SplitBySizeWithNull()       => Assert.Throws<ArgumentNullException>(() => _null!.Split(5));
	[Fact] public void SplitBySizeWithZero()       => Assert.Throws<DivideByZeroException>(() => "AAAAABBBBBCCCCC".Split(0));
	[Fact] public void SplitBySizeWithNegative()   => Assert.Throws<ArgumentOutOfRangeException>(() => "AAAAABBBBBCCCCC".Split(-1));
	[Fact] public void SplitBySizeWithWhiteSpace() => Assert.Throws<ArgumentEmptyException>(() => " ".Split(5));
}
