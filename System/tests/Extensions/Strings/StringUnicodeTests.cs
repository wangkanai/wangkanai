// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.Strings;

public class StringUnicodeTests
{
	[Fact] public void Unicode_Mixed()       => Assert.True("sarinสาริน".IsUnicode());
	[Fact] public void Unicode_UnicodeOnly() => Assert.True("สาริน".IsUnicode());
	[Fact] public void Unicode_DigitOnly()   => Assert.False("123".IsUnicode());

	[Fact]
	public void Unicode_AsciiOnly()
	{
		Assert.False("123".IsUnicode());
		Assert.False("abc".IsUnicode());
		Assert.False("ABC".IsUnicode());
	}

	[Fact]
	public void Unicode_AlphabetOnly()
	{
		Assert.False("abc".IsUnicode());
		Assert.False("ABC".IsUnicode());
	}
}
