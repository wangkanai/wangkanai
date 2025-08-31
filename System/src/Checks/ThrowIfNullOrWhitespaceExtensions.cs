// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

/// <summary>Extension methods for throwing exceptions if a string is null or whitespace.</summary>
[DebuggerStepThrough]
public static class ThrowIfNullOrWhitespaceExtensions
{
   /// <summary>Throws an exception of type <see cref="ArgumentNullOrWhitespaceException"/> if the specified string value is null or whitespace.</summary>
   /// <param name="value">The string value to check.</param>
   /// <returns>The original string value if it is not null or whitespace.</returns>
   public static string ThrowIfNullOrWhitespace([NotNull] this string? value)
      => value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>();

   /// <summary>Throws an exception of type <see cref="ArgumentNullOrWhitespaceException"/> if the specified string value is null or whitespace.</summary>
   /// <param name="value">The string value to check.</param>
   /// <param name="message">The custom error message for the exception.</param>
   /// <returns>The original string value if it is not null or whitespace.</returns>
   public static string ThrowIfNullOrWhitespace([NotNull] this string? value, string message)
      => value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(message);

   /// <summary>Throws an exception of type <see cref="ArgumentNullOrWhitespaceException"/> if the specified string value is null or whitespace.</summary>
   /// <typeparam name="T">The type of the exception to throw.</typeparam>
   /// <param name="value">The string value to check.</param>
   /// <returns>The original string value if it is not null or whitespace.</returns>
   public static string ThrowIfNullOrWhitespace<T>([NotNull] this string? value)
      where T : ArgumentException
      => value.ThrowIfNullOrWhitespace<T>(nameof(value));

   /// <summary>Throws an exception of type <see cref="ArgumentNullOrWhitespaceException"/> if the specified string value is null or whitespace.</summary>
   /// <typeparam name="T">The type of the exception to throw.</typeparam>
   /// <param name="value">The string value to check.</param>
   /// <param name="message">The custom error message for the exception.</param>
   /// <returns>The original string value if it is not null or whitespace.</returns>
   public static string ThrowIfNullOrWhitespace<T>([NotNull] this string? value, string message)
      where T : ArgumentException
      => value.ThrowIfNullOrWhitespace<T>(message, nameof(value));

   /// <summary>Throws an exception of type <see cref="ArgumentNullOrWhitespaceException"/> if the specified string value is null or whitespace.</summary>
   /// <typeparam name="T">The type of the exception to throw.</typeparam>
   /// <param name="value">The string value to check.</param>
   /// <param name="message">The custom error message for the exception.</param>
   /// <param name="paramName">The parameter name that thrown the exception.</param>
   /// <returns>The original string value if it is not null or whitespace.</returns>
   public static string ThrowIfNullOrWhitespace<T>([NotNull] this string? value, string message, string paramName)
      where T : ArgumentException
      => value!.IsNullOrWhiteSpace()
         ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
         : value!;
}