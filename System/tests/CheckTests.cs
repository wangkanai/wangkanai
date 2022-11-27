// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Runtime;

public class CheckTests
{
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