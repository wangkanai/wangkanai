// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Wangkanai.Domain.Common;

public static class ReflectionUtility
{
	private static readonly ObjectReferenceComparer _comparer = new();

	public static IEnumerable<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] propertyExpressions)
	{
		var result = new List<string>();
		foreach (var expression in propertyExpressions)
			result.Add(GetPropertyName(expression));

		return result;
	}

	public static string GetPropertyName<T>(Expression<Func<T, object>> propertyExpression)
	{
		string result = null;
		if (propertyExpression != null)
		{
			var              lambda = (LambdaExpression)propertyExpression;
			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression unaryExpression)
				memberExpression = (MemberExpression)unaryExpression.Operand;
			else
				memberExpression = (MemberExpression)lambda.Body;

			result = memberExpression.Member.Name;
		}

		return result;
	}

	public static bool IsAssignableFromGenericList(this Type type)
	{
		foreach (var intType in type.GetInterfaces())
		{
			if (intType.IsGenericType && intType.GetGenericTypeDefinition() == typeof(IList<>))
				return true;
		}

		return false;
	}

	private class ObjectReferenceComparer : IEqualityComparer<object>
	{
		public new bool Equals(object x, object y)
			=> ReferenceEquals(x, y);

		public int GetHashCode(object obj)
			=> obj.ThrowIfNull()
			      .GetHashCode();
	}
}