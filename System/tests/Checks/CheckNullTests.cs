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
		Assert.Throws<CustomArgumentException>(() => _bool.ThrowIfNull<CustomArgumentException>());
	}

	[Fact]
	public void BooleanReturnTrueIfNotNull()
	{
		bool? _false = false;
		bool? _true  = true;

		Assert.True(_true.ThrowIfNull());
		Assert.True(_true.ThrowIfNull<ArgumentNullException>());
		Assert.True(_true.ThrowIfNull<CustomArgumentException>());

		Assert.False(_false.ThrowIfNull());
		Assert.False(_false.ThrowIfNull<ArgumentNullException>());
		Assert.False(_false.ThrowIfNull<CustomArgumentException>());
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

		Assert.Throws<CustomArgumentException>(() => _sbyte.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _byte.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _short.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _int.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _long.ThrowIfNull<CustomArgumentException>());

		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_sbyte));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_byte));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_short));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_int));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_long));
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
		Assert.Throws<CustomArgumentException>(() => _nint.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _nuint.ThrowIfNull<CustomArgumentException>());
	}

	[Fact]
	public void PositiveIntegralThrowIfNullCustomException()
	{
		ushort? _ushort = null;
		uint?   _uint   = null;
		ulong?  _ulong  = null;

		Assert.Throws<CustomArgumentException>(() => _ushort.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _uint.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _ulong.ThrowIfNull<CustomArgumentException>());

		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_ushort));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_uint));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_ulong));
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

		Assert.Throws<CustomArgumentException>(() => _float.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _double.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _decimal.ThrowIfNull<CustomArgumentException>());

		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_float));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_double));
		Assert.Throws<CustomArgumentException>(() => Check.ThrowIfNull<CustomArgumentException>(_decimal));
	}

	[Fact]
	public void ObjectIsNull()
	{
		object? _null = null;

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>());
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNull<CustomArgumentException>());

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNull<CustomArgumentException>("Null Exception"));    
		
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull("Null Exception", new Exception()));
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>("Null Exception", new Exception()));
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNull<CustomArgumentException>("Null Exception", new Exception()));
	}

	[Fact]
	public void ObjectIsNotNullThenReturn()
	{
		object? _obj = new object();

		Assert.Equal(_obj, _obj.ThrowIfNull());
		Assert.Equal(_obj, _obj.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_obj, _obj.ThrowIfNull<CustomArgumentException>());

		Assert.Equal(_obj, _obj.ThrowIfNull("Null Exception"));
		Assert.Equal(_obj, _obj.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_obj, _obj.ThrowIfNull<CustomArgumentException>("Null Exception"));
		
		Assert.Equal(_obj, _obj.ThrowIfNull("Null Exception", new Exception()));
		Assert.Equal(_obj, _obj.ThrowIfNull<ArgumentNullException>("Null Exception", new Exception()));
		Assert.Equal(_obj, _obj.ThrowIfNull<CustomArgumentException>("Null Exception", new Exception()));
	}

	[Fact]
	public void ObjectIsStringThenReturn()
	{
		object? _string = "abc";

		Assert.Equal(_string, _string.ThrowIfNull());
		Assert.Equal(_string, _string.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_string, _string.ThrowIfNull<CustomArgumentException>());

		Assert.Equal(_string, _string.ThrowIfNull("Null Exception"));
		Assert.Equal(_string, _string.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_string, _string.ThrowIfNull<CustomArgumentException>("Null Exception"));
	}

	[Fact]
	public void StringIsNullThrowNullException()
	{
		string? _null = null;

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>());
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNull<CustomArgumentException>());

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull("Null Exception"));
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNull<CustomArgumentException>("Null Exception"));
	}

	[Fact]
	public void StringReturnIfNotNull()
	{
		string? abc = "abc";


		Assert.Equal(abc, abc.ThrowIfNull());
		Assert.Equal(abc, abc.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(abc, abc.ThrowIfNull<CustomArgumentException>());

		Assert.Equal(abc, abc.ThrowIfNull("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNull<CustomArgumentException>("Null Exception"));
	}

	[Fact]
	public void StringEmptyReturnIfNotNull()
	{
		string? _empty = string.Empty;

		Assert.Equal(_empty, _empty.ThrowIfNull());
		Assert.Equal(_empty, _empty.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_empty, _empty.ThrowIfNull<CustomArgumentException>());

		Assert.Equal(_empty, _empty.ThrowIfNull("Null Exception"));
		Assert.Equal(_empty, _empty.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_empty, _empty.ThrowIfNull<CustomArgumentException>("Null Exception"));
	}

	[Fact]
	public void StringSpaceReturnIfNotNull()
	{
		string? _space = " ";

		Assert.Equal(_space, _space.ThrowIfNull());
		Assert.Equal(_space, _space.ThrowIfNull<ArgumentNullException>());
		Assert.Equal(_space, _space.ThrowIfNull<CustomArgumentException>());

		Assert.Equal(_space, _space.ThrowIfNull("Null Exception"));
		Assert.Equal(_space, _space.ThrowIfNull<ArgumentNullException>("Null Exception"));
		Assert.Equal(_space, _space.ThrowIfNull<CustomArgumentException>("Null Exception"));
	}
}