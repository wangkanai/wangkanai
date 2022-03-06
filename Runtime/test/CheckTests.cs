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
        Assert.Throws<ArgumentNullOrEmptyException>(() => Check.NotNullOrEmpty(new List<int>()));
    }

    [Fact]
    public void ListIsExist()
    {
        var list = new List<int>();
        for (int i = 0; i <= 9; i++) list.Add(i);

        Assert.True(Check.NotNullOrEmpty(list));
    }

    [Fact]
    public void LessThanExpected()
    {
        Assert.True(Check.NotLessThan(1, 0));
        Assert.True(Check.NotLessThan(1, 1));
        Assert.True(Check.NotLessThan(0, 0));
        Assert.Throws<ArgumentLessThanException>(() => Check.NotLessThan(0, 1));
    }

    [Fact]
    public void MoreThanExpected()
    {
        Assert.True(Check.NotMoreThan(0, 1));
        Assert.True(Check.NotMoreThan(1, 1));
        Assert.True(Check.NotMoreThan(0, 0));
        Assert.Throws<ArgumentMoreThanException>(() => Check.NotMoreThan(1, 0));
    }

    [Fact]
    public void NotEqual()
    {
        Assert.True(Check.NotEqual(1, 1));
        Assert.Throws<ArgumentEqualException>(() => Check.NotEqual(1, 0));
    }
}