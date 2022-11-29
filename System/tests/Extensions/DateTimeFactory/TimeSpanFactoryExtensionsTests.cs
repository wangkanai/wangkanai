// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions.DateTimeFactory;

public class TimeSpanFactoryExtensionsTests
{
    [Fact]
    public void Ticks()
    {
        var t = 30.Ticks();
        Assert.Equal(30, t.Ticks);
    }

    [Fact]
    public void Milliseconds()
    {
        var t = 30.Milliseconds();
        Assert.Equal(30 * TimeSpan.TicksPerMillisecond, t.Ticks);
    }
    
    [Fact]
    public void Seconds()
    {
        var t = 30.Seconds();
        Assert.Equal(30 * TimeSpan.TicksPerSecond, t.Ticks);
    }
    
    [Fact]
    public void Minutes()
    {
        var t = 30.Minutes();
        Assert.Equal(30 * TimeSpan.TicksPerMinute, t.Ticks);
    }
    
    [Fact]
    public void Hours()
    {
        var t = 30.Hours();
        Assert.Equal(30 * TimeSpan.TicksPerHour, t.Ticks);
    }
    
    [Fact]
    public void Days()
    {
        var t = 30.Days();
        Assert.Equal(30 * TimeSpan.TicksPerDay, t.Ticks);
    }
}