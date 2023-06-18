// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai;

public class MathTests
{
	[Fact]
	public void ShortDivideWithZero()
	{
		Assert.Equal(0, Math.Divider((short)1, (short)0));
	}

	[Fact]
	public void ShortDivideWithOne()
	{
		Assert.Equal(1, Math.Divider((short)1, (short)1));
	}

	[Fact]
	public void ShortDivideWithTwo()
	{
		Assert.Equal(2, Math.Divider((short)4, (short)2));
	}

	[Fact]
	public void ShortDivideWithThree()
	{
		Assert.Equal(3, Math.Divider((short)9, (short)3));
	}

	[Fact]
	public void IntDivideWithZero()
	{
		Assert.Equal(0, Math.Divider(1, 0));
	}

	[Fact]
	public void IntDivideWithOne()
	{
		Assert.Equal(1, Math.Divider(1, 1));
	}

	[Fact]
	public void IntDivideWithTwo()
	{
		Assert.Equal(2, Math.Divider(4, 2));
	}

	[Fact]
	public void IntDivideWithThree()
	{
		Assert.Equal(3, Math.Divider(9, 3));
	}

	[Fact]
	public void LongDivideWithZero()
	{
		Assert.Equal(0, Math.Divider(1L, 0L));
	}

	[Fact]
	public void LongDivideWithOne()
	{
		Assert.Equal(1, Math.Divider(1L, 1L));
	}

	[Fact]
	public void LongDivideWithTwo()
	{
		Assert.Equal(2, Math.Divider(4L, 2L));
	}

	[Fact]
	public void LongDivideWithThree()
	{
		Assert.Equal(3, Math.Divider(9L, 3L));
	}

	[Fact]
	public void FloatDivideWithZero()
	{
		Assert.Equal(0, Math.Divider(1F, 0F));
	}

	[Fact]
	public void FloatDivideWithOne()
	{
		Assert.Equal(1, Math.Divider(1F, 1F));
	}

	[Fact]
	public void FloatDivideWithTwo()
	{
		Assert.Equal(2, Math.Divider(4F, 2F));
	}

	[Fact]
	public void FloatDivideWithThree()
	{
		Assert.Equal(3, Math.Divider(9F, 3F));
	}

	[Fact]
	public void DoubleDivideWithZero()
	{
		Assert.Equal(0, Math.Divider(1D, 0D));
	}

	[Fact]
	public void DoubleDivideWithOne()
	{
		Assert.Equal(1, Math.Divider(1D, 1D));
	}

	[Fact]
	public void DoubleDivideWithTwo()
	{
		Assert.Equal(2, Math.Divider(4D, 2D));
	}

	[Fact]
	public void DoubleDivideWithThree()
	{
		Assert.Equal(3, Math.Divider(9D, 3D));
	}

	[Fact]
	public void DecimalDivideWithZero()
	{
		Assert.Equal(0, Math.Divider(1M, 0M));
	}

	[Fact]
	public void DecimalDivideWithOne()
	{
		Assert.Equal(1, Math.Divider(1M, 1M));
	}

	[Fact]
	public void DecimalDivideWithTwo()
	{
		Assert.Equal(2, Math.Divider(4M, 2M));
	}

	[Fact]
	public void DecimalDivideWithThree()
	{
		Assert.Equal(3, Math.Divider(9M, 3M));
	}
}