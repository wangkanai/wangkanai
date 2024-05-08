// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

/// <summary>
/// Provides extension methods for throwing exceptions if a string is whitespace.
/// </summary>
[DebuggerStepThrough]
public static class ThrowIfWhitespaceExtensions
{
	/// <summary>
	/// Throws an exception if the specified string is null, empty, or consists only of whitespace characters.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <returns>The original string if it is not null, empty, or whitespace.</returns>
	public static string ThrowIfWhitespace([NotNull] this string? value)
		=> value.ThrowIfWhitespace<ArgumentWhitespaceException>();

	/// <summary>
	/// Throws an exception if the specified string is null, empty, or consists only of whitespace characters.
	/// </summary>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The exception error message.</param>
	/// <returns>The original string if it is not null, empty, or whitespace.</returns>
	public static string ThrowIfWhitespace([NotNull] this string? value, string message)
		=> value.ThrowIfWhitespace<ArgumentWhitespaceException>(message);

	/// <summary>
	/// Throws an exception if the specified string is null, empty, or consists only of whitespace characters.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The string to check.</param>
	/// <returns>The original string if it is not null, empty, or whitespace.</returns>
	public static string ThrowIfWhitespace<T>([NotNull] this string? value)
		where T : ArgumentException
		=> value.ThrowIfWhitespace<T>(nameof(value));

	/// <summary>
	/// Throws an exception if the specified string is null, empty, or consists only of whitespace characters.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The exception error message.</param>
	/// <returns>The original string if it is not null, empty, or whitespace.</returns>
	public static string ThrowIfWhitespace<T>([NotNull] this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfWhitespace<T>(message, nameof(value));

	/// <summary>
	/// Throws an exception if the specified string is null, empty, or consists only of whitespace characters.
	/// </summary>
	/// <typeparam name="T">The type of exception to throw.</typeparam>
	/// <param name="value">The string to check.</param>
	/// <param name="message">The exception error message.</param>
	/// <param name="paramName">The parameter name that thrown the exception.</param>
	/// <returns>The original string if it is not null, empty, or whitespace.</returns>
	public static string ThrowIfWhitespace<T>([NotNull] this string? value, string message, string paramName)
		where T : ArgumentException
		=> value!.IsWhiteSpace()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value!;
}
