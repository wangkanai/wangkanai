// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions.Strings;

#nullable enable

public class StringEnumTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";
	
	[Fact]
	public void ValueExistToChar()
	{
		var name  = "sarin";
		var result = name.ToEnum<Names>();
		Assert.Equal(Names.Sarin, result);
	}

	private enum Names
	{
		Sarin,
		Tanya,
		James,
		Wangkanai
	}
}

