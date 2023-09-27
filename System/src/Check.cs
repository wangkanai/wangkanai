// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;
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
	[MemberNotNull]
	public static bool ThrowIfNull(this bool? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static sbyte ThrowIfNull(this sbyte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static byte ThrowIfNull(this byte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static ushort ThrowIfNull(this ushort? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static short ThrowIfNull(this short? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static int ThrowIfNull(this int? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static uint ThrowIfNull(this uint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static nint ThrowIfNull(this nint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static nuint ThrowIfNull(this nuint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static long ThrowIfNull(this long? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static ulong ThrowIfNull(this ulong? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static float ThrowIfNull(this float? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static decimal ThrowIfNull(this decimal? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static double ThrowIfNull(this double? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static char ThrowIfNull(this char? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static string ThrowIfNull(this string? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[MemberNotNull]
	public static string ThrowIfNull(this string? value, string message)
		=> ThrowIfNull<ArgumentNullException>(value, message);

	[MemberNotNull]
	public static bool ThrowIfNull<T>(this bool? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static sbyte ThrowIfNull<T>(this sbyte? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static byte ThrowIfNull<T>(this byte? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static short ThrowIfNull<T>(this short? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static ushort ThrowIfNull<T>(this ushort? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static int ThrowIfNull<T>(this int? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static uint ThrowIfNull<T>(this uint? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static nint ThrowIfNull<T>(this nint? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static nuint ThrowIfNull<T>(this nuint? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static long ThrowIfNull<T>(this long? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static ulong ThrowIfNull<T>(this ulong? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static float ThrowIfNull<T>(this float? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static double ThrowIfNull<T>(this double? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static decimal ThrowIfNull<T>(this decimal? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static char ThrowIfNull<T>(this char? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static string ThrowIfNull<T>(this string? value)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value));

	[MemberNotNull]
	public static string ThrowIfNull<T>(this string? value, string message)
		where T : ArgumentException
		=> value ?? throw CreateArgumentExceptionInstance<T>(nameof(value), message);

	#endregion

	#region Empty

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
	public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T>? value)
		=> !value.ThrowIfNull().Any()
			   ? throw CreateArgumentExceptionInstance<ArgumentEmptyException>(nameof(value))
			   : value;

	public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T>? value, string message)
		=> !value.ThrowIfNull().Any()
			   ? throw CreateArgumentExceptionInstance<ArgumentEmptyException>(nameof(value), message)
			   : value;

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>(this IEnumerable<TType>? value)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw CreateArgumentExceptionInstance<TException>(nameof(value))
			       : value;
	}

	public static IEnumerable<TType> ThrowIfEmpty<TException, TType>(this IEnumerable<TType>? value, string message)
		where TException : ArgumentException
	{
		value.ThrowIfNull<TException>();
		return !value.Any()
			       ? throw CreateArgumentExceptionInstance<TException>(nameof(value), message)
			       : value;
	}
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8604 // Possible null reference argument.

	#endregion

	// Throw is value string is null or empty

	public static string ThrowIfNullOrEmpty(this string? value)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>();

	public static string ThrowIfNullOrEmpty(this string? value, string message)
		=> value.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>(message);

	public static string ThrowIfNullOrEmpty(this string? value, string message, [InvokerParameterName] string paramName)
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

	public static string ThrowIfNullOrEmpty<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw CreateArgumentExceptionInstance<T>(paramName, message);
		return value!;
	}

	// Throw if value string is empty

	public static string ThrowIfEmpty(this string? value)
		=> value.ThrowIfEmpty<ArgumentEmptyException>();

	public static string ThrowIfEmpty(this string? value, string message)
		=> value.ThrowIfEmpty<ArgumentEmptyException>(message);

	public static string ThrowIfEmpty(this string? value, string message, [InvokerParameterName] string paramName)
		=> value.ThrowIfEmpty<ArgumentEmptyException>(message, paramName);

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

	public static string ThrowIfEmpty<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw CreateArgumentExceptionInstance<T>(paramName, message)
			   : value!;

	// Throw if value string is null or whitespace

	public static string ThrowIfNullOrWhitespace(this string? value)
		=> value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>();

	public static string ThrowIfNullOrWhitespace(this string? value, string message)
		=> value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(message);

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

	// Throw if value string is whitespace

	public static string ThrowIfWhitespace(this string? value)
		=> value.ThrowIfWhitespace<ArgumentWhitespaceException>();

	public static string ThrowIfWhitespace(this string? value, string message)
		=> value.ThrowIfWhitespace<ArgumentWhitespaceException>(message);

	public static string ThrowIfWhitespace<T>(this string? value)
		where T : ArgumentException
		=> value.ThrowIfWhitespace<T>(nameof(value));

	public static string ThrowIfWhitespace<T>(this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfWhitespace<T>(message, nameof(value));

	public static string ThrowIfWhitespace<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value.IsWhiteSpace()
			   ? throw CreateArgumentExceptionInstance<T>(paramName, message)
			   : value!;

	// Throw if value object is null

	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
		=> value ?? throw CreateGenericExceptionInstance<T>(nameof(value));

	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
		=> value ?? throw CreateGenericExceptionInstance<T>(nameof(value), message);

	public static T ThrowIfNull<T>(this T value)
		=> value ?? throw CreateArgumentExceptionInstance<ArgumentNullException>(nameof(value));

	public static T ThrowIfNull<T>(this T value, string message)
		=> value ?? throw CreateArgumentExceptionInstance<ArgumentNullException>(nameof(value), message);

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

	public static bool ThrowIfLessThan(this int value, int expected, string message)
		=> value.ThrowIfLessThan<ArgumentLessThanException>(expected, message);

	public static bool ThrowIfLessThan<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfLessThan<T>(expected, nameof(value));

	public static bool ThrowIfLessThan<T>(this int value, int expected, string message)
		where T : ArgumentException
		=> value < expected
			   ? throw CreateArgumentExceptionInstance<T>(message)
			   : true;

	// Throw if value is more than expected

	public static bool ThrowIfMoreThan(this int value, int expected)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected);

	public static bool ThrowIfMoreThan(this int value, int expected, string message)
		=> value.ThrowIfMoreThan<ArgumentMoreThanException>(expected, message);

	public static bool ThrowIfMoreThan<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfMoreThan<T>(expected, nameof(value));

	public static bool ThrowIfMoreThan<T>(this int value, int expected, string message)
		where T : ArgumentException
		=> value > expected
			   ? throw CreateArgumentExceptionInstance<T>(message)
			   : true;

	// Throw if value is negative

	public static int ThrowIfNegative(this int value)
		=> value.ThrowIfNegative<ArgumentNegativeException>();

	public static int ThrowIfNegative(this int value, string message)
		=> value.ThrowIfNegative<ArgumentNegativeException>(message);

	public static int ThrowIfNegative<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfNegative<T>(nameof(value));

	public static int ThrowIfNegative<T>(this int value, string message)
		where T : ArgumentException
		=> value < 0
			   ? throw CreateArgumentExceptionInstance<T>(nameof(value), message)
			   : value;

	// Throw if value is positive

	public static int ThrowIfPositive(this int value)
		=> value.ThrowIfPositive<ArgumentPositiveException>();

	public static int ThrowIfPositive(this int value, string message)
		=> value.ThrowIfPositive<ArgumentPositiveException>(message);

	public static int ThrowIfPositive<T>(this int value)
		where T : ArgumentException
		=> value.ThrowIfPositive<T>(nameof(value));

	public static int ThrowIfPositive<T>(this int value, string message)
		where T : ArgumentException
		=> value > 0
			   ? throw CreateArgumentExceptionInstance<T>(nameof(value), message)
			   : value;

	// Throw if value is zero

	public static int ThrowIfZero(this int value)
		=> value.ThrowIfZero<ArgumentZeroException>();

	public static int ThrowIfZero(this int value, string message)
		=> value.ThrowIfZero<ArgumentZeroException>(message);

	public static int ThrowIfZero<T>(this int value)
		where T : ArgumentException
		=> value == 0
			   ? throw CreateArgumentExceptionInstance<T>(nameof(value))
			   : value;

	public static int ThrowIfZero<T>(this int value, string message)
		where T : ArgumentException
		=> value == 0
			   ? throw CreateArgumentExceptionInstance<T>(nameof(value), message)
			   : value;

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

	internal static T CreateGenericExceptionInstance<T>([InvokerParameterName] string paramName)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), paramName) as T)!;

	internal static T CreateGenericExceptionInstance<T>([InvokerParameterName] string paramName, string message)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), paramName, message) as T)!;

	internal static T CreateArgumentExceptionInstance<T>([InvokerParameterName] string paramName)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), paramName) as T)!;

	internal static T CreateArgumentExceptionInstance<T>([InvokerParameterName] string paramName, string message)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), paramName, message) as T)!;
	

	#endregion
}