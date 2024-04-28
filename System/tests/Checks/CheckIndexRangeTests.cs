// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Checks;

public class CheckIndexRangeTests
{
	[Fact]
	public void IndexIsInRange()
	{
		Assert.Equal(0, 0.ThrowIfOutOfRange(0, 10));
		Assert.Equal(10, 10.ThrowIfOutOfRange(5, 11));
		Assert.Equal(10, 10.ThrowIfOutOfRange(0, 15));
	}

	[Fact]
	public void IndexIsOutOfRange()
	{
		// The index must be 1 less than the upper bound.
		Assert.Throws<ArgumentOutOfRangeException>(() => 10.ThrowIfOutOfRange(0, 10));
		Assert.Throws<ArgumentOutOfRangeException>(() => 10.ThrowIfOutOfRange(0, 9));
		Assert.Throws<ArgumentOutOfRangeException>(() => 10.ThrowIfOutOfRange(11, 15));
	}

	[Fact]
	public void IndexConditionRangeIsNegative()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => 10.ThrowIfOutOfRange(-1, 2));
		Assert.Throws<ArgumentOutOfRangeException>(() => 10.ThrowIfOutOfRange(0, -2));
		Assert.Throws<ArgumentOutOfRangeException>(() => 10.ThrowIfOutOfRange(-1, -2));
	}
}
