// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringEscapeTests
{
	string? _null = null;
	string _empty = string.Empty;
	string _space = " ";
	string _text = "abcde";
	string _plus = "a+b+c+d+e";

	[Fact] public void Normal() => Assert.Equal(_text, _text.EscapeSearch());
	[Fact] public void Null() => Assert.Throws<ArgumentNullException>(() => _null!.EscapeSearch());
	[Fact] public void Empty() => Assert.Throws<ArgumentEmptyException>(() => _empty.EscapeSearch());
	[Fact] public void Space() => Assert.Empty(_space.EscapeSearch());
	[Fact] public void TextGeneral() => Assert.Equal(@"a\+b\+c\+d\+e", _plus.EscapeSearch());
	[Fact] public void Plus() => Assert.Equal(@"a\+b", "a+b".EscapeSearch());
	[Fact] public void Minus() => Assert.Equal(@"a\-b", "a-b".EscapeSearch());
	[Fact] public void Exclamation() => Assert.Equal(@"a\!b", "a!b".EscapeSearch());
	[Fact] public void OpenParenthesis() => Assert.Equal(@"a\(b", "a(b".EscapeSearch());
	[Fact] public void CloseParenthesis() => Assert.Equal(@"a\)b", "a)b".EscapeSearch());
	[Fact] public void OpenBracket() => Assert.Equal(@"a\[b", "a[b".EscapeSearch());
	[Fact] public void CloseBracket() => Assert.Equal(@"a\]b", "a]b".EscapeSearch());
	[Fact] public void OpenBrace() => Assert.Equal(@"a\{b", "a{b".EscapeSearch());
	[Fact] public void CloseBrace() => Assert.Equal(@"a\}b", "a}b".EscapeSearch());
	[Fact] public void Caret() => Assert.Equal(@"a\^b", "a^b".EscapeSearch());
	[Fact] public void Tilde() => Assert.Equal(@"a\~b", "a~b".EscapeSearch());
	[Fact] public void Asterisk() => Assert.Equal(@"a\*b", "a*b".EscapeSearch());
	[Fact] public void Question() => Assert.Equal(@"a\?b", "a?b".EscapeSearch());
	[Fact] public void Colon() => Assert.Equal(@"a\:b", "a:b".EscapeSearch());
	[Fact] public void Backslash() => Assert.Equal(@"a\\b", "a\\b".EscapeSearch());
	[Fact] public void DoubleQuote() => Assert.Equal(@"a\""b", "a\"b".EscapeSearch());
	[Fact] public void Asciitilde() => Assert.Equal(@"a\~b", "a~b".EscapeSearch());
	[Fact] public void DoubleAmpersand() => Assert.Equal(@"a\&&b", "a&&b".EscapeSearch());
	[Fact] public void DoublePipe() => Assert.Equal(@"a\||b", "a||b".EscapeSearch());
}
