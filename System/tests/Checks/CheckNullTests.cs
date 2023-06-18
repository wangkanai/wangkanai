// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using Xunit;

namespace Wangkanai.Checks;

public class CheckNullTests
{
	[Fact]
	public void BooleanThrowIfNull()
	{
		bool? _bool = null;

		Assert.Throws<ArgumentNullException>(() => _bool.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _bool.ThrowIfNull<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _bool.ThrowIfNull<CustomNullException>());
	}

	[Fact]
	public void BooleanReturnTrueIfNotNull()
	{
		bool? _false = false;
		bool? _true  = true;

		Assert.True(_true.ThrowIfNull());
		Assert.True(_true.ThrowIfNull<ArgumentNullException>());
		Assert.True(_true.ThrowIfNull<CustomNullException>());

		Assert.False(_false.ThrowIfNull());
		Assert.False(_false.ThrowIfNull<ArgumentNullException>());
		Assert.False(_false.ThrowIfNull<CustomNullException>());
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

	[Fact]
	public void ObjectIsNull()
	{
		object? _null = null;

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNull<CustomNullException>());

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNull<CustomNullException>("Null Exception"));
	}

	[Fact]
	public void ObjectIsNotNullThenReturn()
	{
		object? _obj = new object();

		Assert.Equal(_obj, _obj.ThrowIfNull());
		Assert.Equal(_obj, _obj.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_obj, _obj.ThrowIfNull<CustomNullException>());

		Assert.Equal(_obj, _obj.ThrowIfNull("Null Exception"));
		Assert.Equal(_obj, _obj.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_obj, _obj.ThrowIfNull<CustomNullException>("Null Exception"));
	}

	[Fact]
	public void ObjectIsStringThenReturn()
	{
		object? _string = "abc";

		Assert.Equal(_string, _string.ThrowIfNull());
		Assert.Equal(_string, _string.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_string, _string.ThrowIfNull<CustomNullException>());

		Assert.Equal(_string, _string.ThrowIfNull("Null Exception"));
		Assert.Equal(_string, _string.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_string, _string.ThrowIfNull<CustomNullException>("Null Exception"));
	}

	[Fact]
	public void StringIsNullThrowNullException()
	{
		string? _null = null;

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>());
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNull<CustomNullException>());

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Throws<CustomNullException>(() => _null.ThrowIfNull<CustomNullException>("Null Exception"));
	}

	[Fact]
	public void StringReturnIfNotNull()
	{
		string? abc = "abc";


		Assert.Equal(abc, abc.ThrowIfNull());
		Assert.Equal(abc, abc.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(abc, abc.ThrowIfNull<CustomNullException>());

		Assert.Equal(abc, abc.ThrowIfNull("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNull<CustomNullException>("Null Exception"));
	}

	[Fact]
	public void StringEmptyReturnIfNotNull()
	{
		string? _empty = string.Empty;

		Assert.Equal(_empty, _empty.ThrowIfNull());
		Assert.Equal(_empty, _empty.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_empty, _empty.ThrowIfNull<CustomNullException>());

		Assert.Equal(_empty, _empty.ThrowIfNull("Null Exception"));
		Assert.Equal(_empty, _empty.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_empty, _empty.ThrowIfNull<CustomNullException>("Null Exception"));
	}

	[Fact]
	public void StringSpaceReturnIfNotNull()
	{
		string? _space = " ";

		Assert.Equal(_space, _space.ThrowIfNull());
		Assert.Equal(_space, _space.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_space, _space.ThrowIfNull<CustomNullException>());

		Assert.Equal(_space, _space.ThrowIfNull("Null Exception"));
		Assert.Equal(_space, _space.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_space, _space.ThrowIfNull<CustomNullException>("Null Exception"));
	}
}