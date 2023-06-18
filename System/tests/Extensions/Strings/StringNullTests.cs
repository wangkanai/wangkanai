// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringNullTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";
	string? _test  = "test";

	[Fact]
	public void StringIsNullOrEmpty()
	{
		Assert.True(_null.IsNullOrEmpty());
		Assert.True(_empty.IsNullOrEmpty());
		Assert.False(_space.IsNullOrEmpty());
		Assert.False(_test.IsNullOrEmpty());
	}

	[Fact]
	public void StringIsNotNullOrEmpty()
	{
		Assert.False(_null.IsNotNullOrEmpty());
		Assert.False(_empty.IsNotNullOrEmpty());
		Assert.True(_space.IsNotNullOrEmpty());
		Assert.True(_test.IsNotNullOrEmpty());
	}

	[Fact]
	public void StringIsNullOrWhitespace()
	{
		Assert.True(_null.IsNullOrWhiteSpace());
		Assert.True(_empty.IsNullOrWhiteSpace());
		Assert.True(_space.IsNullOrWhiteSpace());
		Assert.False(_test.IsNullOrWhiteSpace());
	}

	[Fact]
	public void StringIsNotNullOrWhitespace()
	{
		Assert.False(_null.IsNotNullOrWhiteSpace());
		Assert.False(_empty.IsNotNullOrWhiteSpace());
		Assert.False(_space.IsNotNullOrWhiteSpace());
		Assert.True(_test.IsNotNullOrWhiteSpace());
	}

	[Fact]
	public void StringIsExist()
	{
		// same as string is not null or whitespace
		Assert.False(_null.IsExist());
		Assert.False(_empty.IsExist());
		Assert.False(_space.IsExist());
		Assert.True(_test.IsExist());
	}
}