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

	[Fact]
	public void Paginate()
	{
		var items = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

		var pages = items.Paginate(3).ToList();

		Assert.Equal(4, pages.Count);
		Assert.Equal(new[] { 1, 2, 3 }, pages[0]);
		Assert.Equal(new[] { 4, 5, 6 }, pages[1]);
		Assert.Equal(new[] { 7, 8, 9 }, pages[2]);
		Assert.Equal(new[] { 10 }, pages[3]);
	}

	[Fact]
	public void Apply()
	{
		var items = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		var sum   = 0;
		items.Apply(i => sum += i);
		Assert.Equal(55, sum);
	}

	[Fact]
	public void Apply_Null()
	{
		var items = new int[] { };
		var sum   = 0;
		items.Apply(i => sum += i);
		Assert.Equal(0, sum);
	}

	[Fact]
	public void Apply_List()
	{
		var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		var sum   = 0;
		items.Apply(i => sum += i);
		Assert.Equal(55, sum);
	}

	[Fact]
	public void Apply_List_Null()
	{
		var items = new List<int> { };
		var sum   = 0;
		items.Apply(i => sum += i);
		Assert.Equal(0, sum);
	}

	[Fact]
	public void Apply_Null_List()
	{
		var items = new List<int> { };
		var sum   = 0;
		items.Apply(i => sum += i);
		Assert.Equal(0, sum);
	}

	[Fact]
	public void Apply_Dictionary()
	{
		var items = new Dictionary<string, string> { { "1", "1" }, { "2", "2" }, { "3", "3" } };
		var sum   = 0;
		items.Apply((key, value) => sum += int.Parse(key) + int.Parse(value));
		Assert.Equal(12, sum);
	}

	[Fact]
	public void Apply_Dictionary_Null()
	{
		Dictionary<string, string> items = null!;
		var                        sum   = 0;
		items.Apply((k, v) => sum += int.Parse(k) + int.Parse(v));
		Assert.Equal(0, sum);
	}

	[Fact]
	public void Apply_Dictionary_Empty()
	{
		var items = new Dictionary<string, string>();
		var sum   = 0;
		items.Apply((k, v) => sum += int.Parse(k) + int.Parse(v));
		Assert.Equal(0, sum);
	}

	[Fact]
	public void ToDictionary_Int()
	{
		var items      = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		var dictionary = items.ToIDictionary(i => i);
		Assert.Equal(10, dictionary.Count);
	}

	[Fact]
	public void ToDictionary_String()
	{
		var items      = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
		var dictionary = items.ToIDictionary(i => i);
		Assert.Equal(10, dictionary.Count);
	}

	[Fact]
	public void ToDictionary_Null()
	{
		string[] items      = null!;
		Assert.Throws<ArgumentNullException>(() => items.ToIDictionary(i => i));
	}

	[Fact]
	public void ToDictionary_Empty()
	{
		var items      = new string[] { };
		var dictionary = items.ToIDictionary(i => i);
		Assert.Empty(dictionary);
	}

	[Fact]
	public void ToIDictionary_EqualityComparer_Ints()
	{
		var items      = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		var dictionary = items.ToIDictionary(i => i, EqualityComparer<int>.Default);
		Assert.Equal(10, dictionary.Count);
	}

	[Fact]
	public void ToIDictionary_EqualityComparer_Strings()
	{
		var items      = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
		var dictionary = items.ToIDictionary(i => i, EqualityComparer<string>.Default);
		Assert.Equal(10, dictionary.Count);
	}

	[Fact]
	public void ToIDictionary_EqualityComparer_Null()
	{
		string[] items      = null!;
		Assert.Throws<ArgumentNullException>(() => items.ToIDictionary(i => i, EqualityComparer<string>.Default));
	}

	[Fact]
	public void ToIDictionary_EqualityComparer_Empty()
	{
		var items      = new string[] { };
		var dictionary = items.ToIDictionary(i => i, EqualityComparer<string>.Default);
		Assert.Empty(dictionary);
	}

	[Fact]
	public void ToIDictionary_EqualityComparer_NullComparer()
	{
		var items      = new string[] { };
		var dictionary = items.ToIDictionary(i => i, null!);
		Assert.Empty(dictionary);
	}
}
