// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq.Expressions;
using System.Runtime.ExceptionServices;

using Wangkanai.Domain;

namespace Wangkanai.Extensions;

[DebuggerStepThrough]
public static class QueryableExtensions
{
	/// <summary>
	/// Orders the elements of a sequence in ascending order based on a specified property.
	/// </summary>
	/// <typeparam name="T">The type of the elements of the source.</typeparam>
	/// <param name="source">The sequence to order.</param>
	/// <param name="property">The name of the property to order by.</param>
	/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the specified property.</returns>
	public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
		=> source.ApplyOrder(property, nameof(OrderBy));

	/// <summary>
	/// Applies ordering to the elements of a sequence based on a specified property.
	/// </summary>
	/// <typeparam name="T">The type of the elements of the source.</typeparam>
	/// <param name="source">The sequence to apply ordering to.</param>
	/// <param name="property">The name of the property to order by.</param>
	/// <param name="method">The name of the ordering method.</param>
	/// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the specified property.</returns>
	public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string property, string method)
	{
		property.ThrowIfNull();

		IOrderedQueryable<T> result = null!;

		var effectiveType = GetEffectiveType<T>();
		var expression    = Expression.Parameter(typeof(T));
		var expr = effectiveType == typeof(T)
			           ? (Expression)expression
			           : Expression.Convert(expression, effectiveType);

		var propertyExpression = GetPropertyExpression(property, expr);

		if (propertyExpression.TrueIfNull())
			return source.OrderBy(x => 1);

		var propertyType = propertyExpression.Type;
		var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
		// It is an expression for getting property value on each object from collection - e.g., for Student: x => x.Address.City
		var lambda = Expression.Lambda(delegateType, propertyExpression, expression);

		// Calling existing System.Linq.Queryable sorting method, e.g. for methodName = OrderBy: sourceOfEffectiveType.OrderBy(x => x.Address.City)
		result = (IOrderedQueryable<T>)InvokeGenericMethod(typeof(Queryable), method, new[] { typeof(T), propertyType }, new object[] { source, lambda });

		return result;
	}

	private static object InvokeGenericMethod(Type methodType, string methodName, Type[] genericTypeArguments, object[] methodArguments, object instance = null!)
	{
		try
		{
			return methodType.GetMethods()
			                 .Single(PredicateGenericMethod)
			                 .MakeGenericMethod(genericTypeArguments)
			                 .Invoke(instance, methodArguments)!;
		}
		// This catch is needed to get unwrapped exception, like calling method without reflection. Otherwise it would be TargetInvocationException with meaningful inner exception
		catch (TargetInvocationException ex)
		{
			ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
			return null;
		}

		bool PredicateGenericMethod(MethodInfo method) => method.Name == methodName &&
		                                                  method.IsGenericMethodDefinition &&
		                                                  method.GetGenericArguments().Length == genericTypeArguments.Length &&
		                                                  method.GetParameters().Length == methodArguments.Length;
	}


	private static Type GetEffectiveType<T>()
	{
		var registeredTypes = AbstractTypeFactory<T>.AllTypeInfos.ToList();
		return registeredTypes.Count == 1 ? registeredTypes[0].Type : typeof(T);
	}

	private static Expression GetPropertyExpression(string propertyString, Expression expression)
	{
		var result       = expression;
		var propertyType = expression.Type;
		var properties   = propertyString.Split('.');

		foreach (var property in properties)
		{
			// use reflection (not ComponentModel) to mirror LINQ
			var propertyInfo = propertyType.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (propertyInfo != null)
			{
				result       = Expression.Property(result, propertyInfo);
				propertyType = propertyInfo.PropertyType;
			}
			else
			{
				result = null;
				break;
			}
		}

		return result!;
	}
}
