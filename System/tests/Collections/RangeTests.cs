// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Collections;

public class RangeTests
{
	[Fact]
	public void CustomComparer()
	{
		var subject = new Range<string>("a", "f", StringComparer.Ordinal);
		Assert.False(subject.Contains("B"));
		Assert.True(subject.Contains("b"));

		subject = new Range<string>("a", "f", StringComparer.OrdinalIgnoreCase);
		Assert.True(subject.Contains("B"));
		Assert.True(subject.Contains("A"));
		Assert.True(subject.Contains("F"));
		Assert.False(subject.Contains("G"));
	}

	[Fact]
	public void CustomComparerExcludingEnd()
	{
		var subject = new Range<string>("a", "f", StringComparer.OrdinalIgnoreCase).ExcludeEnd();

		Assert.True(subject.Contains("A"));
		Assert.False(subject.Contains("F"));
	}

	[Fact]
	public void CustomComparerExcludingStart()
	{
		var subject = new Range<string>("a", "f", StringComparer.OrdinalIgnoreCase).ExcludeStart();

		Assert.True(subject.Contains("F"));
		Assert.False(subject.Contains("A"));
	}

	[Fact]
	public void DefaultComparer()
	{
		var subject = new Range<int>(0, 5);

		Assert.True(subject.IncludesStart);
		Assert.True(subject.IncludesEnd);
		Assert.True(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.True(subject.Contains(5));
		Assert.False(subject.Contains(-1));
		Assert.False(subject.Contains(6));
	}

	[Fact]
	public void DefaultComparerExcludingEnd()
	{
		var subject = new Range<int>(0, 5).ExcludeEnd();

		Assert.True(subject.IncludesStart);
		Assert.False(subject.IncludesEnd);
		Assert.True(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.False(subject.Contains(5));
	}

	[Fact]
	public void DefaultComparerExcludingStart()
	{
		var subject = new Range<int>(0, 5).ExcludeStart();

		Assert.False(subject.IncludesStart);
		Assert.True(subject.IncludesEnd);
		Assert.False(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.True(subject.Contains(5));
	}

	[Fact]
	public void DefaultBothEnds()
	{
		var subject = new Range<int>(0, 5).ExcludeStart().ExcludeEnd();

		Assert.False(subject.IncludesStart);
		Assert.False(subject.IncludesEnd);
		Assert.False(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.False(subject.Contains(5));
	}

	[Fact]
	public void ExcludeThenIncludeStart()
	{
		var subject = new Range<int>(0, 5);
		subject = subject.ExcludeStart();
		Assert.False(subject.IncludesStart);
		subject = subject.IncludeStart();
		Assert.True(subject.IncludesStart);
	}

	[Fact]
	public void ExcludeThenIncludeEnd()
	{
		var subject = new Range<int>(0, 5);
		subject = subject.ExcludeEnd();
		Assert.False(subject.IncludesEnd);
		subject = subject.IncludeEnd();
		Assert.True(subject.IncludesEnd);
	}

	[Fact]
	public void IncludeStartOnInclusiveStart()
	{
		var subject = new Range<int>(0, 5);
		Assert.Same(subject, subject.IncludeStart());
	}

	[Fact]
	public void IncludeEndOnInclusiveEnd()
	{
		var subject = new Range<int>(0, 5);
		Assert.Same(subject, subject.IncludeEnd());
	}

	[Fact]
	public void ExcludeStartOnExclusiveStart()
	{
		var subject = new Range<int>(0, 5).ExcludeStart();
		Assert.Same(subject, subject.ExcludeStart());
	}

	[Fact]
	public void ExcludeEndOnExclusiveEnd()
	{
		var subject = new Range<int>(0, 5).ExcludeEnd();
		Assert.Same(subject, subject.ExcludeEnd());
	}

	[Fact]
	public void HalfOpenSamePointIsEmpty()
	{
		var subject = new Range<int>(3, 3).ExcludeEnd();
		Assert.False(subject.Contains(3));

		subject = new Range<int>(3, 3).ExcludeStart();
		Assert.False(subject.Contains(3));
	}

	[Fact]
	public void StartGreaterThanEndThrows()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new Range<int>(5, 0));
	}

	private static readonly int[] even      = { 0, 2, 4 };
	private static readonly int[] odd       = { 5, 3, 1 };
	private static readonly int[] zero_five = { 0, 1, 2, 3, 4, 5 };
	private static readonly int[] five_zero = { 5, 4, 3, 2, 1, 0 };

	[Fact]
	public void Ascending()
	{
		var subject = new Range<int>(0, 5).FromStart(x => x + 2);
		Assert.True(subject.SequenceEqual(even));
	}

	[Fact]
	public void Descending()
	{
		var subject = new Range<int>(0, 5).FromEnd(x => x - 2);
		Assert.True(subject.SequenceEqual(odd));
	}

	[Fact]
	public void StepAscending()
	{
		var subject = new Range<int>(0, 5).Step(x => x + 1);
		Assert.True(subject.SequenceEqual(zero_five));
	}

	[Fact]
	public void StepDescending()
	{
		var subject = new Range<int>(0, 5).Step(x => x - 1);
		Assert.True(subject.SequenceEqual(five_zero));
	}
}