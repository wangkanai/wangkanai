// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>
/// Provides extension methods for throwing exceptions if a value is less than the expected value.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfLessThanExtensions
{
	/// <summary>
	/// Throws an exception if the provided value is less than the expected value.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <param name="expected">The expected value.</param>
	/// <returns>Returns true if the value is greater than or equal to the expected value, otherwise throws an exception.</returns>
	public static bool ThrowIfLessThan([NotNull] this int value, int expected)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, nameof(value));

	/// <summary>
	/// Throws an exception if the provided value is less than the expected value.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="message">The exception error message.</param>
	/// <exception cref="ArgumentLessThanException">Thrown when the value is less than the expected value.</exception>
	/// <returns>Returns true if the value is greater than or equal to the expected value, otherwise throws an exception.</returns>
	public static bool ThrowIfLessThan([NotNull] this int value, int expected, string message)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, message);

	/// <summary>
	/// Throws an exception if the provided value is less than the expected value.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <param name="expected">The expected value.</param>
	/// <typeparam name="T">The generic instance.</typeparam>
	/// <exception cref="ArgumentException">Thrown when the value is less than the expected value.</exception>
	/// <returns>Returns true if the value is greater than or equal to the expected value, otherwise throws an exception.</returns>
	public static bool ThrowIfLessThan<T>([NotNull] this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfLessThan<T>(expected, nameof(value));

	/// <summary>
	/// Throws an exception if the provided value is less than the expected value.
	/// </summary>
	/// <param name="value">The value to check.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="message">The exception error message.</param>
	/// <typeparam name="T">The generic instance.</typeparam>
	/// <exception cref="ArgumentLessThanException">Thrown when the value is less than the expected value.</exception>
	/// <returns>Returns true if the value is greater than or equal to the expected value, otherwise throws an exception.</returns>
	public static bool ThrowIfLessThan<T>([NotNull] this int value, int expected, string message)
		where T : ArgumentException
		=> value < expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
			   : true;
}
