// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Runtime.InteropServices;

using Xunit;

namespace Wangkanai.System.Collections;

public class RangeIteratorTests
{
	[Fact]
	public void InclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x + 1);
		Assert.True(subject.SequenceEqual(new[] { 0, 1, 2, 3, 4, 5 }));
	}

	[Fact]
	public void RangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x + 1);
		Assert.True(subject.SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
	}

	[Fact]
	public void RangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(new[] { 0, 1, 2, 3, 4 }));
	}

	[Fact]
	public void RangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x + 1);
		Assert.True(subject.SequenceEqual(new[] { 1, 2, 3, 4 }));
	}

	[Fact]
	public void DescendingInclusiveRange()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5), x => x - 1, false);
		Assert.True(subject.SequenceEqual(new[] { 5, 4, 3, 2, 1, 0 }));
	}
	
	[Fact]
	public void DescendingRangeExcludingStart()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(new[] { 5, 4, 3, 2, 1 }));
	}
	
	[Fact]
	public void DescendingRangeExcludingEnd()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(new[] { 4, 3, 2, 1, 0 }));
	}
	
	[Fact]
	public void DescendingRangeExcludingBoth()
	{
		var subject = new RangeIterator<int>(new Range<int>(0, 5).ExcludeStart().ExcludeEnd(), x => x - 1, false);
		Assert.True(subject.SequenceEqual(new[] { 4, 3, 2, 1 }));
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