// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.TimeFactory;

public static class TimeSpanFactoryExtensions
{
    public static TimeSpan Ticks(this int ticks)
        => TimeSpan.FromTicks(ticks);

    public static TimeSpan Milliseconds(this int milliseconds)
        => TimeSpan.FromMilliseconds(milliseconds);

    public static TimeSpan Second(this int seconds)
        => TimeSpan.FromSeconds(seconds);

    public static TimeSpan Minutes(this int minutes)
        => TimeSpan.FromMinutes(minutes);

    public static TimeSpan Hour(this int hours)
        => TimeSpan.FromHours(hours);

    public static TimeSpan Days(this int days)
        => TimeSpan.FromHours(days);
}