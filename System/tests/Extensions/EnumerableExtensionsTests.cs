// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class EnumerableExtensionsTests
{
	private readonly IEnumerable<string> _nullStrings  = null!;
	private readonly IEnumerable<int>    _nullInts     = null!;
	private readonly IEnumerable<string> _emptyStrings = Enumerable.Empty<string>();
	private readonly IEnumerable<int>    _emptyInts    = Enumerable.Empty<int>();
	private readonly IEnumerable<string> _existString  = new[] { "hello" };
	private readonly IEnumerable<int>    _existInt     = new[] { 0, int.MinValue };
	private readonly IEnumerable<string> _existStrings = new[] { "hello", "world" };
	private readonly IEnumerable<int>    _existInts    = new[] { int.MinValue, int.MinValue };

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
		Assert.False(_existString.IsNull());
		Assert.False(_existInt.IsNull());

		// Exist Multiple
		Assert.False(_existStrings.IsNull());
		Assert.False(_existInts.IsNull());
	}

	[Fact]
	public void IsEmpty()
	{
		// Null
		Assert.True(_nullStrings.IsEmpty());
		Assert.True(_nullInts.IsEmpty());

		// Empty
		Assert.True(_emptyStrings.IsEmpty());
		Assert.True(_emptyInts.IsEmpty());

		// Exist
		Assert.False(_existString.IsEmpty());
		Assert.False(_existInt.IsEmpty());

		// Exist Multiple
		Assert.False(_existStrings.IsNull());
		Assert.False(_existInts.IsNull());
	}

	[Fact]
	public void IsNullOrEmpty()
	{
		// Null
		Assert.True(_nullStrings.IsNullOrEmpty());
		Assert.True(_nullInts.IsNullOrEmpty());

		// Empty
		Assert.True(_emptyStrings.IsNullOrEmpty());
		Assert.True(_emptyInts.IsNullOrEmpty());

		// Exist
		Assert.False(_existString.IsNullOrEmpty());
		Assert.False(_existInt.IsNullOrEmpty());

		// Exist Multiple
		Assert.False(_existStrings.IsNull());
		Assert.False(_existInts.IsNull());
	}

	[Fact]
	public void HasDuplicates_IntsUnique_ReturnFalse()
	{
		var ints = new[] { 1, 2, 3, 4 };

		Assert.False(ints.HasDuplicates(i => i));
	}

	[Fact]
	public void HasDuplicates_IntsDuplicate_ReturnTrue()
	{
		var ints = new[] { 1, 2, 3, 1 };

		Assert.True(ints.HasDuplicates(i => i));
	}

	[Fact]
	public void HasDuplicates_StringsUnique_ReturnFalse()
	{
		var strings = new[] { "a", "b", "c", "d" };

		Assert.False(strings.HasDuplicates(i => i));
	}

	[Fact]
	public void HasDuplicates_StringsDuplicate_ReturnTrue()
	{
		var strings = new[] { "a", "b", "c", "a" };

		Assert.True(strings.HasDuplicates(i => i));
	}
}
