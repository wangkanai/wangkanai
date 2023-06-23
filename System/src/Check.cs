// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Numerics;

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

#nullable enable

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
	#region Throw

	// Throw if value is null (all possible types)
	public static bool? ThrowIfNull(this bool? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static sbyte? ThrowIfNull(this sbyte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static byte? ThrowIfNull(this byte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static ushort? ThrowIfNull(this ushort? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static short? ThrowIfNull(this short? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static int? ThrowIfNull(this int? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static uint? ThrowIfNull(this uint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static nint? ThrowIfNull(this nint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static nuint? ThrowIfNull(this nuint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static long? ThrowIfNull(this long? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static ulong? ThrowIfNull(this ulong? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static float? ThrowIfNull(this float? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static decimal? ThrowIfNull(this decimal? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static double? ThrowIfNull(this double? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static char? ThrowIfNull(this char? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static string ThrowIfNull(this string? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static string ThrowIfNull(this string? value, string message)
		=> ThrowIfNull<ArgumentNullException>(value, message);

	public static bool? ThrowIfNull<T>(this bool? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static sbyte? ThrowIfNull<T>(this sbyte? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static byte? ThrowIfNull<T>(this byte? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static short? ThrowIfNull<T>(this short? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static ushort? ThrowIfNull<T>(this ushort? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static int? ThrowIfNull<T>(this int? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static uint? ThrowIfNull<T>(this uint? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static nint? ThrowIfNull<T>(this nint? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static nuint? ThrowIfNull<T>(this nuint? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static long? ThrowIfNull<T>(this long? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static ulong? ThrowIfNull<T>(this ulong? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static float? ThrowIfNull<T>(this float? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static double? ThrowIfNull<T>(this double? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static decimal? ThrowIfNull<T>(this decimal? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static char? ThrowIfNull<T>(this char? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static string ThrowIfNull<T>(this string? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	public static string ThrowIfNull<T>(this string? value, string message)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value), message);

	#endregion

	#region Empty

	public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T>? value)
		where T : struct
		=> !value.ThrowIfNull().Any()
			   ? throw CreateArgumentExceptionInstance<ArgumentEmptyException>(nameof(value))
			   : value;

	public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T>? value, string message)
		where T : struct
		=> !value.ThrowIfNull().Any()
			   ? throw CreateArgumentExceptionInstance<ArgumentEmptyException>(nameof(value), message)
			   : value;

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>(this IEnumerable<TType>? value)
		where TType : struct
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw CreateArgumentExceptionInstance<TException>(nameof(value))
			       : value;
	}

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>(this IEnumerable<TType>? value, string message)
		where TType : struct
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw CreateArgumentExceptionInstance<TException>(nameof(value), message)
			       : value;
	}

	#endregion

	// Throw is value string is null or empty

	public static string ThrowIfNullOrEmpty(this string? value)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>();

	public static string ThrowIfNullOrEmpty(this string? value, string message)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message);

	public static string ThrowIfNullOrEmpty(this string? value, string message, string paramName)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message, paramName);

	public static string ThrowIfNullOrEmpty<T>(this string? value)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateArgumentExceptionInstance<T>(nameof(value));
		return value!;
	}

	public static string ThrowIfNullOrEmpty<T>(this string? value, string message)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateArgumentExceptionInstance<T>(nameof(value), message);
		return value!;
	}

	public static string ThrowIfNullOrEmpty<T>(this string? value, string message, string paramName)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateArgumentExceptionInstance<T>(paramName, message);
		return value!;
	}

	// Throw if value string is empty

	public static string ThrowIfEmpty(this string? value)
		=> ThrowIfEmpty<ArgumentEmptyException>(value);

	public static string ThrowIfEmpty(this string? value, string message)
		=> ThrowIfEmpty<ArgumentEmptyException>(value, message);

	public static string ThrowIfEmpty(this string? value, string message, string paramName)
		=> ThrowIfEmpty<ArgumentEmptyException>(value, message, paramName);

	public static string ThrowIfEmpty<T>(this string? value)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw CreateArgumentExceptionInstance<T>(nameof(value))
			   : value!;

	public static string ThrowIfEmpty<T>(this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw CreateArgumentExceptionInstance<T>(nameof(value), message)
			   : value!;

	public static string ThrowIfEmpty<T>(this string? value, string message, string paramName)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw CreateArgumentExceptionInstance<T>(paramName, message)
			   : value!;

	// Throw if value string is null or whitespace

	public static string ThrowIfNullOrWhitespace(this string? value)
		=> ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(value);

	public static string ThrowIfNullOrWhitespace(this string? value, string message)
		=> ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(value, message);

	public static string ThrowIfNullOrWhitespace<T>(this string? value)
		where T : ArgumentException
		=> value.ThrowIfNullOrWhitespace<T>(nameof(value));

	public static string ThrowIfNullOrWhitespace<T>(this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfNullOrWhitespace<T>(message, nameof(value));

	public static string ThrowIfNullOrWhitespace<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value.IsNullOrWhiteSpace()
			   ? throw CreateArgumentExceptionInstance<T>(paramName, message)
			   : value!;

	// Throw if value object is null

	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
		=> value ?? throw CreateGenericExceptionInstance<T>();

	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
		=> value ?? throw CreateGenericExceptionInstance<T>(message);

	public static T ThrowIfNull<T>(this T value)
		=> value ?? throw CreateArgumentExceptionInstance<ArgumentNullException>(nameof(value));

	public static T ThrowIfNull<T>(this T value, string message)
		=> value ?? throw CreateArgumentExceptionInstance<ArgumentNullException>(message);


	// Throw if value is not equal to expected
	public static bool ThrowIfEqual(this int value, int expected)
		=> value.ThrowIfEqual<ArgumentEqualException>(expected, nameof(value));

	public static bool ThrowIfEqual<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfEqual<T>(expected, nameof(value));

	public static bool ThrowIfEqual<T>(this int value, int expected, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value == expected
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : false;

	// Throw if value is not equal to expected

	public static bool ThrowIfNotEqual(this int value, int expected)
		=> value.ThrowIfNotEqual<ArgumentNotEqualException>(expected, nameof(value));

	public static bool ThrowIfNotEqual<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfNotEqual<T>(expected, nameof(value));

	public static bool ThrowIfNotEqual<T>(this int value, int expected, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value != expected
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : true;

	// Throw if value is less than expected

	public static bool ThrowIfLessThan(this int value, int expected)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, nameof(value));

	public static bool ThrowIfLessThan(this int value, int expected, [InvokerParameterName] string paramName)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, paramName);

	public static bool ThrowIfLessThan<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfLessThan<T>(expected, nameof(value));

	public static bool ThrowIfLessThan<T>(this int value, int expected, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value < expected
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : true;

	// Throw if value is more than expected

	public static bool ThrowIfMoreThan(this int value, int expected)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, nameof(value));

	public static bool ThrowIfMoreThan(this int value, int expected, [InvokerParameterName] string paramName)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, paramName);

	public static bool ThrowIfMoreThan<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfMoreThan<T>(expected, nameof(value));

	public static bool ThrowIfMoreThan<T>(this int value, int expected, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value > expected
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : true;

	// Throw if value is negative

	public static int ThrowIfNegative(this int value)
		=> value.ThrowIfNegative<ArgumentNegativeException>(nameof(value));

	public static int ThrowIfNegative(this int value, [InvokerParameterName] string paramName)
		=> value.ThrowIfNegative<ArgumentNegativeException>(paramName);

	public static int ThrowIfNegative<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfNegative<T>(nameof(value));

	public static int ThrowIfNegative<T>(this int value, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value < 0
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : value;

	// Throw if value is positive

	public static int ThrowIfPositive(this int value)
		=> value.ThrowIfPositive<ArgumentPositiveException>(nameof(value));

	public static int ThrowIfPositive(this int value, [InvokerParameterName] string paramName)
		=> value.ThrowIfPositive<ArgumentPositiveException>(paramName);

	public static int ThrowIfPositive<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfPositive<T>(nameof(value));

	public static int ThrowIfPositive<T>(this int value, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value < 0
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : value;

	// Throw if value is zero

	public static bool ThrowIfZero(this int value)
		=> value.ThrowIfZero<ArgumentZeroException>(nameof(value));

	public static bool ThrowIfZero(this int value, [InvokerParameterName] string paramName)
		=> value.ThrowIfZero<ArgumentZeroException>(paramName);

	public static bool ThrowIfZero<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfZero<T>(nameof(value));

	public static bool ThrowIfZero<T>(this int value, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value == 0
			   ? throw CreateArgumentExceptionInstance<T>(paramName)
			   : true;

	#region IfNull

	public static bool TrueIfNull<T>(this T? value)
		=> value is null;

	public static bool FalseIfNull<T>(this T? value)
		=> !value.TrueIfNull();

	#endregion

	public static T ThrowIfOutOfRange<T>(this T index, [NonNegativeValue] T lower, [NonNegativeValue] T upper)
		where T : IBinaryInteger<T>
	{
		if (index < lower || index >= upper)
			throw new ArgumentOutOfRangeException(nameof(index));

		return index;
	}

	#region Instance

	private static T CreateGenericExceptionInstance<T>()
		where T : Exception
		=> (Activator.CreateInstance(typeof(T)) as T)!;

	private static T CreateGenericExceptionInstance<T>(string message)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), message) as T)!;

	private static T CreateGenericExceptionInstance<T>(string message, Exception? innerException)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), message, innerException) as T)!;

	private static T CreateArgumentExceptionInstance<T>(string paramName)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), paramName) as T)!;

	private static T CreateArgumentExceptionInstance<T>(string paramName, string message)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), message, paramName) as T)!;

	private static T CreateArgumentExceptionInstance<T>(string paramName, string message, Exception? innerException)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), message, paramName, innerException) as T)!;

	#endregion
}