// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

/// <summary>
/// Contains extension methods for throwing an exception if a value is equal to the expected value.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfEqualExtensions
{
	/// <summary>
	/// Throws an exception if the value is equal to the expected value.
	/// </summary>
	/// <param name="value">The value to compare.</param>
	/// <param name="expected">The expected value.</param>
	/// <returns>Returns false if the value is not equal to the expected value. Otherwise, throws an exception.</returns>
	public static bool ThrowIfEqual([NotNull] this int value, int expected)
		=> value.ThrowIfEqual<ArgumentEqualException>(expected, nameof(value));

	/// <summary>
	/// Throws an exception if the value is equal to the expected value.
	/// </summary>
	/// <param name="value">The value to compare.</param>
	/// <param name="expected">The expected value.</param>
	/// <returns>Returns false if the value is not equal to the expected value. Otherwise, throws an exception.</returns>
	public static bool ThrowIfEqual<T>([NotNull] this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfEqual<T>(expected, nameof(value));

	/// <summary>
	/// Throws an exception if the value is equal to the expected value.
	/// </summary>
	/// <param name="value">The value to compare.</param>
	/// <param name="expected">The expected value.</param>
	/// <typeparam name="T">The generic instance.</typeparam>
	/// <returns>Returns false if the value is not equal to the expected value. Otherwise, throws an exception.</returns>
	public static bool ThrowIfEqual<T>([NotNull] this int value, int expected, string paramName)
		where T : ArgumentException
		=> value == expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
			   : false;
}
