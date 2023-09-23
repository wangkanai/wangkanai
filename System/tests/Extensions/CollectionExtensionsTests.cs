// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions;

public class CollectionExtensionsTests
{
	List<string>? nullStrings = null;
	List<int>?    nullInts    = null;

	List<string>? emptyStrings = new();
	List<int>?    emptyInts    = new();

	List<string>? existStrings = new() { "hello" };
	List<int>?    existInts    = new() { int.MinValue, Int32.MinValue };

	[Fact]
	public void IsNull()
	{
		// Null
		Assert.True(nullStrings.IsNull());
		Assert.True(nullInts.IsNull());

		// Empty
		Assert.False(emptyStrings.IsNull());
		Assert.False(emptyInts.IsNull());

		// Exist
		Assert.False(existStrings.IsNull());
		Assert.False(existInts.IsNull());
	}

	[Fact]
	public void IsEmpty()
	{
		// Null
		Assert.True(nullStrings.IsEmpty());
		Assert.True(nullInts.IsEmpty());

		// Empty
		Assert.True(emptyStrings.IsEmpty());
		Assert.True(emptyInts.IsEmpty());

		// Exist
		Assert.False(existStrings.IsEmpty());
		Assert.False(existInts.IsEmpty());
	}

	[Fact]
	public void IsNullOrEmpty()
	{
		// Null
		Assert.True(nullStrings.IsNullOrEmpty());
		Assert.True(nullInts.IsNullOrEmpty());

		// Empty
		Assert.True(emptyStrings.IsNullOrEmpty());
		Assert.True(emptyInts.IsNullOrEmpty());

		// Exist
		Assert.False(existStrings.IsNullOrEmpty());
		Assert.False(existInts.IsNullOrEmpty());
	}

	[Fact]
	public void RangeNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => nullStrings!.AddRangeSafe(null!));
		Assert.Throws<ArgumentNullException>(() => nullInts!.AddRangeSafe(null!));
	}

	[Fact]
	public void RangeEmptyNull()
	{
		Assert.Throws<ArgumentNullException>(() => emptyStrings!.AddRangeSafe(null!));
		Assert.Throws<ArgumentNullException>(() => emptyInts!.AddRangeSafe(null!));
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
		Assert.Throws<ArgumentNullException>(() => nullStrings!.AddDistinct());
		Assert.Throws<ArgumentNullException>(() => nullInts!.AddDistinct());
	}

	[Fact]
	public void DistinctEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => emptyStrings!.AddDistinct());
		Assert.Throws<ArgumentEmptyException>(() => emptyInts!.AddDistinct());
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

	EqualityComparer<string> comparerString = EqualityComparer<string>.Default;
	EqualityComparer<int>    comparerInt    = EqualityComparer<int>.Default;

	[Fact]
	public void DistinctComparerNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => nullStrings!.AddDistinct(comparerString, null!));
		Assert.Throws<ArgumentNullException>(() => nullInts!.AddDistinct(comparerInt, null!));
	}

	[Fact]
	public void DistinctComparerEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => emptyStrings!.AddDistinct(comparerString, null!));
		Assert.Throws<ArgumentEmptyException>(() => emptyInts!.AddDistinct(comparerInt, null!));
	}

	[Fact]
	public void DistinctComparerListString()
	{
		var strings = new List<string> { "hello", "world" };

		Assert.Equal(strings, strings.AddDistinct(comparerString, "hello"));
		Assert.Equal(strings, strings.AddDistinct(comparerString, "hello", "world"));
	}

	[Fact]
	public void DistinctComparerListInt()
	{
		var ints = new List<int> { 1, 2 };

		Assert.Equal(ints, ints.AddDistinct(comparerInt, 1));
		Assert.Equal(ints, ints.AddDistinct(comparerInt, 1, 2));
	}

	[Fact]
	public void ReplaceNullNull()
	{
		Assert.Throws<ArgumentNullException>(() => nullStrings!.Replace(nullStrings));
		Assert.Throws<ArgumentNullException>(() => nullInts!.Replace(nullInts));
	}

	[Fact]
	public void ReplaceEmptyNull()
	{
		Assert.Throws<ArgumentEmptyException>(() => emptyStrings!.Replace(nullStrings));
		Assert.Throws<ArgumentEmptyException>(() => emptyInts!.Replace(nullInts));
	}

	[Fact]
	public void ReplaceNullEmpty()
	{
		Assert.Throws<ArgumentNullException>(() => nullStrings!.Replace(emptyStrings));
		Assert.Throws<ArgumentNullException>(() => nullInts!.Replace(emptyInts));
	}

	[Fact]
	public void ReplaceEmptyEmpty()
	{
		Assert.Throws<ArgumentEmptyException>(() => emptyStrings!.Replace(emptyStrings));
		Assert.Throws<ArgumentEmptyException>(() => emptyInts!.Replace(emptyInts));
	}

	[Fact]
	public void ReplaceListString()
	{
		var strings1 = new List<string> { "hello", "world" };
		var strings2 = new List<string> { "bonjour", "monde" };
		var expected = new List<string> { "bonjour", "monde" };

		Assert.Equal(expected, strings1.Replace(strings2));
	}

	[Fact]
	public void ReplaceListInt()
	{
		var ints1    = new List<int> { 1, 2 };
		var ints2    = new List<int> { 3, 4 };
		var expected = new List<int> { 3, 4 };

		Assert.Equal(expected, ints1.Replace(ints2));
	}
}