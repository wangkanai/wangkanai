// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions;

/// <summary>Provides extension methods for the DateTime class.</summary>
[DebuggerStepThrough]
public static class DateTimeExtensions
{
   /// <summary>Truncates a DateTime value based on a given TimeSpan.</summary>
   /// <param name="dateTime">The DateTime value to be truncated.</param>
   /// <param name="timeSpan">The TimeSpan value representing the unit to truncate to.</param>
   /// <returns>The truncated DateTime value.</returns>
   public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
      => timeSpan != TimeSpan.Zero
         ? dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks)) // Or could throw an ArgumentException
         : dateTime;
}