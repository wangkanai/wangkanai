﻿// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai.Checks;

public class CheckEmptyStringTests
{
	private readonly string _null = null!;
	private readonly string _empty = string.Empty;

	[Fact]
	public void BasicNull()
	{
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty("can not be null"));
	}

	[Fact]
	public void BasicEmpty()
	{
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty());
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty("can not be empty"));
	}
}
