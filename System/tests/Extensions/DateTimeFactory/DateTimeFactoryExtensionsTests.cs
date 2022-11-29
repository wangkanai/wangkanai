// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions.DateTimeFactory;

public class DateTimeFactoryExtensionsTests
{
    [Fact]
    public void InvalidDay()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => 29.February(2007));
    }

    [Fact]
    public void LeapYear()
    {
        var day = 29.February(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(2, day.Month);
        Assert.Equal(29, day.Day);
    }
    [Fact]
    public void January()
    {
        var day = 15.January(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(1, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void February()
    {
        var day = 15.February(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(2, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void March()
    {
        var day = 15.March(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(3, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void April()
    {
        var day = 15.April(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(4, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void May()
    {
        var day = 15.May(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(5, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void June()
    {
        var day = 15.June(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(6, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void July()
    {
        var day = 15.July(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(7, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void August()
    {
        var day = 15.August(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(8, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void September()
    {
        var day = 15.September(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(9, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void October()
    {
        var day = 15.October(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(10, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void November()
    {
        var day = 15.November(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(11, day.Month);
        Assert.Equal(15, day.Day);
    }
    
    [Fact]
    public void December()
    {
        var day = 15.December(2008);
        Assert.Equal(2008, day.Year);
        Assert.Equal(12, day.Month);
        Assert.Equal(15, day.Day);
    }
}