// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq.Expressions;

namespace Wangkanai.Domain.Common;

public static class ReflectionUtility
{
	private static readonly ObjectReferenceComparer Comparer = new();

	public static IEnumerable<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] propertyExpressions)
	{
		var result = new List<string>();
		foreach (var expression in propertyExpressions)
			result.Add(GetPropertyName(expression));

		return result;
	}

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

	public static bool IsAssignableFromGenericList(this Type type)
		=> type.GetInterfaces().Any(intType => intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IList<>));

	private class ObjectReferenceComparer : IEqualityComparer<object>
	{
		public new bool Equals(object? x, object? y)
			=> ReferenceEquals(x, y);

		public int GetHashCode(object obj)
			=> obj.ThrowIfNull()
			      .GetHashCode();
	}
}
