// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;

namespace Wangkanai.Extensions;

[DebuggerStepThrough]
public static class TypeExtensions
{
	private static readonly ConcurrentDictionary<Type, string> PrettyPrintCache = new();
	private static readonly ConcurrentDictionary<Type, string> TypeCacheKeys    = new();

	public static string GetCacheKey(this Type type)
		=> TypeCacheKeys.GetOrAdd(type, t => $"{t.PrettyPrint()}");

	public static string PrettyPrint(this Type type)
		=> PrettyPrintCache.GetOrAdd(type, t => {
			try
			{
				return t.PrettyPrintRecursive(0);
			}
			catch (Exception)
			{
				return t.Name;
			}
		});

	public static string PrettyPrint(this Type type, int depth)
		=> PrettyPrintCache.GetOrAdd(type, t => {
			try
			{
				return t.PrettyPrintRecursive(depth);
			}
			catch (Exception)
			{
				return t.Name;
			}
		});

	public static string PrettyPrintRecursive(this Type type, int depth)
	{
		if (depth > 3) return type.Name;

		var nameParts = type.Name.Split('`');
		if (nameParts.Length == 1)
			return nameParts[0];

		var genericArgs = type.GetTypeInfo().GetGenericArguments();
		return !type.IsConstructedGenericType
			       ? $"{nameParts[0]}<{new string(',', genericArgs.Length - 1)}>"
			       : $"{nameParts[0]}<{string.Join(",", genericArgs.Select(t => PrettyPrintRecursive(t, depth + 1)))}>";
	}

	public static Type[] GetTypeInheritanceChainTo(this Type child, Type parent)
	{
		var retVal   = new List<Type> { child };
		var baseType = child.BaseType;
		while (baseType != parent && baseType != typeof(object))
		{
			retVal.Add(baseType!);
			baseType = baseType!.BaseType;
		}

		return retVal.ToArray();
	}

	/// <summary>
	/// Check if type is a value-type, primitive type or string
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	private static bool IsPrimitive(this Type type)
		=> type == typeof(string) || type.IsValueType || type.IsPrimitive;

	/// <summary>
	/// Check if type is a value-type, primitype or string
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public static bool IsPrimitive(this object? obj)
		=> obj == null || obj.GetType().IsPrimitive();

	public static bool IsNullable(this Type type)
	{
		var typeInfo = type.GetTypeInfo();

		return !typeInfo.IsValueType
		       || typeInfo.IsGenericType
		       && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
	}

	public static Type MakeNullable(this Type type, bool nullable = true)
	{
		if (type.IsNullable() == nullable)
			return type;

		return nullable
			       ? typeof(Nullable<>).MakeGenericType(type)
			       : type.GetGenericArguments()[0];
	}

	public static Type UnwrapNullable(this Type type)
		=> Nullable.GetUnderlyingType(type) ?? type;

	public static Type UnwrapEnum(this Type type)
	{
		var isNullable                = type.IsNullable();
		var underlyingNonNullableType = isNullable ? type.UnwrapNullable() : type;
		if (!underlyingNonNullableType.GetTypeInfo().IsEnum)
			return type;

		var underlyingEnumType = Enum.GetUnderlyingType(underlyingNonNullableType);
		return isNullable ? MakeNullable(underlyingEnumType) : underlyingEnumType;
	}

	public static IEnumerable<KeyValuePair<string, object>> TraverseObjectGraph(this object original)
		=> original.TraverseObjectGraphRecursively(new List<object>(), original.GetType().Name);

	private static IEnumerable<KeyValuePair<string, object>> TraverseObjectGraphRecursively(this object? original, ICollection<object> visited, string memberPath)
	{
		yield return new KeyValuePair<string, object>(memberPath, original!);

		if (original == null) yield break;

		var typeOfOriginal = original.GetType();
		// ReferenceEquals is a mandatory approach
		if (IsPrimitive(typeOfOriginal) || visited.Any(x => ReferenceEquals(original, x)))
			yield break;
		visited.Add(original);

		if (original is IEnumerable objEnum)
			foreach (var keyValuePair in YieldOriginalEnumerator(visited, memberPath, objEnum))
				yield return keyValuePair;
		else
			foreach (var keyValuePair1 in YieldPropertyEnumerator(original, visited, memberPath, typeOfOriginal))
				yield return keyValuePair1;
	}

	private static IEnumerable<KeyValuePair<string, object>> YieldPropertyEnumerator(object original, ICollection<object> visited, string memberPath, Type typeOfOriginal)
		=> typeOfOriginal.GetProperties(BindingFlags.Instance | BindingFlags.Public)
		                 .SelectMany(propertyInfo => propertyInfo.GetValue(original).TraverseObjectGraphRecursively(visited, $@"{memberPath}.{propertyInfo.Name}"));

	private static IEnumerable<KeyValuePair<string, object>> YieldOriginalEnumerator(ICollection<object> visited, string memberPath, IEnumerable objEnum)
	{
		// ReSharper disable once NotDisposedResource
		var enumerator = objEnum.GetEnumerator();
		enumerator.ThrowIfNull();
		var index      = 0;
		while (enumerator.MoveNext())
			foreach (var result in enumerator.Current.TraverseObjectGraphRecursively(visited, $@"{memberPath}[{index++}]"))
				yield return result;
	}
}
