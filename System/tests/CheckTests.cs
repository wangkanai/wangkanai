// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;


#nullable enable

namespace Wangkanai;

public class CheckTests
{
	#region ThrowIfNull

	[Fact]
	public void BooleanThrowIfNull()
	{
		bool? _bool = null;

		Assert.Throws<ArgumentNullException>(() => _bool.ThrowIfNull());
		Assert.Throws<CustomNullException>(() => _bool.ThrowIfNull<CustomNullException>());
	}

	[Fact]
	public void IntegralThrowIfNullNullException()
	{
		sbyte? _sbyte = null;
		byte?  _byte  = null;
		short? _short = null;
		int?   _int   = null;
		long?  _long  = null;

		Assert.Throws<ArgumentNullException>(() => _sbyte.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _byte.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _short.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _int.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _long.ThrowIfNull());

		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_sbyte));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_byte));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_short));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_int));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_long));
	}

	[Fact]
	public void IntegralThrowIfNullCustomException()
	{
		sbyte? _sbyte = null;
		byte?  _byte  = null;
		short? _short = null;
		int?   _int   = null;
		long?  _long  = null;

		Assert.Throws<CustomNullException>(() => _sbyte.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _byte.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _short.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _int.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _long.ThrowIfNull<CustomNullException>());

		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_sbyte));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_byte));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_short));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_int));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_long));
	}

	[Fact]
	public void PositiveIntegralThrowIfNullNullException()
	{
		ushort? _ushort = null;
		uint?   _uint   = null;
		ulong?  _ulong  = null;

		Assert.Throws<ArgumentNullException>(() => _ushort.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _uint.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _ulong.ThrowIfNull());

		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_ushort));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_uint));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_ulong));
	}

	[Fact]
	public void NativeIntegralIfNull()
	{
		nint?  _nint  = null;
		nuint? _nuint = null;

		Assert.Throws<ArgumentNullException>(() => _nint.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _nuint.ThrowIfNull());
		Assert.Throws<CustomNullException>(() => _nint.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _nuint.ThrowIfNull<CustomNullException>());
	}

	[Fact]
	public void PositiveIntegralThrowIfNullCustomException()
	{
		ushort? _ushort = null;
		uint?   _uint   = null;
		ulong?  _ulong  = null;

		Assert.Throws<CustomNullException>(() => _ushort.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _uint.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _ulong.ThrowIfNull<CustomNullException>());

		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_ushort));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_uint));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_ulong));
	}


	[Fact]
	public void FloatingIsNullThrowNullException()
	{
		float?   _float   = null;
		double?  _double  = null;
		decimal? _decimal = null;

		Assert.Throws<ArgumentNullException>(() => _float.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _double.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _decimal.ThrowIfNull());

		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_float));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_double));
		Assert.Throws<ArgumentNullException>(() => Check.ThrowIfNull(_decimal));
	}

	[Fact]
	public void FloatingIsNullThrowCustomException()
	{
		float?   _float   = null;
		double?  _double  = null;
		decimal? _decimal = null;

		Assert.Throws<CustomNullException>(() => _float.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _double.ThrowIfNull<CustomNullException>());
		Assert.Throws<CustomNullException>(() => _decimal.ThrowIfNull<CustomNullException>());

		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_float));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_double));
		Assert.Throws<CustomNullException>(() => Check.ThrowIfNull<CustomNullException>(_decimal));
	}

	#endregion

	#region ThrowIfNullOrEmpty

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
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentNullException>());

		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentException>());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentException>("Null Exception", nameof(abc)));
	}

	#endregion

	#region TrueIfNull

	[Fact]
	public void IfNullThenReturnTrue()
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
	public void TrueIfNullReturnFalse()
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

	#endregion

	#region GenericNull

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
#pragma warning disable CS0612
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

#pragma warning restore CS0612
	[Fact]
	public void ListIsNullOrEmpty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => Check.NotNullOrEmpty(null));
	}

	[Fact]
	public void ListIsEmpty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => new List<int>().NotNullOrEmpty());
	}

	[Fact]
	public void ListIsExist()
	{
		var list = new List<int>();
		for (var i = 0; i <= 9; i++) list.Add(i);

		Assert.True(list.NotNullOrEmpty());
	}

	[Fact]
	public void LessThanExpected()
	{
		Assert.True(1.ThrowIfLessThan(0));
		Assert.True(1.ThrowIfLessThan(1));
		Assert.True(0.ThrowIfLessThan(0));
		Assert.Throws<ArgumentLessThanException>(() => 0.ThrowIfLessThan(1));
	}

	[Fact]
	public void MoreThanExpected()
	{
		Assert.True(0.ThrowIfMoreThan(1));
		Assert.True(1.ThrowIfMoreThan(1));
		Assert.True(0.ThrowIfMoreThan(0));
		Assert.Throws<ArgumentMoreThanException>(() => 1.ThrowIfMoreThan(0));
	}

	[Fact]
	public void NotEqual()
	{
		Assert.True(1.ThrowIfNotEqual(1));
		Assert.Throws<ArgumentNotEqualException>(() => 1.ThrowIfNotEqual(0));
	}

	[Fact]
	public void NotEqualExtension()
	{
		var test = "test";
		Assert.True(test.Length.ThrowIfNotEqual(4));
		Assert.Throws<ArgumentTestException>(() => test.Length.ThrowIfNotEqual<ArgumentTestException>(8, nameof(test)));
	}

	#endregion
}

public class ArgumentTestException : ArgumentException
{
	public ArgumentTestException(string message) : base(message) { }
}