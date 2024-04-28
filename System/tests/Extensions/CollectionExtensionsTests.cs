// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions;

public class CollectionExtensionsTests
{
	private readonly List<string>? _nullStrings  = null;
	private readonly List<int>?    _nullInts     = null;
	private readonly List<string>? _emptyStrings = [];
	private readonly List<int>?    _emptyInts    = [];
	private readonly List<string>? _existStrings = ["hello"];
	private readonly List<int>?    _existInts    = [int.MinValue, Int32.MinValue];

	[Fact]
	public void IsNull()
	{
		// Null
		Assert.True(_nullStrings.IsNull());
		Assert.True(_nullInts.IsNull());

		// Empty
		Assert.False(_emptyStrings.IsNull());
		Assert.False(_emptyInts.IsNull());

		// Exist
		Assert.False(_existStrings.IsNull());
		Assert.False(_existInts.IsNull());
	}

	[Fact]
	public void IsEmpty()
	{
		// Null
		Assert.True(_nullStrings!.IsEmpty());
		Assert.True(_nullInts!.IsEmpty());

		// Empty
		Assert.True(_emptyStrings!.IsEmpty());
		Assert.True(_emptyInts!.IsEmpty());

		// Exist
		Assert.False(_existStrings!.IsEmpty());
		Assert.False(_existInts!.IsEmpty());
	}

	[Fact]
	public void IsNullOrEmpty()
	{
		// Null
		Assert.True(_nullStrings!.IsNullOrEmpty());
		Assert.True(_nullInts!.IsNullOrEmpty());

		// Empty
		Assert.True(_emptyStrings!.IsNullOrEmpty());
		Assert.True(_emptyInts!.IsNullOrEmpty());

		// Exist
		Assert.False(_existStrings!.IsNullOrEmpty());
		Assert.False(_existInts!.IsNullOrEmpty());
	}

	[Fact]
	public void RangeNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => _nullStrings!.AddRangeSafe(null!));
		Assert.Throws<ArgumentNullException>(() => _nullInts!.AddRangeSafe(null!));
	}

	[Fact]
	public void RangeEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyStrings!.AddRangeSafe(null!));
		Assert.Throws<ArgumentEmptyException>(() => _emptyInts!.AddRangeSafe(null!));
	}

	[Fact]
	public void RangeListString()
	{
		var strings1 = new List<string> { "hello", "world" };
		var strings2 = new List<string> { "bonjour", "monde" };
		var expected = new List<string> { "hello", "world", "bonjour", "monde" };

		Assert.Equal(expected, strings1.AddRangeSafe(strings2));
	}

	[Fact]
	public void RangeListInt()
	{
		var ints1    = new List<int> { 1, 2 };
		var ints2    = new List<int> { 3, 4 };
		var expected = new List<int> { 1, 2, 3, 4 };

		Assert.Equal(expected, ints1.AddRangeSafe(ints2));
	}

	[Fact]
	public void RangeArrayString()
	{
		var strings1 = new List<string> { "hello", "world" };
		var strings2 = new[] { "bonjour", "monde" };
		var expected = new List<string> { "hello", "world", "bonjour", "monde" };

		Assert.Equal(expected, strings1.AddRangeSafe(strings2));
	}

	[Fact]
	public void RangeArrayInt()
	{
		var ints1    = new List<int> { 1, 2 };
		var ints2    = new[] { 3, 4 };
		var expected = new List<int> { 1, 2, 3, 4 };

		Assert.Equal(expected, ints1.AddRangeSafe(ints2));
	}

	[Fact]
	public void DistinctNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => _nullStrings!.AddDistinct());
		Assert.Throws<ArgumentNullException>(() => _nullInts!.AddDistinct());
	}

	[Fact]
	public void DistinctEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyStrings!.AddDistinct());
		Assert.Throws<ArgumentEmptyException>(() => _emptyInts!.AddDistinct());
	}

	[Fact]
	public void DistinctListString()
	{
		var strings = new List<string> { "hello", "world" };

		Assert.Equal(strings, strings.AddDistinct("hello"));
		Assert.Equal(strings, strings.AddDistinct("hello", "world"));
	}

	[Fact]
	public void DistinctListInt()
	{
		var ints = new List<int> { 1, 2 };

		Assert.Equal(ints, ints.AddDistinct(1));
		Assert.Equal(ints, ints.AddDistinct(1, 2));
	}

	EqualityComparer<string> _comparerString = EqualityComparer<string>.Default;
	EqualityComparer<int>    _comparerInt    = EqualityComparer<int>.Default;

	[Fact]
	public void DistinctComparerNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => _nullStrings!.AddDistinct(_comparerString, null!));
		Assert.Throws<ArgumentNullException>(() => _nullInts!.AddDistinct(_comparerInt, null!));
	}

	[Fact]
	public void DistinctComparerEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyStrings!.AddDistinct(_comparerString, null!));
		Assert.Throws<ArgumentEmptyException>(() => _emptyInts!.AddDistinct(_comparerInt, null!));
	}

	[Fact]
	public void DistinctComparerListString()
	{
		var strings = new List<string> { "hello", "world" };

		Assert.Equal(strings, strings.AddDistinct(_comparerString, "hello"));
		Assert.Equal(strings, strings.AddDistinct(_comparerString, "hello", "world"));
	}

	[Fact]
	public void DistinctComparerListInt()
	{
		var ints = new List<int> { 1, 2 };

		Assert.Equal(ints, ints.AddDistinct(_comparerInt, 1));
		Assert.Equal(ints, ints.AddDistinct(_comparerInt, 1, 2));
	}

	[Fact]
	public void ReplaceNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => _nullStrings!.Replace(_nullStrings!));
		Assert.Throws<ArgumentNullException>(() => _nullInts!.Replace(_nullInts!));
	}

	[Fact]
	public void ReplaceEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyStrings!.Replace(_nullStrings!));
		Assert.Throws<ArgumentEmptyException>(() => _emptyInts!.Replace(_nullInts!));
	}

	[Fact]
	public void ReplaceNullEmpty()
	{
		Assert.Throws<ArgumentNullException>(() => _nullStrings!.Replace(_emptyStrings!));
		Assert.Throws<ArgumentNullException>(() => _nullInts!.Replace(_emptyInts!));
	}

	[Fact]
	public void ReplaceEmptyEmpty()
	{
		Assert.Throws<ArgumentEmptyException>(() => _emptyStrings!.Replace(_emptyStrings!));
		Assert.Throws<ArgumentEmptyException>(() => _emptyInts!.Replace(_emptyInts!));
	}

	[Fact]
	public void ReplaceListString()
	{
		var strings1 = new List<string> { "hello", "world" };
		var strings2 = new List<string> { "bonjour", "monde" };
		var expected = new List<string> { "bonjour", "monde" };
		var replaced = strings1.Replace(strings2);
		Assert.Equal(expected, replaced);
	}

	[Fact]
	public void ReplaceListInt()
	{
		var ints1    = new List<int> { 1, 2 };
		var ints2    = new List<int> { 3, 4 };
		var expected = new List<int> { 3, 4 };
		var replaced = ints1.Replace(ints2);

		Assert.Equal(expected, replaced);
	}
}
