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
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool? ThrowIfNull<T>(this bool? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static sbyte? ThrowIfNull(this sbyte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static sbyte? ThrowIfNull<T>(this sbyte? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static byte? ThrowIfNull(this byte? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static byte? ThrowIfNull<T>(this byte? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static short? ThrowIfNull(this short? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static short? ThrowIfNull<T>(this short? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ushort? ThrowIfNull(this ushort? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ushort? ThrowIfNull<T>(this ushort? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static int? ThrowIfNull(this int? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static int? ThrowIfNull<T>(this int? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static uint? ThrowIfNull(this uint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static uint? ThrowIfNull<T>(this uint? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nint? ThrowIfNull(this nint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nint? ThrowIfNull<T>(this nint? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nuint? ThrowIfNull(this nuint? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static nuint? ThrowIfNull<T>(this nuint? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static long? ThrowIfNull(this long? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static long? ThrowIfNull<T>(this long? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ulong? ThrowIfNull(this ulong? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static ulong? ThrowIfNull<T>(this ulong? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static float? ThrowIfNull(this float? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static float? ThrowIfNull<T>(this float? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static double? ThrowIfNull(this double? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static double? ThrowIfNull<T>(this double? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static decimal? ThrowIfNull(this decimal? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static decimal? ThrowIfNull<T>(this decimal? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static char? ThrowIfNull(this char? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static char? ThrowIfNull<T>(this char? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string ThrowIfNull(this string? value)
		=> ThrowIfNull<ArgumentNullException>(value);

	public static string ThrowIfNull(this string? value, string message)
		=> ThrowIfNull<ArgumentNullException>(value, message);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string ThrowIfNull<T>(this string? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static string ThrowIfNull<T>(this string? value, string message)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(message);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static object ThrowIfNull<T>(this object? value)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static object ThrowIfNull<T>(this object? value, string message)
		where T : Exception
		=> value ?? throw CreateExceptionInstance<T>(message);

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static T ThrowIfNull<T>(this T value)
		=> value ?? throw CreateExceptionInstance<ArgumentNullException>(nameof(value));

	private static T CreateExceptionInstance<T>(string name)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), name) as T)!;

	#endregion

	#region IfNull

	public static bool TrueIfNull<T>(this T? value)
		=> value is null;

	public static T ReturnIfNotNull<T>(this T? value)
		=> value ?? value!.ThrowIfNull<T>();

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
		=> NotNullOrEmpty(value, nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	private static string NotNullOrEmpty(string value, [InvokerParameterName] string parameterName)
		=> value.IsNullOrEmpty()
			   ? throw new ArgumentNullOrEmptyException(parameterName)
			   : value;

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotNullOrEmpty<T>(this IEnumerable<T> value)
		=> NotNullOrEmpty(value, nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	internal static bool NotNullOrEmpty<T>(this IEnumerable<T> value, [InvokerParameterName] string parameterName)
		=> value.IsNullOrEmpty()
			   ? throw new ArgumentNullOrEmptyException(parameterName)
			   : true;


	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotEqual(this int value, int expected)
		=> value.NotEqual(expected, nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotEqual(this int value, int expected, ArgumentException exception)
		=> value != expected
			   ? throw exception
			   : true;

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	internal static bool NotEqual(this int value, int expected, [InvokerParameterName] string parameterName)
		=> value != expected
			   ? throw new ArgumentEqualException(parameterName)
			   : true;

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotLessThan(this int value, int expected)
		=> NotLessThan(value, expected, nameof(value));

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotLessThan(this int value, int expected, [InvokerParameterName] string parameterName)
		=> value < expected
			   ? throw new ArgumentLessThanException(parameterName)
			   : true;

	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	public static bool NotMoreThan(this int value, int expected)
		=> NotMoreThan(value, expected, nameof(value));


	[ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
	internal static bool NotMoreThan(this int value, int expected, [InvokerParameterName] [NotNull] string parameterName)
		=> value > expected
			   ? throw new ArgumentMoreThanException(parameterName)
			   : true;
}