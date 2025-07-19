// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;

namespace Wangkanai.Extensions;

/// <summary>
/// Provides extension methods for the System.Type class.
/// </summary>
[DebuggerStepThrough]
public static class TypeExtensions
{
	private static readonly ConcurrentDictionary<Type, string> PrettyPrintCache = new();
	private static readonly ConcurrentDictionary<Type, string> TypeCacheKeys = new();

	/// <summary>
	/// Get the cache key for a given type.
	/// </summary>
	/// <param name="type">The type for which to get the cache key.</param>
	/// <returns>The cache key for the given type.</returns>
	public static string GetCacheKey(this Type type)
		=> TypeCacheKeys.GetOrAdd(type, t => $"{t.PrettyPrint()}");

	/// <summary>
	/// Pretty-print the given Type.
	/// </summary>
	/// <param name="type">The Type to be pretty-printed.</param>
	/// <returns>The pretty-printed string representation of the Type.</returns>
	public static string PrettyPrint(this Type type)
		=> PrettyPrintCache.GetOrAdd(type, t =>
		{
			try
			{
				return t.PrettyPrintRecursive(0);
			}
			catch (Exception)
			{
				return t.Name;
			}
		});

	/// <summary>
	/// Pretty-print the given Type.
	/// </summary>
	/// <param name="type">The Type to be pretty-printed.</param>
	/// <param name="depth">The depth of the pretty-printed string representation.</param>
	/// <returns>The pretty-printed string representation of the Type.</returns>
	public static string PrettyPrint(this Type type, int depth)
		=> PrettyPrintCache.GetOrAdd(type, t =>
		{
			try
			{
				return t.PrettyPrintRecursive(depth);
			}
			catch (Exception)
			{
				return t.Name;
			}
		});

	/// <summary>
	/// Recursively pretty-prints the given Type.
	/// </summary>
	/// <param name="type">The Type to be pretty-printed.</param>
	/// <param name="depth">The current depth of recursion.</param>
	/// <returns>The pretty-printed string representation of the Type.</returns>
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

	/// <summary>
	/// Returns an array of types representing the inheritance chain from a given child type to a specified parent type.
	/// </summary>
	/// <param name="child">The child type.</param>
	/// <param name="parent">The parent type.</param>
	/// <returns>An array of types representing the inheritance chain from the child type to the parent type.</returns>
	public static Type[] GetTypeInheritanceChainTo(this Type child, Type parent)
	{
		var retVal = new List<Type> { child };
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

	/// <summary>
	/// Determines whether a given type is nullable.
	/// </summary>
	/// <param name="type">The type to check.</param>
	/// <returns>True if the type is nullable; otherwise, false.</returns>
	public static bool IsNullable(this Type type)
	{
		var typeInfo = type.GetTypeInfo();

		return !typeInfo.IsValueType
			   || typeInfo.IsGenericType
			   && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
	}

	/// <summary>
	/// Makes a given type nullable or non-nullable based on the specified flag.
	/// </summary>
	/// <param name="type">The type to make nullable or non-nullable.</param>
	/// <param name="nullable">A flag indicating whether to make the type nullable or non-nullable. The default value is true.</param>
	/// <returns>The nullable or non-nullable version of the given type.</returns>
	public static Type MakeNullable(this Type type, bool nullable = true)
	{
		if (type.IsNullable() == nullable)
			return type;

		return nullable
				   ? typeof(Nullable<>).MakeGenericType(type)
				   : type.GetGenericArguments()[0];
	}

	/// <summary>
	/// Unwraps a nullable type, returning the underlying non-nullable type. If the given type is not nullable, it returns the same type.
	/// </summary>
	/// <param name="type">The nullable type to unwrap.</param>
	/// <returns>The underlying non-nullable type.</returns>
	public static Type UnwrapNullable(this Type type)
		=> Nullable.GetUnderlyingType(type) ?? type;

	/// <summary>
	/// Unwraps an enum type, returning the underlying non-enum type. If the given type is not an enum, it returns the same type.
	/// </summary>
	/// <param name="type">The enum type to unwrap.</param>
	/// <returns>The underlying non-enum type.</returns>
	public static Type UnwrapEnum(this Type type)
	{
		var isNullable = type.IsNullable();
		var underlyingNonNullableType = isNullable ? type.UnwrapNullable() : type;
		if (!underlyingNonNullableType.GetTypeInfo().IsEnum)
			return type;

		var underlyingEnumType = Enum.GetUnderlyingType(underlyingNonNullableType);
		return isNullable ? MakeNullable(underlyingEnumType) : underlyingEnumType;
	}

	/// <summary>
	/// Traverse the object graph of the given object recursively and return a collection of key-value pairs representing the members of the object and their corresponding values.
	/// </summary>
	/// <param name="original">The original object to traverse.</param>
	/// <returns>A collection of key-value pairs representing the members of the object and their corresponding values.</returns>
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
		var index = 0;
		while (enumerator.MoveNext())
			foreach (var result in enumerator.Current.TraverseObjectGraphRecursively(visited, $@"{memberPath}[{index++}]"))
				yield return result;
	}
}
