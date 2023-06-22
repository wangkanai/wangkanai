// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringAccentTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";

	[Fact]
	public void Normal()
	{
		Assert.Equal("a", "á".RemoveAccent());
	}
	
	[Fact]
	public void Null()
	{
		Assert.Throws<ArgumentNullException>(() => _null!.RemoveAccent());
	}
	
	[Fact]
	public void Empty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty!.RemoveAccent());
	}
	
	[Fact]
	public void Space()
	{
		Assert.Equal(_space, _space.RemoveAccent());
	}
}