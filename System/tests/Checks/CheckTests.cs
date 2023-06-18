// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;


#nullable enable

namespace Wangkanai.Checks;

public class CheckTests
{


	#region ThrowIfNullOrEmpty

	[Fact]
	public void StringThrowIfNull()
	{
		char?   _char   = null;
		string? _string = null;

		Assert.Throws<ArgumentNullException>(() => _char.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _string.ThrowIfNull());

		Assert.Throws<CustomNullException>(() => _char.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _string.ThrowIfNull<CustomNullException>());
	}

	[Fact]
	public void StringIsNullOrEmptyThrowNullException()
	{
		string? _null  = null;
		string? _empty = string.Empty;

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNullOrEmpty());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNullOrEmpty<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNullOrEmpty<CustomNullException>());

		Assert.Throws<ArgumentNullException>(() => _empty.ThrowIfNullOrEmpty());
		Assert.Throws<ArgumentNullException>(() => _empty.ThrowIfNullOrEmpty<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _empty.ThrowIfNullOrEmpty<CustomNullException>());
	}

	[Fact]
	public void StringIsNullOrEmptyThrowArgumentException()
	{
		string? _null  = null;
		string? _empty = string.Empty;

		Assert.Throws<ArgumentException>(() => _null.ThrowIfNullOrEmpty<ArgumentException>());
		Assert.Throws<ArgumentException>(() => _null.ThrowIfNullOrEmpty<ArgumentException>("Null Exception"));
		Assert.Throws<ArgumentException>(() => _null.ThrowIfNullOrEmpty<ArgumentException>("Null Exception", nameof(_null)));

		Assert.Throws<ArgumentException>(() => _empty.ThrowIfNullOrEmpty<ArgumentException>());
		Assert.Throws<ArgumentException>(() => _empty.ThrowIfNullOrEmpty<ArgumentException>("Null Exception"));
		Assert.Throws<ArgumentException>(() => _empty.ThrowIfNullOrEmpty<ArgumentException>("Null Exception", nameof(_null)));
	}

	[Fact]
	public void StringIsNotNullOrEmptyReturnValue()
	{
		string abc = "abc";

		Assert.Equal(abc, abc.ThrowIfNullOrEmpty());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty("Null Exception", nameof(abc)));

		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentNullException>());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentNullException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<CustomNullException>());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<CustomNullException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<CustomNullException>("Null Exception", nameof(abc)));
	}

	#endregion

	#region ThrowIfNullOrWhiteSpace

	[Fact]
	public void StringIsNullOrWhiteSpaceThrowException()
	{
		string? _null  = null;
		string? _empty = string.Empty;
		string? _space = " ";


		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNullOrWhitespace());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNullOrWhitespace<ArgumentNullException>());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNullOrWhitespace<ArgumentNullException>("Null Exception"));
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNullOrWhitespace<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNullOrWhitespace<CustomNullException>("Null Exception"));

		Assert.Throws<ArgumentNullException>(() => _empty.ThrowIfNullOrWhitespace());
		Assert.Throws<ArgumentNullException>(() => _empty.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _empty.ThrowIfNullOrWhitespace<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _empty.ThrowIfNullOrWhitespace<CustomNullException>());

		Assert.Throws<ArgumentNullException>(() => _space.ThrowIfNullOrWhitespace());
		Assert.Throws<ArgumentNullException>(() => _space.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _space.ThrowIfNullOrWhitespace<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _space.ThrowIfNullOrWhitespace<CustomNullException>());
	}

	[Fact]
	public void StringIsNotNullOrWhitespaceThenReturn()
	{
		string? _abc = "abc";

		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentNullException>());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentException>());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentException>("Null Exception"));
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentException>("Null Exception", nameof(_abc)));
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<CustomNullException>());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<CustomNullException>("Null Exception"));
	}

	#endregion

	#region BooleanIfNull

	[Fact]
	public void CheckTrueIfNullThenReturnTrue()
	{
		byte?    _byte    = null;
		short?   _short   = null;
		int?     _int     = null;
		long?    _long    = null;
		float?   _float   = null;
		double?  _double  = null;
		decimal? _decimal = null;
		char?    _char    = null;
		string?  _string  = null;

		Assert.True(_byte.TrueIfNull());
		Assert.True(_short.TrueIfNull());
		Assert.True(_int.TrueIfNull());
		Assert.True(_long.TrueIfNull());
		Assert.True(_float.TrueIfNull());
		Assert.True(_double.TrueIfNull());
		Assert.True(_decimal.TrueIfNull());
		Assert.True(_char.TrueIfNull());
		Assert.True(_string.TrueIfNull());
	}

	[Fact]
	public void CheckTrueIfExistReturnFalse()
	{
		byte?    _byte    = 0;
		short?   _short   = 0;
		int?     _int     = 0;
		long?    _long    = 0;
		float?   _float   = 0;
		double?  _double  = 0.0;
		decimal? _decimal = 0;
		char?    _char    = 'a';
		string?  _string  = "xyz";

		Assert.False(_byte.TrueIfNull());
		Assert.False(_short.TrueIfNull());
		Assert.False(_int.TrueIfNull());
		Assert.False(_long.TrueIfNull());
		Assert.False(_float.TrueIfNull());
		Assert.False(_double.TrueIfNull());
		Assert.False(_decimal.TrueIfNull());
		Assert.False(_char.TrueIfNull());
		Assert.False(_string.TrueIfNull());
	}

	[Fact]
	public void CheckFalseIfNullThenReturnFalse()
	{
		byte?    _byte    = null;
		short?   _short   = null;
		int?     _int     = null;
		long?    _long    = null;
		float?   _float   = null;
		double?  _double  = null;
		decimal? _decimal = null;
		char?    _char    = null;
		string?  _string  = null;

		Assert.False(_byte.FalseIfNull());
		Assert.False(_short.FalseIfNull());
		Assert.False(_int.FalseIfNull());
		Assert.False(_long.FalseIfNull());
		Assert.False(_float.FalseIfNull());
		Assert.False(_double.FalseIfNull());
		Assert.False(_decimal.FalseIfNull());
		Assert.False(_char.FalseIfNull());
		Assert.False(_string.FalseIfNull());
	}

	[Fact]
	public void CheckFalseIfExistReturnFalse()
	{
		byte?    _byte    = 0;
		short?   _short   = 0;
		int?     _int     = 0;
		long?    _long    = 0;
		float?   _float   = 0;
		double?  _double  = 0.0;
		decimal? _decimal = 0;
		char?    _char    = 'a';
		string?  _string  = "xyz";

		Assert.True(_byte.FalseIfNull());
		Assert.True(_short.FalseIfNull());
		Assert.True(_int.FalseIfNull());
		Assert.True(_long.FalseIfNull());
		Assert.True(_float.FalseIfNull());
		Assert.True(_double.FalseIfNull());
		Assert.True(_decimal.FalseIfNull());
		Assert.True(_char.FalseIfNull());
		Assert.True(_string.FalseIfNull());
	}

	#endregion

	#region NumericIfNull

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

	#endregion

	#region ThrowIfNotInRange

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
		// The index must be 1 less then the upper bound.
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

	#endregion

	#region ThrowIfCompareNotExpect

	[Fact]
	public void ThrowIfEqualCondition()
	{
		Assert.False(1.ThrowIfEqual(0));
		Assert.Throws<ArgumentEqualException>(() => 1.ThrowIfEqual(1));
	}

	[Fact]
	public void ThrowIfEqualExtension()
	{
		var test = "test";
		Assert.False(test.Length.ThrowIfEqual(8));
		Assert.Throws<ArgumentEqualException>(() => test.Length.ThrowIfEqual(4));
		Assert.Throws<ArgumentEqualException>(() => test.Length.ThrowIfEqual<ArgumentEqualException>(4));
		Assert.Throws<ArgumentEqualException>(() => test.Length.ThrowIfEqual<ArgumentEqualException>(4, nameof(test)));
	}

	[Fact]
	public void ThrowIfNotEqualCondition()
	{
		Assert.True(1.ThrowIfNotEqual(1));
		Assert.Throws<ArgumentNotEqualException>(() => 1.ThrowIfNotEqual(0));
	}

	[Fact]
	public void ThrowIfNotEqualExtension()
	{
		var test = "test";
		Assert.True(test.Length.ThrowIfNotEqual(4));
		Assert.Throws<ArgumentNotEqualException>(() => test.Length.ThrowIfNotEqual(8));
		Assert.Throws<ArgumentNotEqualException>(() => test.Length.ThrowIfNotEqual<ArgumentNotEqualException>(8));
		Assert.Throws<ArgumentNotEqualException>(() => test.Length.ThrowIfNotEqual<ArgumentNotEqualException>(8, nameof(test)));
	}

	[Fact]
	public void LessThanExpectedTrue()
	{
		Assert.True(1.ThrowIfLessThan(0));
		Assert.True(1.ThrowIfLessThan(1));
		Assert.True(0.ThrowIfLessThan(0));
		Assert.True(1.ThrowIfLessThan(0, nameof(LessThanExpectedTrue)));
		Assert.True(1.ThrowIfLessThan(1, nameof(LessThanExpectedTrue)));
		Assert.True(0.ThrowIfLessThan(0, nameof(LessThanExpectedTrue)));
	}

	[Fact]
	public void LessThanExpectedThrow()
	{
		Assert.Throws<ArgumentLessThanException>(() => 0.ThrowIfLessThan(1));
		Assert.Throws<ArgumentLessThanException>(() => 0.ThrowIfLessThan(1, nameof(LessThanExpectedThrow)));
		Assert.Throws<ArgumentLessThanException>(() => 0.ThrowIfLessThan<ArgumentLessThanException>(1));
		Assert.Throws<ArgumentLessThanException>(() => 0.ThrowIfLessThan<ArgumentLessThanException>(1, nameof(LessThanExpectedThrow)));
	}

	[Fact]
	public void MoreThanExpectedTrue()
	{
		Assert.True(0.ThrowIfMoreThan(1));
		Assert.True(1.ThrowIfMoreThan(1));
		Assert.True(0.ThrowIfMoreThan(0));
		Assert.True(0.ThrowIfMoreThan(1, nameof(MoreThanExpectedTrue)));
		Assert.True(1.ThrowIfMoreThan(1, nameof(MoreThanExpectedTrue)));
		Assert.True(0.ThrowIfMoreThan(0, nameof(MoreThanExpectedTrue)));
	}

	[Fact]
	public void MoreThanExpectedThrow()
	{
		Assert.Throws<ArgumentMoreThanException>(() => 1.ThrowIfMoreThan(0));
		Assert.Throws<ArgumentMoreThanException>(() => 1.ThrowIfMoreThan(0, nameof(MoreThanExpectedThrow)));
		Assert.Throws<ArgumentMoreThanException>(() => 1.ThrowIfMoreThan<ArgumentMoreThanException>(0));
		Assert.Throws<ArgumentMoreThanException>(() => 1.ThrowIfMoreThan<ArgumentMoreThanException>(0, nameof(MoreThanExpectedThrow)));
	}

	[Fact]
	public void ThrowIfZeroTruePositive()
	{
		var positive = 1;
		Assert.True(positive.ThrowIfZero());
		Assert.True(positive.ThrowIfZero(nameof(ThrowIfZeroTruePositive)));
		Assert.True(positive.ThrowIfZero<ArgumentZeroException>());
		Assert.True(positive.ThrowIfZero<ArgumentZeroException>(nameof(ThrowIfZeroTruePositive)));
	}
	[Fact]
	public void ThrowIfZeroTrueNegative()
	{
		var negative = -1;
		Assert.True(negative.ThrowIfZero());
		Assert.True(negative.ThrowIfZero(nameof(ThrowIfZeroTrueNegative)));
		Assert.True(negative.ThrowIfZero<ArgumentZeroException>());
		Assert.True(negative.ThrowIfZero<ArgumentZeroException>(nameof(ThrowIfZeroTrueNegative)));
	}
	[Fact]
	public void ThrowIfZeroFail()
	{
		var zero = 0;
		Assert.Throws<ArgumentZeroException>(() => zero.ThrowIfZero());
		Assert.Throws<ArgumentZeroException>(() => zero.ThrowIfZero(nameof(ThrowIfZeroFail)));
		Assert.Throws<ArgumentZeroException>(() => zero.ThrowIfZero<ArgumentZeroException>());
		Assert.Throws<ArgumentZeroException>(() => zero.ThrowIfZero<ArgumentZeroException>(nameof(ThrowIfZeroFail)));
	}
	
	#endregion
}