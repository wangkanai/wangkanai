// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

/// <summary>
/// Provides extension methods to throw an exception if a string is null or empty.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfNullOrEmptyExtensions
{
	/// <summary>
	/// Throws an exception if the given string is null or empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <returns>The original string if it is not null or empty.</returns>
	public static string ThrowIfNullOrEmpty([NotNull] this string? value)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>();

	/// <summary>
	/// Throws an exception if the given string is null or empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The custom error message.</param>
	/// <returns>The original string if it is not null or empty.</returns>
	public static string ThrowIfNullOrEmpty([NotNull] this string? value, string message)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message);

	/// <summary>
	/// Throws an exception if the given string is null or empty.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The custom error message.</param>
	/// <param name="paramName">The parameter name that thrown the exception.</param>
	/// <returns>The original string if it is not null or empty.</returns>
	public static string ThrowIfNullOrEmpty([NotNull] this string? value, string message, string paramName)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message, paramName);

	/// <summary>
	/// Throws an exception if the given string is null or empty.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The string to check.</param>
	/// <returns>The original string if it is not null or empty.</returns>
	public static string ThrowIfNullOrEmpty<T>([NotNull] this string? value)
		where T : ArgumentException
		=> value!.IsNullOrEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value))
			   : value;

	/// <summary>
	/// Throws an exception if the given string is null or empty.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The custom error message.</param>
	/// <returns>The original string if it is not null or empty.</returns>
	public static string ThrowIfNullOrEmpty<T>([NotNull] this string? value, string message)
		where T : ArgumentException
		=> value!.IsNullOrEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value;

	/// <summary>
	/// Throws an exception if the given string is null or empty.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The custom error message.</param>
	/// <param name="paramName">The parameter name that thrown the exception.</param>
	/// <returns>The original string if it is not null or empty.</returns>
	public static string ThrowIfNullOrEmpty<T>([NotNull] this string? value, string message, string paramName)
		where T : ArgumentException
		=> value!.IsNullOrEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value;
}
