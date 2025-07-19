// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Checks;

public class CheckNumericTests
{
	[Fact]
	public void IntegralIsNull()
	{
		byte? byte1 = null;
		short? short2 = null;
		int? int4 = null;
		long? long8 = null;

		Assert.Throws<ArgumentNullException>(() => byte1.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => short2.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => int4.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => long8.ThrowIfNull());
	}

	[Fact]
	public void FloatingIsNull()
	{
		float? float16 = null;
		double? double32 = null;
		decimal? decimal32 = null;

		Assert.Throws<ArgumentNullException>(() => float16.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => double32.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => decimal32.ThrowIfNull());
	}

	[Fact]
	public void ThrowIfNegative()
	{
		const int positive = 1;
		const int negative = -1;
		const int zero = 0;

		Assert.Equal(0, zero.ThrowIfNegative());
		Assert.Equal(1, positive.ThrowIfNegative());
		Assert.Throws<ArgumentNegativeException>(() => negative.ThrowIfNegative());
		Assert.Throws<ArgumentNegativeException>(() => negative.ThrowIfNegative("message"));
		Assert.Throws<ArgumentNegativeException>(() => negative.ThrowIfNegative<ArgumentNegativeException>());
		Assert.Throws<ArgumentNegativeException>(() => negative.ThrowIfNegative<ArgumentNegativeException>("meesage"));
	}

	[Fact]
	public void ThrowIfZero()
	{
		const int posittive = 1;
		const int negative = -1;
		const int zero = 0;

		Assert.Throws<ArgumentZeroException>(() => zero.ThrowIfZero());
		Assert.Throws<ArgumentZeroException>(() => zero.ThrowIfZero("message"));
		Assert.Equal(1, posittive.ThrowIfZero());
		Assert.Equal(-1, negative.ThrowIfZero());
	}

	[Fact]
	public void ThrowIfPositive()
	{
		const int positive = 1;
		const int negative = -1;
		const int zero = 0;

		Assert.Throws<ArgumentPositiveException>(() => positive.ThrowIfPositive());
		Assert.Throws<ArgumentPositiveException>(() => positive.ThrowIfPositive("message"));
		Assert.Equal(0, zero.ThrowIfPositive());
		Assert.Equal(-1, negative.ThrowIfPositive());
	}
}
