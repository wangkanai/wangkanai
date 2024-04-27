// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable disable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Checks;

public class CheckEmptyListTests
{
	List<int> _null  = null!;
	List<int> _empty = new();
	List<int> _list  = new() { 1, 2, 3 };

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

	[Fact]
	public void BasicList()
	{
		Assert.Equal(_list, _list.ThrowIfEmpty());
		Assert.Equal(_list, _list.ThrowIfEmpty("can not be empty"));
	}

	[Fact]
	public void GenericNull()
	{
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty<int>());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty<int>("can not be null"));
	}

	[Fact]
	public void GenericEmpty()
	{
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty<int>());
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty<int>("can not be empty"));
	}

	[Fact]
	public void GenericList()
	{
		Assert.Equal(_list, _list.ThrowIfEmpty<int>());
		Assert.Equal(_list, _list.ThrowIfEmpty<int>("can not be empty"));
	}

	[Fact]
	public void CustomNull()
	{
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfEmpty<CustomArgumentException, int>());
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfEmpty<CustomArgumentException, int>("can not be null"));
	}

	[Fact]
	public void CustomEmpty()
	{
		Assert.Throws<CustomEmptyException>(() => _empty.ThrowIfEmpty<CustomEmptyException, int>());
		Assert.Throws<CustomEmptyException>(() => _empty.ThrowIfEmpty<CustomEmptyException, int>("can not be empty"));
	}

	[Fact]
	public void CustomList()
	{
		Assert.Equal(_list, _list.ThrowIfEmpty<CustomEmptyException, int>());
		Assert.Equal(_list, _list.ThrowIfEmpty<CustomEmptyException, int>("can not be empty"));
	}
}
