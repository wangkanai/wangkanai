// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class DateTimeExtensions
{
	public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
		=> timeSpan != TimeSpan.Zero
			   ? dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks)) // Or could throw an ArgumentException
			   : dateTime;
}
