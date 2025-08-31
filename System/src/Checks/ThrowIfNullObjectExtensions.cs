// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

/// <summary>Provides extension methods for throwing exceptions if an object is null.</summary>
[DebuggerStepThrough]
public static class ThrowIfNullObjectExtensions
{
   /// <summary>Throws an exception to the specified type if the provided object is null.</summary>
   /// <typeparam name="T">The type of exception to throw.</typeparam>
   /// <param name="value">The object to check for null.</param>
   /// <returns>The provided object if it is not null.</returns>
   /// <exception cref="Exception">Thrown when the provided object is null.</exception>
   public static object ThrowIfNull<T>([NotNull] this object? value)
      where T : Exception
      => value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value));

   /// <summary>Throws an exception to the specified type if the provided object is null.</summary>
   /// <typeparam name="T">The type of exception to throw.</typeparam>
   /// <param name="value">The object to check for null.</param>
   /// <param name="message">The custom error message.</param>
   /// <returns>The provided object if it is not null.</returns>
   /// <exception cref="Exception">Thrown when the provided object is null.</exception>
   public static object ThrowIfNull<T>([NotNull] this object? value, string message)
      where T : Exception
      => value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value), message);
}