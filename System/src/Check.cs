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
    public static int? IfNullThrow(this int? value)
        => IfNullThrow<ArgumentNullException>(value);

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static int? IfNullThrow<T>(this int? value)
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

    private static T CreateExceptionInstance<T>(string name)
        where T : Exception
        => Activator.CreateInstance(typeof(T), name) as T;

    #region preparedepreciate

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static T NotNull<T>([CanBeNull] T value)
        => NotNull(value, nameof(value));

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