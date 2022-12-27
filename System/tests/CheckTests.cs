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

        ushort? _ushort = null;
        uint?   _uint   = null;
        ulong?  _ulong  = null;

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

    #region IfNullThen

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