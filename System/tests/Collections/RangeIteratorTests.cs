// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public class RangeIteratorTests
{
	private static readonly int[] ArrayZeroFour = [0, 1, 2, 3, 4];
	private static readonly int[] ArrayZeroFive = [0, 1, 2, 3, 4, 5];
	private static readonly int[] ArrayOneFour  = [1, 2, 3, 4];
	private static readonly int[] ArrayOneFive  = [1, 2, 3, 4, 5];
	private static readonly int[] ArrayFiveOne  = [5, 4, 3, 2, 1];
	private static readonly int[] ArrayFiveZero = [5, 4, 3, 2, 1, 0];
	private static readonly int[] ArrayFourOne  = [4, 3, 2, 1];
	private static readonly int[] ArrayFourZero = [4, 3, 2, 1, 0];

	[Fact]
	public void InclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x + 1);
		Assert.True(subject.SequenceEqual(ArrayZeroFive));
	}

	[Fact]
	public void RangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x + 1);
		Assert.True(subject.SequenceEqual(ArrayOneFive));
	}

	[Fact]
	public void RangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(ArrayZeroFour));
	}

	[Fact]
	public void RangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(ArrayOneFour));
	}

	[Fact]
	public void DescendingInclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x - 1, false);
		Assert.True(subject.SequenceEqual(ArrayFiveZero));
	}

	[Fact]
	public void DescendingRangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(ArrayFiveOne));
	}

	[Fact]
	public void DescendingRangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(ArrayFourZero));
	}

	[Fact]
	public void DescendingRangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(ArrayFourOne));
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
