// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Xunit;

namespace Wangkanai.Checks;

public class CheckNumericTests
{
	[Fact]
	public void IntegralIsNull()
	{
		byte?  byte1  = null;
		short? short2 = null;
		int?   int4   = null;
		long?  long8  = null;

		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(byte1));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(short2));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(int4));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(long8));
	}

	[Fact]
	public void FloatingIsNull()
	{
		float?   float16   = null;
		double?  double32  = null;
		decimal? decimal32 = null;

		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(float16));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(double32));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(decimal32));
	}

	[Fact]
	public void ThrowIfNegative()
	{
		int postitive = 1;
		int negative  = -1;
		int zero      = 0;

		Assert.Throws<ArgumentNullException>(() => negative.ThrowIfNegative());
		Assert.Equal(0, zero.ThrowIfNegative());
		Assert.Equal(1, postitive.ThrowIfNegative());
	}

	[Fact]
	public void ThrowIfZero()
	{
		int posittive = 1;
		int negative  = -1;
		int zero      = 0;

		Assert.Throws<ArgumentNullException>(() => zero.ThrowIfZero());
		Assert.Equal(1, posittive.ThrowIfZero());
		Assert.Equal(-1, negative.ThrowIfZero());
	}

	[Fact]
	public void ThrowIfPositive()
	{
		int posittive = 1;
		int negative  = -1;
		int zero      = 0;

		Assert.Throws<ArgumentNullException>(() => posittive.ThrowIfPositive());
		Assert.Equal(0, zero.ThrowIfPositive());
		Assert.Equal(-1, negative.ThrowIfPositive());
	}
}