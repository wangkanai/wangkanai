// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringEscapeTests
{
	string? _null     = null;
	string? _empty    = string.Empty;
	string? _space    = " ";
	string? text      = "abcde";
	string? text_plus = "a+b+c+d+e";

	[Fact]
	public void Normal()
	{
		Assert.Equal(text, text.EscapeSearch());
	}

	[Fact]
	public void Null()
	{
		Assert.Throws<ArgumentNullException>(() => _null.EscapeSearch());
	}

	[Fact]
	public void Empty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.EscapeSearch());
	}

	[Fact]
	public void Space()
	{
		Assert.Empty(_space.EscapeSearch());
	}
	
	[Fact]
	public void TextGeneral()
	{
		Assert.Equal(@"a\+b\+c\+d\+e", text_plus.EscapeSearch());
	}
	
	[Fact]
	public void Plus()
	{
		Assert.Equal(@"a\+b", "a+b".EscapeSearch());
	}
	
	[Fact]
	public void Minus()
	{
		Assert.Equal(@"a\-b", "a-b".EscapeSearch());
	}
}