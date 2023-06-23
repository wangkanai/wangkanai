// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringSeparationTests
{
	string? _nullString  = null;
	string? _emptyString = string.Empty;
	string? _spaceString = " ";

	List<string> _nullList = null;
	List<string> _emptyList = new();
	List<string> _spaceList = new() { " " };

	[Fact]
	public void ToSpaceNull()
	{
		Assert.Throws<ArgumentNullException>(() => _nullList.SeparateToSpace());
	}
	
	[Fact]
	public void ToSpaceEmpty()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyList.SeparateToSpace());
	}
	
	[Fact]
	public void ToSpaceSpace()
	{
		Assert.Equal(" ", _spaceList.SeparateToSpace());
	}
	
	[Fact]
	public void ToSpaceAlphabet()
	{
		var list = new List<string> { "a", "b", "c" };
		Assert.Equal("a b c", list.SeparateToSpace());
	}
	
	[Fact]
	public void ToSpaceNumber()
	{
		var list = new List<string> { "1", "2", "3" };
		Assert.Equal("1 2 3", list.SeparateToSpace());
	}
	
	[Fact]
	public void ToSpaceMix()
	{
		var list = new List<string> { "a", "1", "b", "2", "c", "3" };
		Assert.Equal("a 1 b 2 c 3", list.SeparateToSpace());
	}

	[Fact]
	public void FromSpaceNull()
	{
		Assert.Throws<ArgumentNullException>(()=> _nullString.SeparateFromSpace());
	}
	
	[Fact]
	public void FromSpaceEmpty()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyString.SeparateFromSpace());
	}
	
	[Fact]
	public void FromSpaceSpace()
	{
		Assert.Equal(new List<string>(), _spaceString.SeparateFromSpace());
	}
	
	[Fact]
	public void FromSpaceAlphabet()
	{
		var list = new List<string> { "a", "b", "c" };
		Assert.Equal(list, "a b c".SeparateFromSpace());
	}
}