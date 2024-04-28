// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringSelectorTests
{
	string? _null  = null;
	string  _empty = string.Empty;
	string  _space = " ";

	[Fact] public void Null()             => Assert.Throws<ArgumentNullException>(() => _null!.EscapeSelector());
	[Fact] public void Empty()            => Assert.Throws<ArgumentEmptyException>(() => _empty.EscapeSelector());
	[Fact] public void Space()            => Assert.Equal(_space, _space.EscapeSelector());
	[Fact] public void Parenthesis()      => Assert.Equal(@"\\{test\\}", "{test}".EscapeSelector());
	[Fact] public void Slash()            => Assert.Equal(@"test\\/test", "test/test".EscapeSelector());
	[Fact] public void Dot()              => Assert.Equal(@"test\\.test", "test.test".EscapeSelector());
	[Fact] public void Hash()             => Assert.Equal(@"test\\#test", "test#test".EscapeSelector());
	[Fact] public void Comma()            => Assert.Equal(@"test\\,test", "test,test".EscapeSelector());
	[Fact] public void Colon()            => Assert.Equal(@"test\\:test", "test:test".EscapeSelector());
	[Fact] public void Semicolon()        => Assert.Equal(@"test\\;test", "test;test".EscapeSelector());
	[Fact] public void Plus()             => Assert.Equal(@"test\\+test", "test+test".EscapeSelector());
	[Fact] public void Equal()            => Assert.Equal(@"\\=test\\=", "=test=".EscapeSelector());
	[Fact] public void Question()         => Assert.Equal(@"test\\?test", "test?test".EscapeSelector());
	[Fact] public void Ampersand()        => Assert.Equal(@"test\\&test", "test&test".EscapeSelector());
	[Fact] public void Percent()          => Assert.Equal(@"test\\%test", "test%test".EscapeSelector());
	[Fact] public void Tilde()            => Assert.Equal(@"test\\~test", "test~test".EscapeSelector());
	[Fact] public void Quote()            => Assert.Equal(@"test\\'test", "test'test".EscapeSelector());
	[Fact] public void DoubleQuote()      => Assert.Equal("test\\\\\"test", "test\"test".EscapeSelector());
	[Fact] public void LeftBracket()      => Assert.Equal(@"test\\[test", "test[test".EscapeSelector());
	[Fact] public void RightBracket()     => Assert.Equal(@"test\\]test", "test]test".EscapeSelector());
	[Fact] public void LeftParenthesis()  => Assert.Equal(@"test\\(test", "test(test".EscapeSelector());
	[Fact] public void RightParenthesis() => Assert.Equal(@"test\\)test", "test)test".EscapeSelector());

	// [Fact]
	// public void Backslash()
	// {
	// 	Assert.Equal(@"\\test", "\\test".EscapeSelector());
	// }

}
