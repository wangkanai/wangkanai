// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
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
    public static bool NotNullOrEmpty<T>(IEnumerable<T> value)
        => NotNullOrEmpty(value, nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static bool NotNullOrEmpty<T>(IEnumerable<T> value, [InvokerParameterName] string parameterName)
        => value.IsNullOrEmpty()
               ? throw new ArgumentNullOrEmptyException(parameterName)
               : true;

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static bool NotEqual(int value, int expected)
        => NotEqual(value, expected, nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static bool NotEqual(int value, int expected, [InvokerParameterName] string parameterName)
        => value != expected
               ? throw new ArgumentEqualException(parameterName)
               : true;

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static bool NotLessThan(int value, int expected)
        => NotLessThan(value, expected, nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static bool NotLessThan(int value, int expected, [InvokerParameterName] string parameterName)
        => value < expected
               ? throw new ArgumentLessThanException(parameterName)
               : true;

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    public static bool NotMoreThan(int value, int expected)
        => NotMoreThan(value, expected, nameof(value));

    [ContractAnnotation(AnnotationResources.ValueNullThenHalt)]
    internal static bool NotMoreThan(int value, int expected, [InvokerParameterName] [NotNull] string parameterName)
        => value > expected
               ? throw new ArgumentMoreThanException(parameterName)
               : true;
}