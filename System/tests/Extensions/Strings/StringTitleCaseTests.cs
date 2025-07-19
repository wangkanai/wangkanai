// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringTitleCaseTests
{
	string? _null = null;
	string _empty = string.Empty;
	string _space = " ";
	string _text = "abcde";

	[Fact] public void Null() => Assert.Throws<ArgumentNullException>(() => _null!.ToTitleCase());
	[Fact] public void Empty() => Assert.Throws<ArgumentEmptyException>(() => _empty.ToTitleCase());
	[Fact] public void Space() => Assert.Equal(_space, _space.ToTitleCase());
	[Fact] public void Text() => Assert.Equal("Abcde", _text.ToTitleCase());
	[Fact] public void TextWithSpace() => Assert.Equal("Abcde fghij", "abcde fghij".ToTitleCase());
	[Fact] public void TextWithSpaceAndNumber() => Assert.Equal("Abcde fghij 123", "abcde fghij 123".ToTitleCase());
	[Fact] public void TextWithSpaceAndSymbol() => Assert.Equal("Abcde fghij 123 !@#$%^&*()", "abcde fghij 123 !@#$%^&*()".ToTitleCase());
	[Fact] public void TextWithSpaceAndSymbolAndUnicode() => Assert.Equal("Abcde fghij 123 !@#$%^&*() กขคง", "abcde fghij 123 !@#$%^&*() กขคง".ToTitleCase());
	[Fact] public void TextWithSpaceAndSymbolAndUnicodeAndNewLine() => Assert.Equal("Abcde fghij 123 !@#$%^&*() กขคง\n", "abcde fghij 123 !@#$%^&*() กขคง\n".ToTitleCase());
}
