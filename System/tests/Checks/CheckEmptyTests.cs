// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Checks;

public class CheckEmptyTests
{
	List<int>? _null  = null!;
	List<int>? _empty = new();
	List<int>? _list  = new() { 1, 2, 3 };

	[Fact]
	public void Null()
	{
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty("can not be null"));
	}

	[Fact]
	public void Empty()
	{
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty());
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty("can not be empty"));
	}

	[Fact]
	public void CustomNull()
	{
		Assert.Throws<CustomNullException>(() => _null.ThrowIfEmpty<CustomNullException, int>());
	}

	[Fact]
	public void CustomEmpty()
	{
		Assert.Throws<CustomEmptyException>(() => _empty.ThrowIfEmpty<CustomEmptyException, int>());
	}
}