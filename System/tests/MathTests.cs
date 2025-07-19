// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public class MathTests
{
	[Fact] public void ByteDivideWithZero() => Assert.Equal(0, Mathematical.Divider((byte)1, (byte)0));
	[Fact] public void ByteDivideWithOne() => Assert.Equal(1, Mathematical.Divider((byte)1, (byte)1));
	[Fact] public void ByteDivideWithTwo() => Assert.Equal(2, Mathematical.Divider((byte)4, (byte)2));
	[Fact] public void ByteDivideWithThree() => Assert.Equal(3, Mathematical.Divider((byte)9, (byte)3));
	[Fact] public void SByteDivideWithZero() => Assert.Equal(0, Mathematical.Divider((sbyte)1, (sbyte)0));
	[Fact] public void SByteDivideWithOne() => Assert.Equal(1, Mathematical.Divider((sbyte)1, (sbyte)1));
	[Fact] public void SByteDivideWithTwo() => Assert.Equal(2, Mathematical.Divider((sbyte)4, (sbyte)2));
	[Fact] public void SByteDivideWithThree() => Assert.Equal(3, Mathematical.Divider((sbyte)9, (sbyte)3));
	[Fact] public void ShortDivideWithZero() => Assert.Equal(0, Mathematical.Divider((short)1, (short)0));
	[Fact] public void ShortDivideWithOne() => Assert.Equal(1, Mathematical.Divider((short)1, (short)1));
	[Fact] public void ShortDivideWithTwo() => Assert.Equal(2, Mathematical.Divider((short)4, (short)2));
	[Fact] public void ShortDivideWithThree() => Assert.Equal(3, Mathematical.Divider((short)9, (short)3));
	[Fact] public void IntDivideWithZero() => Assert.Equal(0, Mathematical.Divider(1, 0));
	[Fact] public void IntDivideWithOne() => Assert.Equal(1, Mathematical.Divider(1, 1));
	[Fact] public void IntDivideWithTwo() => Assert.Equal(2, Mathematical.Divider(4, 2));
	[Fact] public void IntDivideWithThree() => Assert.Equal(3, Mathematical.Divider(9, 3));
	[Fact] public void UIntDivideWithZero() => Assert.Equal(0U, Mathematical.Divider(1U, 0U));
	[Fact] public void UIntDivideWithOne() => Assert.Equal(1U, Mathematical.Divider(1U, 1U));
	[Fact] public void UIntDivideWithTwo() => Assert.Equal(2U, Mathematical.Divider(4U, 2U));
	[Fact] public void UIntDivideWithThree() => Assert.Equal(3U, Mathematical.Divider(9U, 3U));
	[Fact] public void LongDivideWithZero() => Assert.Equal(0, Mathematical.Divider(1L, 0L));
	[Fact] public void LongDivideWithOne() => Assert.Equal(1, Mathematical.Divider(1L, 1L));
	[Fact] public void LongDivideWithTwo() => Assert.Equal(2, Mathematical.Divider(4L, 2L));
	[Fact] public void LongDivideWithThree() => Assert.Equal(3, Mathematical.Divider(9L, 3L));
	[Fact] public void ULongDivideWithZero() => Assert.Equal(0UL, Mathematical.Divider(1UL, 0UL));
	[Fact] public void ULongDivideWithOne() => Assert.Equal(1UL, Mathematical.Divider(1UL, 1UL));
	[Fact] public void ULongDivideWithTwo() => Assert.Equal(2UL, Mathematical.Divider(4UL, 2UL));
	[Fact] public void ULongDivideWithThree() => Assert.Equal(3UL, Mathematical.Divider(9UL, 3UL));
	[Fact] public void FloatDivideWithLessThanEpsilon() => Assert.Equal(0, Mathematical.Divider(1F, float.Epsilon / 2));
	[Fact] public void FloatDivideWithEpsilon() => Assert.Equal(1, Mathematical.Divider(float.Epsilon, float.Epsilon));
	[Fact] public void FloatDivideWithGreaterThanEpsilon() => Assert.Equal(2, Mathematical.Divider(float.Epsilon * 2, float.Epsilon));
	[Fact] public void FloatDivideWithZero() => Assert.Equal(0, Mathematical.Divider(1F, 0F));
	[Fact] public void FloatDivideWithOne() => Assert.Equal(1, Mathematical.Divider(1F, 1F));
	[Fact] public void FloatDivideWithTwo() => Assert.Equal(2, Mathematical.Divider(4F, 2F));
	[Fact] public void FloatDivideWithThree() => Assert.Equal(3, Mathematical.Divider(9F, 3F));
	[Fact] public void DoubleDivideWithLessThanEpsilon() => Assert.Equal(0, Mathematical.Divider(1D, double.Epsilon / 2));
	[Fact] public void DoubleDivideWithEpsilon() => Assert.Equal(1, Mathematical.Divider(double.Epsilon, double.Epsilon));
	[Fact] public void DoubleDivideWithGreaterThanEpsilon() => Assert.Equal(2, Mathematical.Divider(double.Epsilon * 2, double.Epsilon));
	[Fact] public void DoubleDivideWithZero() => Assert.Equal(0, Mathematical.Divider(1D, 0D));
	[Fact] public void DoubleDivideWithOne() => Assert.Equal(1, Mathematical.Divider(1D, 1D));
	[Fact] public void DoubleDivideWithTwo() => Assert.Equal(2, Mathematical.Divider(4D, 2D));
	[Fact] public void DoubleDivideWithThree() => Assert.Equal(3, Mathematical.Divider(9D, 3D));
	[Fact] public void DecimalDivideWithZero() => Assert.Equal(0, Mathematical.Divider(1M, 0M));
	[Fact] public void DecimalDivideWithOne() => Assert.Equal(1, Mathematical.Divider(1M, 1M));
	[Fact] public void DecimalDivideWithTwo() => Assert.Equal(2, Mathematical.Divider(4M, 2M));
	[Fact] public void DecimalDivideWithThree() => Assert.Equal(3, Mathematical.Divider(9M, 3M));
}
