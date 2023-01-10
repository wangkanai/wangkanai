// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Numerics;

using Wangkanai.Exceptions;
using Wangkanai.Extensions;
using Wangkanai.Resources;

#nullable enable

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
	#region ThrowIfNull

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool? ThrowIfNull(this bool? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool? ThrowIfNull<T>(this bool? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static sbyte? ThrowIfNull(this sbyte? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static sbyte? ThrowIfNull<T>(this sbyte? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static byte? ThrowIfNull(this byte? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static byte? ThrowIfNull<T>(this byte? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static short? ThrowIfNull(this short? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static short? ThrowIfNull<T>(this short? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ushort? ThrowIfNull(this ushort? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ushort? ThrowIfNull<T>(this ushort? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static int? ThrowIfNull(this int? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static int? ThrowIfNull<T>(this int? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static uint? ThrowIfNull(this uint? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static uint? ThrowIfNull<T>(this uint? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nint? ThrowIfNull(this nint? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nint? ThrowIfNull<T>(this nint? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nuint? ThrowIfNull(this nuint? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nuint? ThrowIfNull<T>(this nuint? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static long? ThrowIfNull(this long? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static long? ThrowIfNull<T>(this long? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ulong? ThrowIfNull(this ulong? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ulong? ThrowIfNull<T>(this ulong? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static float? ThrowIfNull(this float? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static float? ThrowIfNull<T>(this float? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static double? ThrowIfNull(this double? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static double? ThrowIfNull<T>(this double? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static decimal? ThrowIfNull(this decimal? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static decimal? ThrowIfNull<T>(this decimal? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static char? ThrowIfNull(this char? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static char? ThrowIfNull<T>(this char? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string ThrowIfNull(this string? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	public static string ThrowIfNull(this string? value, string message)
	{
		return ThrowIfNull<ArgumentNullException>(value, message);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string ThrowIfNull<T>(this string? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string ThrowIfNull<T>(this string? value, string message)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(message);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
	{
		return value ?? throw CreateExceptionInstance<T>(message);
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static T ThrowIfNull<T>(this T value)
	{
		return value ?? throw CreateExceptionInstance<ArgumentNullException>(nameof(value));
	}

	private static T CreateExceptionInstance<T>(string name)
		where T : Exception
	{
		return (Activator.CreateInstance(typeof(T), name) as T)!;
	}

	#endregion

	#region IfNull

	public static bool TrueIfNull<T>(this T? value)
	{
		return value is null;
	}

	public static bool FalseIfNull<T>(this T? value)
	{
		return !value.TrueIfNull();
	}

	public static T ReturnIfNotNull<T>(this T? value)
	{
		return value ?? value!.ThrowIfNull<T>();
	}

	#endregion

#if NET7_0_OR_GREATER
	public static T ThrowIfOutOfRange<T>(this T index, [NonNegativeValue] T lower, [NonNegativeValue] T upper)
		where T : IBinaryInteger<T>
	{
		if (index < lower || index >= upper)
			throw new ArgumentOutOfRangeException(nameof(index));

		return index;
	}
#else
	public static int ThrowIfOutOfRange(this int index, [NonNegativeValue] int lower, [NonNegativeValue] int upper)
	{
		if (index < lower || index >= upper)
			throw new ArgumentOutOfRangeException(nameof(index));

		return index;
	}
#endif
// #region prepare depreciate
//
// [Deprecate(nameof(ThrowIfNull))]
// [Obsolete]
// [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
// public static T NotNull<T>([CanBeNull] T value)
// 	=> NotNull(value, nameof(value));
//
// [Obsolete]
// [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
// internal static T NotNull<T>([CanBeNull] T value, [InvokerParameterName] string parameterName)
// 	=> value is null
// 		   ? throw new ArgumentNullException(parameterName)
// 		   : value;
//
// #endregion
	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string NotNullOrEmpty(string value)
	{
		return NotNullOrEmpty(value, nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	private static string NotNullOrEmpty(string value, [InvokerParameterName] string parameterName)
	{
		return value.IsNullOrEmpty()
			       ? throw new ArgumentNullOrEmptyException(parameterName)
			       : value;
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotNullOrEmpty<T>(this IEnumerable<T> value)
	{
		return NotNullOrEmpty(value, nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	internal static bool NotNullOrEmpty<T>(this IEnumerable<T> value, [InvokerParameterName] string parameterName)
	{
		return value.IsNullOrEmpty()
			       ? throw new ArgumentNullOrEmptyException(parameterName)
			       : true;
	}


	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotEqual(this int value, int expected)
	{
		return value.NotEqual(expected, nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotEqual(this int value, int expected, ArgumentException exception)
	{
		return value != expected
			       ? throw exception
			       : true;
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	internal static bool NotEqual(this int value, int expected, [InvokerParameterName] string parameterName)
	{
		return value != expected
			       ? throw new ArgumentEqualException(parameterName)
			       : true;
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotLessThan(this int value, int expected)
	{
		return NotLessThan(value, expected, nameof(value));
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotLessThan(this int value, int expected, [InvokerParameterName] string parameterName)
	{
		return value < expected
			       ? throw new ArgumentLessThanException(parameterName)
			       : true;
	}

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotMoreThan(this int value, int expected)
	{
		return NotMoreThan(value, expected, nameof(value));
	}


	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	internal static bool NotMoreThan(this int value, int expected, [InvokerParameterName] [NotNull] string parameterName)
	{
		return value > expected
			       ? throw new ArgumentMoreThanException(parameterName)
			       : true;
	}
}