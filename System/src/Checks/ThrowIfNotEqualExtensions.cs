// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>
/// Provides extension methods for throwing exceptions if values are not equal.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfNotEqualExtensions
{
	/// <summary>
	/// Throws an exception if the specified value is not equal to the expected value.
	/// </summary>
	/// <param name="value">The value to compare.</param>
	/// <param name="expected">The expected value.</param>
	/// <returns>True if the value is equal to the expected value; otherwise, throws an exception of type T.</returns>
	/// <exception cref="ArgumentNotEqualException">Thrown if the value is not equal to the expected value.</exception>
	public static bool ThrowIfNotEqual([NotNull] this int value, int expected)
		=> value.ThrowIfNotEqual<ArgumentNotEqualException>(expected, nameof(value));

	/// <summary>
	/// Throws an exception if the specified value is not equal to the expected value.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The value to compare.</param>
	/// <param name="expected">The expected value.</param>
	/// <returns>True if the value is equal to the expected value; otherwise, throws an exception of type T.</returns>
	/// <exception cref="ArgumentException">Thrown if the value is not equal to the expected value.</exception>
	public static bool ThrowIfNotEqual<T>([NotNull] this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfNotEqual<T>(expected, nameof(value));

	/// <summary>
	/// Throws an exception if the specified value is not equal to the expected value.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The value to compare.</param>
	/// <param name="expected">The expected value.</param>
	/// <param name="paramName">The name of the parameter being checked.</param>
	/// <returns>True if the value is equal to the expected value; otherwise, throws an exception of type T.</returns>
	/// <exception cref="ArgumentException">Thrown if the value is not equal to the expected value.</exception>
	public static bool ThrowIfNotEqual<T>([NotNull] this int value, int expected, string paramName)
		where T : ArgumentException
		=> value != expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
			   : true;
}
