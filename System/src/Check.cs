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
	[DebuggerStepThrough]
	public static bool? ThrowIfNull(this bool? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static sbyte? ThrowIfNull(this sbyte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static byte? ThrowIfNull(this byte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static ushort? ThrowIfNull(this ushort? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static short? ThrowIfNull(this short? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static int? ThrowIfNull(this int? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static uint? ThrowIfNull(this uint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static nint? ThrowIfNull(this nint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static nuint? ThrowIfNull(this nuint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static long? ThrowIfNull(this long? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static ulong? ThrowIfNull(this ulong? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static float? ThrowIfNull(this float? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static decimal? ThrowIfNull(this decimal? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static double? ThrowIfNull(this double? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static char? ThrowIfNull(this char? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static string ThrowIfNull(this string? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[DebuggerStepThrough]
	public static string ThrowIfNull(this string? value, string message)
		=> ThrowIfNull<ArgumentNullException>(value, message);

	[DebuggerStepThrough]
	public static bool? ThrowIfNull<T>(this bool? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static sbyte? ThrowIfNull<T>(this sbyte? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static byte? ThrowIfNull<T>(this byte? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static short? ThrowIfNull<T>(this short? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static ushort? ThrowIfNull<T>(this ushort? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static int? ThrowIfNull<T>(this int? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static uint? ThrowIfNull<T>(this uint? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static nint? ThrowIfNull<T>(this nint? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static nuint? ThrowIfNull<T>(this nuint? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static long? ThrowIfNull<T>(this long? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static ulong? ThrowIfNull<T>(this ulong? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static float? ThrowIfNull<T>(this float? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static double? ThrowIfNull<T>(this double? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static decimal? ThrowIfNull<T>(this decimal? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static char? ThrowIfNull<T>(this char? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static string ThrowIfNull<T>(this string? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static string ThrowIfNull<T>(this string? value, string message)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(message);

	#endregion

	// Throw is value string is null or empty
	[DebuggerStepThrough]
	public static string ThrowIfNullOrEmpty(this string? value)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>();

	[DebuggerStepThrough]
	public static string ThrowIfNullOrEmpty(this string? value, string message)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message);

	[DebuggerStepThrough]
	public static string ThrowIfNullOrEmpty(this string? value, string message, string paramName)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message, paramName);

	[DebuggerStepThrough]
	public static string ThrowIfNullOrEmpty<T>(this string? value)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateExceptionInstance<T>(nameof(value));
		return value!;
	}

	[DebuggerStepThrough]
	public static string ThrowIfNullOrEmpty<T>(this string? value, string message)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateExceptionInstance<T>(message);
		return value!;
	}

	[DebuggerStepThrough]
	public static string ThrowIfNullOrEmpty<T>(this string? value, string message, string paramName)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateExceptionInstance<T>(message, paramName);
		return value!;
	}

	// Throw if value string is null or whitespace
	[DebuggerStepThrough]
	public static string ThrowIfNullOrWhitespace(this string? value)
		=> ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(value);

	[DebuggerStepThrough]
	public static string ThrowIfNullOrWhitespace(this string? value, string message)
		=> ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(value, message);

	[DebuggerStepThrough]
	public static string ThrowIfNullOrWhitespace<T>(this string? value)
		where T : ArgumentException
		=> value.ThrowIfNullOrWhitespace<T>(nameof(value));

	[DebuggerStepThrough]
	public static string ThrowIfNullOrWhitespace<T>(this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfNullOrWhitespace<T>(message, nameof(value));

	[DebuggerStepThrough]
	public static string ThrowIfNullOrWhitespace<T>(this string? value, string message, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value.IsNullOrWhiteSpace()
			   ? throw CreateExceptionInstance<T>(message, parameterName)
			   : value!;

	// Throw if value object is null
	[DebuggerStepThrough]
	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[DebuggerStepThrough]
	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(message);

	[DebuggerStepThrough]
	public static T ThrowIfNull<T>(this T value)
		=> value ?? throw CreateExceptionInstance<ArgumentNullException>(nameof(value));

	[DebuggerStepThrough]
	public static T ThrowIfNull<T>(this T value, string message)
		=> value ?? throw CreateExceptionInstance<ArgumentNullException>(message);


	// Throw if value is not equal to expected
	[DebuggerStepThrough]
	public static bool ThrowIfEqual(this int value, int expected)
		=> value.ThrowIfEqual<ArgumentEqualException>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfEqual<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfEqual<T>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfEqual<T>(this int value, int expected, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value == expected
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : false;
	
	// Throw if value is not equal to expected
	[DebuggerStepThrough]
	public static bool ThrowIfNotEqual(this int value, int expected)
		=> value.ThrowIfNotEqual<ArgumentNotEqualException>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfNotEqual<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfNotEqual<T>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfNotEqual<T>(this int value, int expected, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value != expected
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : true;

	// Throw if value is less than expected
	[DebuggerStepThrough]
	public static bool ThrowIfLessThan(this int value, int expected)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfLessThan(this int value, int expected, [InvokerParameterName] string parameterName)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, parameterName);

	[DebuggerStepThrough]
	public static bool ThrowIfLessThan<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfLessThan<T>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfLessThan<T>(this int value, int expected, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value < expected
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : true;

	// Throw if value is more than expected
	[DebuggerStepThrough]
	public static bool ThrowIfMoreThan(this int value, int expected)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfMoreThan(this int value, int expected, [InvokerParameterName] string parameterName)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, parameterName);

	[DebuggerStepThrough]
	public static bool ThrowIfMoreThan<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfMoreThan<T>(expected, nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfMoreThan<T>(this int value, int expected, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value > expected
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : true;

	// Throw if value is negative
	[DebuggerStepThrough]
	public static int ThrowIfNegative(this int value)
		=> value.ThrowIfNegative<ArgumentNegativeException>(nameof(value));

	[DebuggerStepThrough]
	public static int ThrowIfNegative(this int value, [InvokerParameterName] string parameterName)
		=> value.ThrowIfNegative<ArgumentNegativeException>(parameterName);

	[DebuggerStepThrough]
	public static int ThrowIfNegative<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfNegative<T>(nameof(value));

	[DebuggerStepThrough]
	public static int ThrowIfNegative<T>(this int value, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value < 0
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : value;

	// Throw if value is positive
	[DebuggerStepThrough]
	public static int ThrowIfPositive(this int value)
		=> value.ThrowIfPositive<ArgumentPositiveException>(nameof(value));

	[DebuggerStepThrough]
	public static int ThrowIfPositive(this int value, [InvokerParameterName] string parameterName)
		=> value.ThrowIfPositive<ArgumentPositiveException>(parameterName);

	[DebuggerStepThrough]
	public static int ThrowIfPositive<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfPositive<T>(nameof(value));

	[DebuggerStepThrough]
	public static int ThrowIfPositive<T>(this int value, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value < 0
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : value;

	// Throw if value is zero
	[DebuggerStepThrough]
	public static bool ThrowIfZero(this int value)
		=> value.ThrowIfZero<ArgumentZeroException>(nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfZero(this int value, [InvokerParameterName] string parameterName)
		=> value.ThrowIfZero<ArgumentZeroException>(parameterName);

	[DebuggerStepThrough]
	public static bool ThrowIfZero<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfZero<T>(nameof(value));

	[DebuggerStepThrough]
	public static bool ThrowIfZero<T>(this int value, [InvokerParameterName] string parameterName)
		where T : ArgumentException
		=> value == 0
			   ? throw CreateExceptionInstance<T>(parameterName)
			   : true;

	#region IfNull

	[DebuggerStepThrough]
	public static bool TrueIfNull<T>(this T? value)
		=> value is null;

	[DebuggerStepThrough]
	public static bool FalseIfNull<T>(this T? value)
		=> !value.TrueIfNull();

	#endregion

	[DebuggerStepThrough]
	public static T ThrowIfOutOfRange<T>(this T index, [NonNegativeValue] T lower, [NonNegativeValue] T upper)
		where T : IBinaryInteger<T>
	{
		if (index < lower || index >= upper)
			throw new ArgumentOutOfRangeException(nameof(index));

		return index;
	}

	#region Instance

	private static T CreateExceptionInstance<T>(string name)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), name) as T)!;

	private static T CreateExceptionInstance<T>(string name, string paramName)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), name, paramName) as T)!;

	#endregion
}