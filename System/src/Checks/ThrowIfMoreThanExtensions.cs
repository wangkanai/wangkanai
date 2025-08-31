// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>Provides extension methods for throwing an exception if a value is greater than or equal to the expected value.</summary>
[DebuggerStepThrough]
public static class ThrowIfMoreThanExtensions
{
   /// <summary>Throws an exception if the provided value is more than the expected value.</summary>
   /// <param name="value">The value to be checked.</param>
   /// <param name="expected">The expected value.</param>
   /// <returns>True if the value is less than expected, otherwise throws an exception.</returns>
   /// <exception cref="ArgumentMoreThanException">Thrown when the value is more than the expected value.</exception>
   public static bool ThrowIfMoreThan([NotNull] this int value, int expected)
      => value.ThrowIfMoreThan<ArgumentMoreThanException>(expected);

   /// <summary>Throws an exception if the provided value is more than the expected value.</summary>
   /// <param name="value">The value to be checked.</param>
   /// <param name="expected">The expected value.</param>
   /// <param name="message">The exception error message.</param>
   /// <returns>True if the value is less than expected, otherwise throws an exception.</returns>
   /// <exception cref="ArgumentMoreThanException">Thrown when the value is more than the expected value.</exception>
   public static bool ThrowIfMoreThan([NotNull] this int value, int expected, string message)
      => value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, message);

   /// <summary>Throws an exception if the provided value is more than the expected value.</summary>
   /// <param name="value">The value to be checked.</param>
   /// <param name="expected">The expected value.</param>
   /// <typeparam name="T">The generic instance.</typeparam>
   /// <returns>True if the value is less than expected, otherwise throws an exception.</returns>
   /// <exception cref="ArgumentMoreThanException">Thrown when the value is more than the expected value.</exception>
   public static bool ThrowIfMoreThan<T>([NotNull] this int value, int expected)
      where T : ArgumentException
      => value.ThrowIfMoreThan<T>(expected, nameof(value));

   /// <summary>Throws an exception if the provided value is more than the expected value.</summary>
   /// <param name="value">The value to be checked.</param>
   /// <param name="expected">The expected value.</param>
   /// <param name="message">The exception error message.</param>
   /// <typeparam name="T">The generic instance.</typeparam>
   /// <returns>True if the value is less than expected, otherwise throws an exception.</returns>
   /// <exception cref="ArgumentMoreThanException">Thrown when the value is more than the expected value.</exception>
   public static bool ThrowIfMoreThan<T>([NotNull] this int value, int expected, string message)
      where T : ArgumentException
      => value > expected
         ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
         : true;
}