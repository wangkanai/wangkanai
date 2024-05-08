// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#pragma warning disable CS8777 // Parameter must have a non-null value when exiting.

namespace Wangkanai.Extensions;

/// <summary>
/// Provides extension methods for enumerable.
/// </summary>
[DebuggerStepThrough]
public static class EnumerableExtensions
{
	/// <summary>
	/// Determines whether the given enumerable is null.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable.</typeparam>
	/// <param name="list">The enumerable to check.</param>
	/// <returns>True if the enumerable is null; otherwise, false.</returns>
	public static bool IsNull<T>([NotNull] this IEnumerable<T>? list)
		=> list is null;

	/// <summary>
	/// Determines whether the given enumerable is empty.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable.</typeparam>
	/// <param name="list">The enumerable to check.</param>
	/// <returns>True if the enumerable is empty; otherwise, false.</returns>
	public static bool IsEmpty<T>([NotNull] this IEnumerable<T>? list)
		=> list is null || !list.Any();

	/// <summary>
	/// Determines whether the given enumerable is null or empty.
	/// </summary>
	/// <typeparam name="T">The type of the enumerable.</typeparam>
	/// <param name="list">The enumerable to check.</param>
	/// <returns>True if the enumerable is null or empty; otherwise, false.</returns>
	public static bool IsNullOrEmpty<T>([NotNull] this IEnumerable<T>? list)
		=> list is null || !list.Any();

	/// <summary>
	/// Determines whether the given enumerable has any duplicates based on a specified property selector.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
	/// <typeparam name="TProp">The type of the property to check for duplicates.</typeparam>
	/// <param name="list">The enumerable to check for duplicates.</param>
	/// <param name="selector">The property selector to extract the property value from each element.</param>
	/// <returns>True if the enumerable has duplicates; otherwise, false.</returns>
	public static bool HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector)
	{
		var hash       = new HashSet<TProp>();
		var enumerable = list as T[] ?? list.ToArray();
		foreach (var value in enumerable)
			hash.Add(selector(value));
		var result = hash.Count != enumerable.Length;
		return result;
	}

	/// <summary>
	/// Splits an enumerable into smaller chunks (pages) based on a specified page size.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the enumerable.</typeparam>
	/// <param name="items">The enumerable to be paginated.</param>
	/// <param name="pageSize">The maximum number of elements in each page.</param>
	/// <returns>An enumerable of enumerable representing the paginated results.</returns>
	public static IEnumerable<IEnumerable<T>> Paginate<T>(this IEnumerable<T>? items, int pageSize)
	{
		var page = new List<T>();

		foreach (var item in items ?? Enumerable.Empty<T>())
		{
			page.Add(item);
			if (page.Count >= pageSize)
			{
				yield return page;
				page = [];
			}
		}

		if (page.Count > 0)
			yield return page;
	}

	/// <summary>
	/// Performs the indicated action on each item.
	/// </summary>
	/// <param name="items"></param>
	/// <param name="action">The action to be performed.</param>
	/// <remarks>If an exception occurs, the action will not be performed on the remaining items.</remarks>
	public static void Apply<T>(this IEnumerable<T> items, Action<T> action)
	{
		foreach (var item in items ?? Enumerable.Empty<T>())
			action(item);
	}

	/// <summary>
	/// Performs the indicated action on each item.
	/// </summary>
	/// <param name="items"></param>
	/// <param name="action">The action to be performed.</param>
	/// <remarks>If an exception occurs, the action will not be performed on the remaining items.</remarks>
	public static void Apply<T>(this List<T> items, Action<T> action)
	{
		foreach (var item in items ?? Enumerable.Empty<T>())
			action(item);
	}

	/// <summary>
	/// Performs the indicated action on each item.
	/// </summary>
	/// <param name="items"></param>
	/// <param name="action">The action to be performed.</param>
	/// <remarks>If an exception occurs, the action will not be performed on the remaining items.</remarks>
	public static void Apply<T1, T2>([NotNull] this IDictionary<T1, T2>? items, Action<T1, T2> action)
	{
		if (items is null)
			return;

		foreach (var key in items.Keys)
			action(key, items[key]!);
	}

	/// <summary>
	/// Converts an enumerable to a dictionary using the provided selector function to map the values to keys.
	/// </summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	/// <param name="source">The source enumerable.</param>
	/// <param name="selector">The function used to map the values to keys.</param>
	/// <returns>A dictionary with keys and values derived from the source enumerable.</returns>
	public static IDictionary<TKey, TValue> ToIDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> selector)
		where TKey : notnull
		=> source.ToDictionary(selector);

	/// <summary>
	/// Converts an enumerable of values to an <see cref="IDictionary{TKey, TValue}"/> using the specified key selector and default equality comparer for the key type.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="source">The enumerable of values to convert.</param>
	/// <param name="selector">A function to extract the key from each value.</param>
	/// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys, or <see langword="null"/> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
	/// <returns>An <see cref="IDictionary{TKey, TValue}"/> containing the converted key-value pairs.</returns>
	public static IDictionary<TKey, TValue> ToIDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> selector, IEqualityComparer<TKey> comparer)
		where TKey : notnull
		=> source.ToDictionary(selector, comparer);
}
