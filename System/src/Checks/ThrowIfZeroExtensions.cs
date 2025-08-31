// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>A static class that provides extension methods to throw exceptions if a value is zero.</summary>
[DebuggerStepThrough]
public static class ThrowIfZeroExtensions
{
   /// <summary>Throws an exception of type T if the given value is zero.</summary>
   /// <param name="value">The value to check.</param>
   /// <returns>The original value if it is not zero.</returns>
   /// <exception cref="ArgumentException">Thrown if the value is zero.</exception>
   public static int ThrowIfZero([NotNull] this int value)
      => value.ThrowIfZero<ArgumentZeroException>();

   /// <summary>Throws an exception of type <exception cref="ArgumentZeroException"></exception> if the given value is zero.</summary>
   /// <param name="value">The value to check.</param>
   /// <param name="message">The custom error message of exception thrown.</param>
   /// <returns>The original value if it is not zero.</returns>
   /// <exception cref="ArgumentException">Thrown if the value is zero.</exception>
   public static int ThrowIfZero([NotNull] this int value, string message)
      => value.ThrowIfZero<ArgumentZeroException>(message);

   /// <summary>Throws an exception of type <see cref="ArgumentException"/> if the given value is zero.</summary>
   /// <typeparam name="T">The type of exception to throw.</typeparam>
   /// <param name="value">The value to check.</param>
   /// <returns>The original value if it is not zero.</returns>
   /// <exception cref="ArgumentException">Thrown if the value is zero.</exception>
   public static int ThrowIfZero<T>([NotNull] this int value)
      where T : ArgumentException
      => value == 0
         ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value))
         : value;

   /// <summary>Throws an exception of type <see cref="ArgumentException"/> if the given value is zero.</summary>
   /// <typeparam name="T">The type of exception to throw.</typeparam>
   /// <param name="value">The value to check.</param>
   /// <param name="message">The custom error message of exception thrown.</param>
   /// <returns>The original value if it is not zero.</returns>
   /// <exception cref="ArgumentException">Thrown if the value is zero.</exception>
   public static int ThrowIfZero<T>([NotNull] this int value, string message)
      where T : ArgumentException
      => value == 0
         ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
         : value;
}