// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringEnumTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";

	[Fact]
	public void ValueExistBasic()
	{
		const string name = "sarin";
		Assert.Equal(Names.Sarin, name.ToEnum<Names>());
	}

	[Fact]
	public void ValueExistBasicIgnoreCase()
	{
		const string name = "SARIN";
		Assert.Equal(Names.Sarin, name.ToEnum<Names>());
	}

	[Fact]
	public void ValueExistCastNotIgnoreMatch()
	{
		const string name = "Tanya";
		Assert.Equal(Names.Tanya, name.ToEnum<Names>(false));
	}

	[Fact]
	public void ValueExistCaseNotIgnoreNoMatch()
	{
		const string name = "james";
		Assert.Throws<ArgumentException>(() => name.ToEnum<Names>(false));
	}

	[Fact]
	public void ValueExistCaseNotIgnoreMatch()
	{
		const string name = "james";
		Assert.Equal(Names.James, name.ToEnum<Names>(true));
	}

	[Fact]
	public void ValueNotExist()
	{
		const string name = "Wang";
		Assert.Throws<ArgumentException>(() => name.ToEnum<Names>());
	}

	[Fact]
	public void ValueNotExistCastNotIgnoreMatch()
	{
		const string name = "Wang";
		Assert.Throws<ArgumentException>(() => name.ToEnum<Names>(false));
	}

	[Fact] public void ValueNull()  => Assert.Throws<ArgumentNullException>(() => _null!.ToEnum<Names>());
	[Fact] public void ValueEmpty() => Assert.Throws<ArgumentEmptyException>(() => _empty!.ToEnum<Names>());
	[Fact] public void ValueSpace() => Assert.Throws<ArgumentNullOrWhitespaceException>(() => _space!.ToEnum<Names>());


	enum Names
	{
		Sarin,
		Tanya,
		James,
		Wangkanai
	}
}
