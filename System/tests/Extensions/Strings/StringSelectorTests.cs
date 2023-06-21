// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringSelectorTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";

	[Fact]
	public void Null()
	{
		Assert.Throws<ArgumentNullException>(() => _null.EscapeSelector());
	}

	[Fact]
	public void Empty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.EscapeSelector());
	}
	
	[Fact]
	public void Space()
	{
		Assert.Equal(_space, _space.EscapeSelector());
	}

	[Fact]
	public void Parenthesis()
	{
		Assert.Equal(@"\\{test\\}", "{test}".EscapeSelector());
	}
	
	[Fact]
	public void Slash()
	{
		Assert.Equal(@"test\\/test", "test/test".EscapeSelector());
	}
	
	[Fact]
	public void Dot()
	{
		Assert.Equal(@"test\\.test", "test.test".EscapeSelector());
	}
	
	[Fact]
	public void Hash()
	{
		Assert.Equal(@"test\\#test", "test#test".EscapeSelector());
	}
	
	[Fact]
	public void Comma()
	{
		Assert.Equal(@"test\\,test", "test,test".EscapeSelector());
	}
}