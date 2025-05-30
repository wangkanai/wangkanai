// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Linq.Expressions;

namespace Wangkanai.Domain.Common;

/// <summary>Provides utility methods for working with reflection in C#.</summary>
public static class ReflectionUtility
{
	private static readonly ObjectReferenceComparer Comparer = new();

	/// <summary>Retrieves the property names from the given expressions.</summary>
	/// <typeparam name="T">The type containing the properties.</typeparam>
	/// <param name="propertyExpressions">An array of expressions pointing to the properties of the type.</param>
	/// <returns>A collection of property names extracted from the expressions.</returns>
	public static IEnumerable<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] propertyExpressions)
	{
		var result = new List<string>();
		foreach (var expression in propertyExpressions)
			result.Add(GetPropertyName(expression));

		return result;
	}

	/// <summary>Retrieves the property name from the given expression.</summary>
	/// <typeparam name="T">The type containing the property.</typeparam>
	/// <param name="propertyExpression">An expression pointing to the property of the type.</param>
	/// <returns>The name of the property extracted from the expression, or null if the expression is null.</returns>
	public static string GetPropertyName<T>(Expression<Func<T, object>>? propertyExpression)
	{
		if (propertyExpression is null)
			return null!;

		var lambda = (LambdaExpression)propertyExpression;

		var memberExpression = lambda.Body is UnaryExpression unaryExpression
			                       ? (MemberExpression)unaryExpression.Operand
			                       : (MemberExpression)lambda.Body;

		return memberExpression.Member.Name;
	}

	/// <summary>Determines whether the specified type implements a generic list interface.</summary>
	/// <param name="type">The type to check against the generic IList interface.</param>
	/// <returns>True if the specified type implements a generic IList interface; otherwise, false.</returns>
	public static bool IsAssignableFromGenericList(this Type type)
		=> type.GetInterfaces()
		       .Any(intType => intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IList<>));

	private class ObjectReferenceComparer : IEqualityComparer<object>
	{
		public new bool Equals(object? x, object? y)
			=> ReferenceEquals(x, y);

		public int GetHashCode(object obj)
			=> obj.ThrowIfNull()
			      .GetHashCode();
	}
}
