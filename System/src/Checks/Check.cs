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
			throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
		return value!;
	}

	public static string ThrowIfNullOrEmpty<T>(this string? value, string message)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message);
		return value!;
	}

	public static string ThrowIfNullOrEmpty<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
	{
		if (value.IsNullOrEmpty())
			throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message);
		return value!;
	}


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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value!;

	// Throw if value object is null
	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
		=> value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value));

	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
		=> value ?? throw ExceptionActivator.CreateGenericInstance<T>(nameof(value), message);

	public static T ThrowIfNull<T>(this T value)
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value));

	public static T ThrowIfNull<T>(this T value, string message)
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<ArgumentNullException>(nameof(value), message);

	// Throw if value is not equal to expected
	public static bool ThrowIfEqual(this int value, int expected)
		=> value.ThrowIfEqual<ArgumentEqualException>(expected, nameof(value));

	public static bool ThrowIfEqual<T>(this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfEqual<T>(expected, nameof(value));

	public static bool ThrowIfEqual<T>(this int value, int expected, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value == expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(message)
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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
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
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value;

	// Throw if value is zero
	public static int ThrowIfZero(this int value)
		=> value.ThrowIfZero<ArgumentZeroException>();

	public static int ThrowIfZero(this int value, string message)
		=> value.ThrowIfZero<ArgumentZeroException>(message);

	public static int ThrowIfZero<T>(this int value)
		where T : ArgumentException
		=> value == 0
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value))
			   : value;

	public static int ThrowIfZero<T>(this int value, string message)
		where T : ArgumentException
		=> value == 0
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
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
}
