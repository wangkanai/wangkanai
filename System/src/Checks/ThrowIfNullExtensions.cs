// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

/// <summary>
/// Provides extension methods to throw an exception if a nullable value is null.
/// </summary>
[DebuggerStepThrough]
public static class CheckThrowIfNullExtensions
{
	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception if thrown if the value is null.</exception>
	public static bool ThrowIfNull([NotNull] this bool? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static byte ThrowIfNull([NotNull] this byte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static sbyte ThrowIfNull([NotNull] this sbyte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static short ThrowIfNull([NotNull] this short? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static ushort ThrowIfNull([NotNull] this ushort? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static int ThrowIfNull([NotNull] this int? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static uint ThrowIfNull([NotNull] this uint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static nint ThrowIfNull([NotNull] this nint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static nuint ThrowIfNull([NotNull] this nuint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static long ThrowIfNull([NotNull] this long? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static ulong ThrowIfNull([NotNull] this ulong? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static float ThrowIfNull([NotNull] this float? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static decimal ThrowIfNull([NotNull] this decimal? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static double ThrowIfNull([NotNull] this double? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static char ThrowIfNull([NotNull] this char? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static string ThrowIfNull([NotNull] this string? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	/// <summary>
	/// Throws an <exception cref="ArgumentNullException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <param name="message">The exception error message.</param>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentNullException">The exception is thrown if the value is null.</exception>
	public static string ThrowIfNull([NotNull] this string? value, string message)
		=> ThrowIfNull<ArgumentNullException>(value, message);

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static bool ThrowIfNull<T>([NotNull] this bool? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static sbyte ThrowIfNull<T>([NotNull] this sbyte? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static byte ThrowIfNull<T>([NotNull] this byte? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static short ThrowIfNull<T>([NotNull] this short? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static ushort ThrowIfNull<T>([NotNull] this ushort? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static int ThrowIfNull<T>([NotNull] this int? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static uint ThrowIfNull<T>([NotNull] this uint? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static nint ThrowIfNull<T>([NotNull] this nint? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static nuint ThrowIfNull<T>([NotNull] this nuint? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static long ThrowIfNull<T>([NotNull] this long? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static ulong ThrowIfNull<T>([NotNull] this ulong? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static float ThrowIfNull<T>([NotNull] this float? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static double ThrowIfNull<T>([NotNull] this double? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static decimal ThrowIfNull<T>([NotNull] this decimal? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static char ThrowIfNull<T>([NotNull] this char? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static string ThrowIfNull<T>([NotNull] this string? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	/// <summary>
	/// Throws an <exception cref="ArgumentException">exception</exception> if the value is null.
	/// </summary>
	/// <param name="value">The value to check for null.</param>
	/// <param name="message">The exception error message.</param>
	/// <typeparam name="T">The type of exception thrown if the value is null</typeparam>
	/// <returns>The value if it is not null.</returns>
	/// <exception cref="ArgumentException">The exception is thrown if the value is null.</exception>
	public static string ThrowIfNull<T>([NotNull] this string? value, string message)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message);
}
