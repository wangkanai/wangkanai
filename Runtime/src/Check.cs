// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static T NotNull<T>(T value)
    {
        return NotNull(value, nameof(value));
    }

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static T NotNull<T>(T value, [InvokerParameterName] string parameterName)
    {
        return value is null
                   ? throw new ArgumentNullException(parameterName)
                   : value;
    }

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