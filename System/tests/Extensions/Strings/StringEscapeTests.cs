// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

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
}