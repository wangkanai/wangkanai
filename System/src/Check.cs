// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Runtime.CompilerServices;

using Wangkanai.Exceptions;
using Wangkanai.Extensions;
using Wangkanai.Resources;

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static bool? IfNullThrow(this bool? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static bool? IfNullThrow<T>(this bool? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static sbyte? IfNullThrow(this sbyte? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static sbyte? IfNullThrow<T>(this sbyte? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static byte? IfNullThrow(this byte? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static byte? IfNullThrow<T>(this byte? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static short? IfNullThrow(this short? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static short? IfNullThrow<T>(this short? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static ushort? IfNullThrow(this ushort? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static ushort? IfNullThrow<T>(this ushort? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static int? IfNullThrow(this int? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static int? IfNullThrow<T>(this int? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static uint? IfNullThrow(this uint? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static uint? IfNullThrow<T>(this uint? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static nint? IfNullThrow(this nint? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static nint? IfNullThrow<T>(this nint? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static nuint? IfNullThrow(this nuint? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static nuint? IfNullThrow<T>(this nuint? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static long? IfNullThrow(this long? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static long? IfNullThrow<T>(this long? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static ulong? IfNullThrow(this ulong? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static ulong? IfNullThrow<T>(this ulong? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static float? IfNullThrow(this float? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static float? IfNullThrow<T>(this float? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static double? IfNullThrow(this double? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static double? IfNullThrow<T>(this double? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static decimal? IfNullThrow(this decimal? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static decimal? IfNullThrow<T>(this decimal? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static char? IfNullThrow(this char? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static char? IfNullThrow<T>(this char? value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [CanBeNull]
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static string IfNullThrow([CanBeNull] this string value)
        => IfNullThrow<ArgumentNullException>(value);

    [CanBeNull]
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static string IfNullThrow<T>([CanBeNull] this string value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static object IfNullThrow<T>(this object value)
        where T : Exception
        => value ?? throw CreateExceptionInstance<T>(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static T IfNullThrow<T>(this T value)
        where T : class
        => value ?? throw CreateExceptionInstance<ArgumentNullException>(nameof(value));


    private static T CreateExceptionInstance<T>(string name)
        where T : Exception
        => Activator.CreateInstance(typeof(T), name) as T;

    #region prepare depreciate

    [Obsolete]
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static T NotNull<T>([CanBeNull] T value)
        => NotNull(value, nameof(value));

    [Obsolete]
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static T NotNull<T>([CanBeNull] T value, [InvokerParameterName] string parameterName)
        => value is null
               ? throw new ArgumentNullException(parameterName)
               : value;

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

    #endregion

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