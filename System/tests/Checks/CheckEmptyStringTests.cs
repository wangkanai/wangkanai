// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Checks;

public class CheckEmptyStringTests
{
	string _null  = null!;
	string _empty = string.Empty;
	
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