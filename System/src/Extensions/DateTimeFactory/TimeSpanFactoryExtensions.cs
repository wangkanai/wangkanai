// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.DateTimeFactory;

public static class TimeSpanFactoryExtensions
{
	public static TimeSpan Ticks(this int ticks)
	{
		return TimeSpan.FromTicks(ticks);
	}

	public static TimeSpan Milliseconds(this int milliseconds)
	{
		return TimeSpan.FromMilliseconds(milliseconds);
	}

	public static TimeSpan Seconds(this int seconds)
	{
		return TimeSpan.FromSeconds(seconds);
	}

	public static TimeSpan Minutes(this int minutes)
	{
		return TimeSpan.FromMinutes(minutes);
	}

	public static TimeSpan Hours(this int hours)
	{
		return TimeSpan.FromHours(hours);
	}

	public static TimeSpan Days(this int days)
	{
		return TimeSpan.FromDays(days);
	}
}