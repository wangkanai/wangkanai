// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

/// <summary>
/// Contains extension methods for throwing exceptions when a string is empty.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfEmptyStringExtensions
{
	/// <summary>
	/// Throws an exception if the string is empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <returns>The input string if it is not empty.</returns>
	public static string ThrowIfEmpty([NotNull] this string? value)
		=> value.ThrowIfNull().ThrowIfEmpty<ArgumentEmptyException>();

	/// <summary>
	/// Throws an exception if the string is empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The exception error message.</param>
	/// <returns>The input string if it is not empty.</returns>
	/// <exception cref="ArgumentEmptyException">The string is empty.</exception>
	public static string ThrowIfEmpty([NotNull] this string? value, string message)
		=> value.ThrowIfNull().ThrowIfEmpty<ArgumentEmptyException>(message);

	/// <summary>
	/// Throws an exception if the string is empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <returns>The input string if it is not empty.</returns>
	public static string ThrowIfEmpty<T>([NotNull] this string? value)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value))
			   : value!;

	/// <summary>
	/// Throws an exception if the string is empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The exception error message.</param>
	/// <returns>The input string if it is not empty.</returns>
	/// <typeparam name="T">The type of the exception to throw.</typeparam>
	/// <exception cref="ArgumentException">The string is empty.</exception>
	public static string ThrowIfEmpty<T>([NotNull] this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value!;

	/// <summary>
	/// Throws an exception if the string is empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The exception error meesage</param>
	/// <param name="paramName">The name of the parameter.</param>
	/// <returns>The input string if it is not empty.</returns>
	/// <exception cref="ArgumentException">The string is empty.</exception>
	public static string ThrowIfEmpty<T>([NotNull] this string? value, string message, string paramName)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value!;
}
