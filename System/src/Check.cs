// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;
using Wangkanai.Resources;

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static T IfNullThrow<T>(this T value)
        => value.IfNullThrow(nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static T IfNullThrow<T>(this T value, [InvokerParameterName] string name)
        => value is null
               ? throw new ArgumentNullException(name)
               : value;

    // [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    // public static void IfNullThrow<TException, TValue>(this TValue value)
    //     where TException : Exception
    //     => value.IfNullThrow<TValue, TException>(nameof(value));
    //
    // [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    // public static void IfNullThrow<TValue, TException>(this TValue value, string name)
    //     where TException : Exception
    // {
    //     if (value is null)
    //         throw (TException)Activator.CreateInstance(typeof(TException), name);
    // }

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static T NotNull<T>(T value)
        => NotNull(value, nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static T NotNull<T>(T value, [InvokerParameterName] string parameterName)
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