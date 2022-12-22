// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai;

public class CheckTests
{
    [Fact]
    public void BooleanIfNullThrow()
    {
        bool? _bool = null;

        Assert.Throws<ArgumentNullException>(() => _bool.IfNullThrow());
        Assert.Throws<CustomNullException>(() => _bool.IfNullThrow<CustomNullException>());
    }

    [Fact]
    public void IntegralIfNullThrowNullException()
    {
        sbyte? _sbyte = null;
        byte?  _byte  = null;
        short? _short = null;
        int?   _int   = null;
        long?  _long  = null;

        ushort? _ushort = null;
        uint?   _uint   = null;
        ulong?  _ulong  = null;

        Assert.Throws<ArgumentNullException>(() => _sbyte.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _byte.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _short.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _int.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _long.IfNullThrow());

        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_sbyte));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_byte));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_short));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_int));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_long));
    }

    [Fact]
    public void IntegralIfNullThrowCustomException()
    {
        sbyte? _sbyte = null;
        byte?  _byte  = null;
        short? _short = null;
        int?   _int   = null;
        long?  _long  = null;

        Assert.Throws<CustomNullException>(() => _sbyte.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _byte.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _short.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _int.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _long.IfNullThrow<CustomNullException>());

        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_sbyte));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_byte));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_short));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_int));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_long));
    }

    [Fact]
    public void PositiveIntegralIfNullThrowNullException()
    {
        ushort? _ushort = null;
        uint?   _uint   = null;
        ulong?  _ulong  = null;

        Assert.Throws<ArgumentNullException>(() => _ushort.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _uint.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _ulong.IfNullThrow());

        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_ushort));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_uint));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_ulong));
    }

    [Fact]
    public void NativeIntegralIfNull()
    {
        nint?  _nint  = null;
        nuint? _nuint = null;

        Assert.Throws<ArgumentNullException>(() => _nint.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _nuint.IfNullThrow());
        Assert.Throws<CustomNullException>(() => _nint.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _nuint.IfNullThrow<CustomNullException>());
    }

    [Fact]
    public void PositiveIntegralIfNullThrowCustomException()
    {
        ushort? _ushort = null;
        uint?   _uint   = null;
        ulong?  _ulong  = null;

        Assert.Throws<CustomNullException>(() => _ushort.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _uint.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _ulong.IfNullThrow<CustomNullException>());

        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_ushort));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_uint));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_ulong));
    }


    [Fact]
    public void FloatingIsNullThrowNullException()
    {
        float?   _float   = null;
        double?  _double  = null;
        decimal? _decimal = null;

        Assert.Throws<ArgumentNullException>(() => _float.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _double.IfNullThrow());
        Assert.Throws<ArgumentNullException>(() => _decimal.IfNullThrow());

        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_float));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_double));
        Assert.Throws<ArgumentNullException>(() => Check.IfNullThrow(_decimal));
    }

    [Fact]
    public void FloatingIsNullThrowCustomException()
    {
        float?   _float   = null;
        double?  _double  = null;
        decimal? _decimal = null;

        Assert.Throws<CustomNullException>(() => _float.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _double.IfNullThrow<CustomNullException>());
        Assert.Throws<CustomNullException>(() => _decimal.IfNullThrow<CustomNullException>());

        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_float));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_double));
        Assert.Throws<CustomNullException>(() => Check.IfNullThrow<CustomNullException>(_decimal));
    }

    [Fact]
    public void IntegralIsNull()
    {
        byte?  byte1  = null;
        short? short2 = null;
        int?   int4   = null;
        long?  long8  = null;

        Assert.Throws<ArgumentNullException>(() => Check.NotNull(byte1));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(short2));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(int4));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(long8));
    }

    [Fact]
    public void FloatingIsNull()
    {
        float?   float16   = null;
        double?  double32  = null;
        decimal? decimal32 = null;

        Assert.Throws<ArgumentNullException>(() => Check.NotNull(float16));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(double32));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(decimal32));
    }

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
        Assert.True(1.NotLessThan(0));
        Assert.True(1.NotLessThan(1));
        Assert.True(0.NotLessThan(0));
        Assert.Throws<ArgumentLessThanException>(() => 0.NotLessThan(1));
    }

    [Fact]
    public void MoreThanExpected()
    {
        Assert.True(0.NotMoreThan(1));
        Assert.True(1.NotMoreThan(1));
        Assert.True(0.NotMoreThan(0));
        Assert.Throws<ArgumentMoreThanException>(() => 1.NotMoreThan(0));
    }

    [Fact]
    public void NotEqual()
    {
        Assert.True(1.NotEqual(1));
        Assert.Throws<ArgumentEqualException>(() => 1.NotEqual(0));
    }

    [Fact]
    public void NotEqualExtension()
    {
        var test = "test";
        Assert.True(test.Length.NotEqual(4, new ArgumentTestException()));
        Assert.Throws<ArgumentTestException>(() => test.Length.NotEqual(8, new ArgumentTestException()));
    }
}

public class ArgumentTestException : ArgumentException
{
}