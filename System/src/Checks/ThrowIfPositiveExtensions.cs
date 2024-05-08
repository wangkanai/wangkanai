// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>
/// Provides extension methods to throw an exception if a value is positive.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfPositiveExtensions
{
	/// <summary>
	/// Throws an exception if the specified value is positive.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <returns>The specified value if it is not positive.</returns>
	public static int ThrowIfPositive([NotNull] this int value)
		=> value.ThrowIfPositive<ArgumentPositiveException>();

	/// <summary>
	/// Throws an exception if the specified value is positive.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <param name="message">The custom error message of the exception.</param>
	/// <returns>The specified value if it is not positive.</returns>
	public static int ThrowIfPositive([NotNull] this int value, string message)
		=> value.ThrowIfPositive<ArgumentPositiveException>(message);

	/// <summary>
	/// Throws an exception if the specified value is positive.
	/// </summary>
	/// <typeparam name="T">The type of exception should throw.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <returns>The specified value if it is not positive.</returns>
	public static int ThrowIfPositive<T>([NotNull] this int value)
		where T : ArgumentException
		=> value.ThrowIfPositive<T>(nameof(value));

	/// <summary>
	/// Throws an exception if the specified value is positive.
	/// </summary>
	/// <typeparam name="T">The type of exception should throw.</typeparam>
	/// <param name="value">The value to check.</param>
	/// <param name="message">The custom error message of the exception.</param>
	/// <returns>The specified value if it is not positive.</returns>
	public static int ThrowIfPositive<T>([NotNull] this int value, string message)
		where T : ArgumentException
		=> value > 0
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value;
}
