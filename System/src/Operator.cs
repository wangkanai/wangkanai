// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq.Expressions;

using Wangkanai.Linq;
using Wangkanai.System.Operators;

namespace Wangkanai;

public static class Operator
{
    public static bool HasValue<T>(T value)
        => Operator<T>.NullOperator.HasValue(value);

    public static bool AddIfNotNull<T>(ref T accumulator, T value)
        => Operator<T>.NullOperator.AddIfNotNull(ref accumulator, value);

    public static T Negate<T>(T value)
        => Operator<T>.Negate(value);

    public static T Not<T>(T value)
        => Operator<T>.Not(value);

    public static T Or<T>(T left, T right)
        => Operator<T>.Or(left, right);

    public static T And<T>(T left, T right)
        => Operator<T>.And(left, right);

    public static T Xor<T>(T left, T right)
        => Operator<T>.Xor(left, right);

    public static TTo Convert<TFrom, TTo>(TFrom value)
        => Operator<TFrom, TTo>.Convert(value);

    public static T Add<T>(T left, T right)
        => Operator<T>.Add(left, right);

    public static TArg1 AddAlternative<TArg1, TArg2>(TArg1 left, TArg2 right)
        => Operator<TArg2, TArg1>.Add(left, right);

    public static T Subtract<T>(T left, T right)
        => Operator<T>.Subtract(left, right);

    public static TArg1 SubtractAlternative<TArg1, TArg2>(TArg1 left, TArg2 right)
        => Operator<TArg2, TArg1>.Subtract(left, right);

    public static T Multiply<T>(T left, T right)
        => Operator<T>.Multiply(left, right);

    public static TArg1 MultiplyAlternative<TArg1, TArg2>(TArg1 left, TArg2 right)
        => Operator<TArg2, TArg1>.Multiply(left, right);

    public static T Divide<T>(T left, T right)
        => Operator<T>.Divide(left, right);

    public static TArg1 DivideAlternative<TArg1, TArg2>(TArg1 left, TArg2 right)
        => Operator<TArg2, TArg1>.Divide(left, right);

    public static T DivideInt32<T>(T value, int divisor)
        => Operator<int, T>.Divide(value, divisor);

    public static bool Equal<T>(T left, T right)
        => Operator<T>.Equal(left, right);

    public static bool NotEqual<T>(T left, T right)
        => Operator<T>.NotEqual(left, right);

    public static bool GreaterThan<T>(T left, T right)
        => Operator<T>.GreaterThan(left, right);

    public static bool GreaterThanOrEqual<T>(T left, T right)
        => Operator<T>.GreaterThanOrEqual(left, right);

    public static bool LessThan<T>(T left, T right)
        => Operator<T>.LessThan(left, right);

    public static bool LessThanOrEqual<T>(T left, T right)
        => Operator<T>.LessThanOrEqual(left, right);
}

public static class Operator<T>
{
    private static readonly INullOperator<T> nullOperator;
    internal static         INullOperator<T> NullOperator => nullOperator;

    private static readonly T zero;

    public static T Zero => zero;

    private static readonly Func<T, T, T> add, subtract, multiply, divide;

    public static Func<T, T, T> Add      => add;
    public static Func<T, T, T> Subtract => subtract;
    public static Func<T, T, T> Multiply => multiply;
    public static Func<T, T, T> Divide   => divide;

    private static readonly Func<T, T, bool> equal, notEqual, greaterThan, greaterThanOrEqual, lessThan, lessThanOrEqual;

    public static Func<T, T, bool> Equal              => equal;
    public static Func<T, T, bool> NotEqual           => notEqual;
    public static Func<T, T, bool> GreaterThan        => greaterThan;
    public static Func<T, T, bool> GreaterThanOrEqual => greaterThanOrEqual;
    public static Func<T, T, bool> LessThan           => lessThan;
    public static Func<T, T, bool> LessThanOrEqual    => lessThanOrEqual;

    private static readonly Func<T, T>    negate, not;
    private static readonly Func<T, T, T> or,     and, xor;

    public static Func<T, T>    Negate => negate;
    public static Func<T, T>    Not    => not;
    public static Func<T, T, T> Or     => or;
    public static Func<T, T, T> And    => and;
    public static Func<T, T, T> Xor    => xor;

    static Operator()
    {
        add      = ExpressionFactory.Create<T, T, T>(Expression.Add);
        subtract = ExpressionFactory.Create<T, T, T>(Expression.Subtract);
        divide   = ExpressionFactory.Create<T, T, T>(Expression.Divide);
        multiply = ExpressionFactory.Create<T, T, T>(Expression.Multiply);

        equal              = ExpressionFactory.Create<T, T, bool>(Expression.Equal);
        notEqual           = ExpressionFactory.Create<T, T, bool>(Expression.NotEqual);
        greaterThan        = ExpressionFactory.Create<T, T, bool>(Expression.GreaterThan);
        greaterThanOrEqual = ExpressionFactory.Create<T, T, bool>(Expression.GreaterThanOrEqual);
        lessThan           = ExpressionFactory.Create<T, T, bool>(Expression.LessThan);
        lessThanOrEqual    = ExpressionFactory.Create<T, T, bool>(Expression.LessThanOrEqual);

        negate = ExpressionFactory.Create<T, T>(Expression.Negate);
        not    = ExpressionFactory.Create<T, T>(Expression.Not);
        or     = ExpressionFactory.Create<T, T, T>(Expression.Or);
        and    = ExpressionFactory.Create<T, T, T>(Expression.And);
        xor    = ExpressionFactory.Create<T, T, T>(Expression.ExclusiveOr);

        var type = typeof(T);
        if (type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable))
        {
            var nullType = type.GetGenericArguments()[0];
            zero         = (T)Activator.CreateInstance(nullType);
            nullOperator = (INullOperator<T>)Activator.CreateInstance(typeof(StructNullOperator<>).MakeGenericType(nullType));
        }
        else
        {
            zero = default(T);
            if (type.IsValueType)
                nullOperator = (INullOperator<T>)Activator.CreateInstance(typeof(StructNullOperator<>).MakeGenericType(type));
            else
                nullOperator = (INullOperator<T>)Activator.CreateInstance(typeof(ClassNullOperator<>).MakeGenericType(type));
        }
    }
}

public static class Operator<TValue, TResult>
{
    private static readonly Func<TValue, TResult> convert;

    public static Func<TValue, TResult> Convert => convert;

    private static readonly Func<TResult, TValue, TResult> add, subtract, multiply, divide;

    public static Func<TResult, TValue, TResult> Add      => add;
    public static Func<TResult, TValue, TResult> Subtract => subtract;
    public static Func<TResult, TValue, TResult> Multiply => multiply;
    public static Func<TResult, TValue, TResult> Divide   => divide;

    static Operator()
    {
        convert  = ExpressionFactory.Create<TValue, TResult>(body => Expression.Convert(body, typeof(TResult)));
        add      = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Add, true);
        subtract = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Subtract, true);
        multiply = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Multiply, true);
        divide   = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Divide, true);
    }
}