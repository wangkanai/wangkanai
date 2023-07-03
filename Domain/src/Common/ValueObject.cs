// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Wangkanai.Domain.Caching;

namespace Wangkanai.Domain.Common;

public abstract class ValueObject : IValueObject, ICacheKey, ICloneable
{
	private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>> TypeProperties = new();

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(this, obj))
			return true;
		if (ReferenceEquals(null, obj))
			return false;
		if (GetType() != obj.GetType())
			return false;

		var other = obj as ValueObject;
		return other != null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
	}

	public override int GetHashCode()
	{
		unchecked
		{
			return GetEqualityComponents()
				.Aggregate(17, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
		}
	}

	public static bool operator ==(ValueObject left, ValueObject right)
		=> Equals(left, right);

	public static bool operator !=(ValueObject left, ValueObject right)
		=> !Equals(left, right);

	public override string ToString()
		=> $"{{{string.Join(", ", GetProperties().Select(f => $"{f.Name}: {f.GetValue(this)}"))}}}";

	public virtual string GetCacheKey()
	{
		var keyValues = GetEqualityComponents()
		                .Select(x => x is string ? $"'{x}'" : x)
		                .Select(x => x is ICacheKey cacheKey ? cacheKey.GetCacheKey() : x?.ToString());

		return string.Join("|", keyValues);
	}

	protected virtual IEnumerable<object> GetEqualityComponents()
	{
		foreach (var property in GetProperties())
		{
			var value = property.GetValue(this);
			if (value == null)
				yield return null;
			else
			{
				var valueType = value.GetType();
				if (valueType.IsAssignableFromGenericList())
				{
					yield return '[';
					foreach (var child in (IEnumerable)value)
						yield return child;

					yield return ']';
				}
				else
					yield return value;
			}
		}
	}

	public virtual IEnumerable<PropertyInfo> GetProperties()
		=> TypeProperties.GetOrAdd(GetType(), t => t.GetTypeInfo()
		                                            .GetProperties(BindingFlags.Instance | BindingFlags.Public))
		                 .OrderBy(p => p.Name)
		                 .ToList();

	public object Clone() => MemberwiseClone();
}