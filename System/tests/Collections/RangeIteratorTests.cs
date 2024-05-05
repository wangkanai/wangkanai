// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public class RangeIteratorTests
{
	private static readonly int[] ZeroFour = [0, 1, 2, 3, 4];
	private static readonly int[] ZeroFive = [0, 1, 2, 3, 4, 5];
	private static readonly int[] OneFour  = [1, 2, 3, 4];
	private static readonly int[] OneFive  = [1, 2, 3, 4, 5];
	private static readonly int[] FiveOne  = [5, 4, 3, 2, 1];
	private static readonly int[] FiveZero = [5, 4, 3, 2, 1, 0];
	private static readonly int[] FourOne  = [4, 3, 2, 1];
	private static readonly int[] FourZero = [4, 3, 2, 1, 0];

	[Fact]
	public void InclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x + 1);
		Assert.True(subject.SequenceEqual(ZeroFive));
	}

	[Fact]
	public void RangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x + 1);
		Assert.True(subject.SequenceEqual(OneFive));
	}

	[Fact]
	public void RangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(ZeroFour));
	}

	[Fact]
	public void RangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(OneFour));
	}

	[Fact]
	public void DescendingInclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x - 1, false);
		Assert.True(subject.SequenceEqual(FiveZero));
	}

	[Fact]
	public void DescendingRangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(FiveOne));
	}

	[Fact]
	public void DescendingRangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(FourZero));
	}

	[Fact]
	public void DescendingRangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(FourOne));
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
