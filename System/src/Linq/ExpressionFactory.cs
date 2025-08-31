// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Linq.Expressions;

namespace Wangkanai.Linq;

public static class ExpressionFactory
{
   public static Func<TArg1, TResult> Create<TArg1, TResult>(Func<Expression, UnaryExpression> body)
   {
      var inp = Expression.Parameter(typeof(TArg1), "inp");
      try
      {
         return Expression.Lambda<Func<TArg1, TResult>>(body(inp), inp).Compile();
      }
      catch (Exception ex)
      {
         return delegate
         {
            throw new InvalidOperationException(ex.Message);
         };
      }
   }

   public static Func<TArg1, TArg2, TResult> Create<TArg1, TArg2, TResult>(Func<Expression, Expression, BinaryExpression> body)
      => Create<TArg1, TArg2, TResult>(body, false);

   public static Func<TArg1, TArg2, TResult> Create<TArg1, TArg2, TResult>(Func<Expression, Expression, BinaryExpression> body, bool castArgsToResultOnFailure)
   {
      var lhs = Expression.Parameter(typeof(TArg1), "lhs");
      var rhs = Expression.Parameter(typeof(TArg2), "rhs");
      try
      {
         try
         {
            return Expression.Lambda<Func<TArg1, TArg2, TResult>>(body(lhs, rhs), lhs, rhs).Compile();
         }
         catch (InvalidOperationException)
         {
            if (castArgsToResultOnFailure           // if we show retry
                && typeof(TArg1) == typeof(TResult) // and the args are not
                && typeof(TArg2) == typeof(TResult))
            {
               // convert both lhs and rhs to TResult (as appropriate)
               var castLhs = typeof(TArg1) == typeof(TResult)
                  ? lhs
                  : (Expression)Expression.Convert(lhs, typeof(TResult));
               var castRhs = typeof(TArg2) == typeof(TResult)
                  ? rhs
                  : (Expression)Expression.Convert(rhs, typeof(TResult));

               return Expression.Lambda<Func<TArg1, TArg2, TResult>>(body(castLhs, castRhs), lhs, rhs).Compile();
            }

            throw;
         }
      }
      catch (Exception ex)
      {
         return delegate
         {
            throw new InvalidOperationException(ex.Message);
         };
      }
   }
}