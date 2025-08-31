// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Linq.Expressions;

using Wangkanai.Linq;
using Wangkanai.Operators;

namespace Wangkanai;

public static class Operator
{
   public static bool HasValue<T>(T value) => Operator<T>.NullOperator.HasValue(value);

   public static bool AddIfNotNull<T>(ref T accumulator, T value) => Operator<T>.NullOperator.AddIfNotNull(ref accumulator, value);

   public static T Negate<T>(T value)         => Operator<T>.Negate(value);
   public static T Not<T>(T    value)         => Operator<T>.Not(value);
   public static T Or<T>(T     left, T right) => Operator<T>.Or(left, right);
   public static T And<T>(T    left, T right) => Operator<T>.And(left, right);
   public static T Xor<T>(T    left, T right) => Operator<T>.Xor(left, right);


   public static bool Equal<T>(T              left, T right) => Operator<T>.Equal(left, right);
   public static bool NotEqual<T>(T           left, T right) => Operator<T>.NotEqual(left, right);
   public static bool GreaterThan<T>(T        left, T right) => Operator<T>.GreaterThan(left, right);
   public static bool GreaterThanOrEqual<T>(T left, T right) => Operator<T>.GreaterThanOrEqual(left, right);
   public static bool LessThan<T>(T           left, T right) => Operator<T>.LessThan(left, right);
   public static bool LessThanOrEqual<T>(T    left, T right) => Operator<T>.LessThanOrEqual(left, right);

   public static TTo Convert<TFrom, TTo>(TFrom value) => Operator<TFrom, TTo>.Convert(value);

   public static T     Add<T>(T                                left,  T     right)   => Operator<T>.Add(left, right);
   public static TArg1 AddAlternative<TArg1, TArg2>(TArg1      left,  TArg2 right)   => Operator<TArg2, TArg1>.Add(left, right);
   public static T     Subtract<T>(T                           left,  T     right)   => Operator<T>.Subtract(left, right);
   public static TArg1 SubtractAlternative<TArg1, TArg2>(TArg1 left,  TArg2 right)   => Operator<TArg2, TArg1>.Subtract(left, right);
   public static T     Multiply<T>(T                           left,  T     right)   => Operator<T>.Multiply(left, right);
   public static TArg1 MultiplyAlternative<TArg1, TArg2>(TArg1 left,  TArg2 right)   => Operator<TArg2, TArg1>.Multiply(left, right);
   public static T     Divide<T>(T                             left,  T     right)   => Operator<T>.Divide(left, right);
   public static T     DivideInt32<T>(T                        value, int   divisor) => Operator<int, T>.Divide(value, divisor);
   public static TArg1 DivideAlternative<TArg1, TArg2>(TArg1   left,  TArg2 right)   => Operator<TArg2, TArg1>.Divide(left, right);
}

public static class Operator<T>
{
   static Operator()
   {
      Add      = ExpressionFactory.Create<T, T, T>(Expression.Add);
      Subtract = ExpressionFactory.Create<T, T, T>(Expression.Subtract);
      Divide   = ExpressionFactory.Create<T, T, T>(Expression.Divide);
      Multiply = ExpressionFactory.Create<T, T, T>(Expression.Multiply);

      Equal              = ExpressionFactory.Create<T, T, bool>(Expression.Equal);
      NotEqual           = ExpressionFactory.Create<T, T, bool>(Expression.NotEqual);
      GreaterThan        = ExpressionFactory.Create<T, T, bool>(Expression.GreaterThan);
      GreaterThanOrEqual = ExpressionFactory.Create<T, T, bool>(Expression.GreaterThanOrEqual);
      LessThan           = ExpressionFactory.Create<T, T, bool>(Expression.LessThan);
      LessThanOrEqual    = ExpressionFactory.Create<T, T, bool>(Expression.LessThanOrEqual);

      Negate = ExpressionFactory.Create<T, T>(Expression.Negate);
      Not    = ExpressionFactory.Create<T, T>(Expression.Not);
      Or     = ExpressionFactory.Create<T, T, T>(Expression.Or);
      And    = ExpressionFactory.Create<T, T, T>(Expression.And);
      Xor    = ExpressionFactory.Create<T, T, T>(Expression.ExclusiveOr);

      var type = typeof(T);
      if (type is { IsValueType: true, IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(Nullable))
      {
         var nullType = type.GetGenericArguments()[0];
         Zero         = (T)Activator.CreateInstance(nullType)!;
         NullOperator = (INullOperator<T>)Activator.CreateInstance(typeof(StructNullOperator<>).MakeGenericType(nullType))!;
      }
      else
      {
         Zero = default!;
         NullOperator = type.IsValueType
            ? (INullOperator<T>)Activator.CreateInstance(typeof(StructNullOperator<>).MakeGenericType(type))!
            : (INullOperator<T>)Activator.CreateInstance(typeof(ClassNullOperator<>).MakeGenericType(type))!;
      }
   }

   internal static INullOperator<T> NullOperator { get; }

   public static T Zero { get; }

   // Numeric operators
   public static Func<T, T, T> Add      { get; }
   public static Func<T, T, T> Subtract { get; }
   public static Func<T, T, T> Multiply { get; }

   public static Func<T, T, T> Divide { get; }

   // Comparison operators
   public static Func<T, T, bool> Equal              { get; }
   public static Func<T, T, bool> NotEqual           { get; }
   public static Func<T, T, bool> GreaterThan        { get; }
   public static Func<T, T, bool> GreaterThanOrEqual { get; }
   public static Func<T, T, bool> LessThan           { get; }
   public static Func<T, T, bool> LessThanOrEqual    { get; }

   // Logical operators
   public static Func<T, T>    Negate { get; }
   public static Func<T, T>    Not    { get; }
   public static Func<T, T, T> Or     { get; }
   public static Func<T, T, T> And    { get; }
   public static Func<T, T, T> Xor    { get; }
}

public static class Operator<TValue, TResult>
{
   static Operator()
   {
      Convert  = ExpressionFactory.Create<TValue, TResult>(body => Expression.Convert(body, typeof(TResult)));
      Add      = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Add,      true);
      Subtract = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Subtract, true);
      Multiply = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Multiply, true);
      Divide   = ExpressionFactory.Create<TResult, TValue, TResult>(Expression.Divide,   true);
   }

   public static Func<TValue, TResult>          Convert  { get; }
   public static Func<TResult, TValue, TResult> Add      { get; }
   public static Func<TResult, TValue, TResult> Subtract { get; }
   public static Func<TResult, TValue, TResult> Multiply { get; }
   public static Func<TResult, TValue, TResult> Divide   { get; }
}