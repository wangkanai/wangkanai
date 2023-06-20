// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions.Strings;

#nullable enable

public class StringEnumTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";

	[Fact]
	public void ValueExistBasic()
	{
		var name = "sarin";
		Assert.Equal(Names.Sarin, name.ToEnum<Names>());
	}

	[Fact]
	public void ValueExistBasicIgnoreCase()
	{
		var name = "SARIN";
		Assert.Equal(Names.Sarin, name.ToEnum<Names>());
	}

	[Fact]
	public void ValueExistCastNotIgnoreMatch()
	{
		var name = "Tanya";
		Assert.Equal(Names.Tanya, name.ToEnum<Names>(false));
	}

	[Fact]
	public void ValueExistCaseNotIgnoreNoMatch()
	{
		var name = "james";
		Assert.Throws<ArgumentException>(() => name.ToEnum<Names>(false));
	}

	[Fact]
	public void ValueExistCaseNotIgnoreMatch()
	{
		var name = "james";
		Assert.Equal(Names.James, name.ToEnum<Names>(true));
	}

	[Fact]
	public void ValueNull()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _null!.ToEnum<Names>());
	}

	[Fact]
	public void ValueEmpty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty!.ToEnum<Names>());
	}

	[Fact]
	public void ValueSpace()
	{
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _space!.ToEnum<Names>());
	}

	[Fact]
	public void ValueNotExist()
	{
		var name = "Wang";
		Assert.Throws<ArgumentException>(() => name.ToEnum<Names>());
	}

	[Fact]
	public void ValueNotExistCastNotIgnoreMatch()
	{
		var name = "Wang";
		Assert.Throws<ArgumentException>(() => name.ToEnum<Names>(false));
	}

	enum Names
	{
		Sarin,
		Tanya,
		James,
		Wangkanai
	}
}