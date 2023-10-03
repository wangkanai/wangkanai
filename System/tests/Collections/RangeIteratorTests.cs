// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Collections;

public class RangeIteratorTests
{
	private static readonly int[] zero_four = { 0, 1, 2, 3, 4 };
	private static readonly int[] zero_five = { 0, 1, 2, 3, 4, 5 };
	private static readonly int[] one_four  = { 1, 2, 3, 4 };
	private static readonly int[] one_five  = { 1, 2, 3, 4, 5 };
	private static readonly int[] five_one  = { 5, 4, 3, 2, 1 };
	private static readonly int[] five_zero = { 5, 4, 3, 2, 1, 0 };
	private static readonly int[] four_one  = { 4, 3, 2, 1 };
	private static readonly int[] four_zero = { 4, 3, 2, 1, 0 };

	[Fact]
	public void InclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x + 1);
		Assert.True(subject.SequenceEqual(zero_five));
	}

	[Fact]
	public void RangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x + 1);
		Assert.True(subject.SequenceEqual(one_five));
	}

	[Fact]
	public void RangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(zero_four));
	}

	[Fact]
	public void RangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(one_four));
	}

	[Fact]
	public void DescendingInclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x - 1, false);
		Assert.True(subject.SequenceEqual(five_zero));
	}

	[Fact]
	public void DescendingRangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(five_one));
	}

	[Fact]
	public void DescendingRangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(four_zero));
	}

	[Fact]
	public void DescendingRangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(four_one));
	}

	[Fact]
	public void StepWrongWayThrows()
	{
		Assert.Throws<ArgumentException>(() => new RangeIterator<int>(new Range<int>(0, 5), x => x - 1));
	}

	[Fact]
	public void NoOpStepThrows()
	{
		Assert.Throws<ArgumentException>(() => new RangeIterator<int>(new Range<int>(0, 5), x => x));
	}

	[Fact]
	public void NullStepThrows()
	{
		Assert.Throws<ArgumentNullException>(() => new RangeIterator<int>(new Range<int>(0, 5), null));
	}

	[Fact]
	public void DescendingStepWrongWayThrows()
	{
		Assert.Throws<ArgumentException>(() => new RangeIterator<int>(new Range<int>(0, 5), x => x + 1, false));
	}
}