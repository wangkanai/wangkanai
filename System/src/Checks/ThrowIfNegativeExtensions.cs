// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>Provides extension methods for throwing exceptions if a value is negative.</summary>
[DebuggerStepThrough]
public static class ThrowIfNegativeExtensions
{
   /// <summary>Throws an exception if the specified value is negative.</summary>
   /// <param name="value">The value to check.</param>
   /// <returns>The value itself if it is non-negative.</returns>
   public static int ThrowIfNegative([NotNull] this int value)
      => value.ThrowIfNegative<ArgumentNegativeException>();

   /// <summary>Throws an exception if the specified value is negative.</summary>
   /// <param name="value">The value to check.</param>
   /// <param name="message">The exception error message.</param>
   /// <returns>The value itself if it is non-negative.</returns>
   public static int ThrowIfNegative([NotNull] this int value, string message)
      => value.ThrowIfNegative<ArgumentNegativeException>(message);

   /// <summary>Throws an exception if the specified value is negative.</summary>
   /// <param name="value">The value to check.</param>
   /// <returns>The value itself if it is non-negative.</returns>
   /// <typeparam name="T">The argument exception thrown when failure.</typeparam>
   public static int ThrowIfNegative<T>([NotNull] this int value)
      where T : ArgumentException
      => value.ThrowIfNegative<T>(nameof(value));

   /// <summary>Throws an exception if the specified value is negative.</summary>
   /// <param name="value">The value to check.</param>
   /// <param name="message">The exception error message.</param>
   /// <returns>The value itself if it is non-negative.</returns>
   /// <typeparam name="T">The argument exception thrown when failure.</typeparam>
   public static int ThrowIfNegative<T>([NotNull] this int value, string message)
      where T : ArgumentException
      => value < 0
         ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
         : value;
}