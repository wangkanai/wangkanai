// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class EnumerableExtensions
{
	/// <summary>
	/// Indicates whether the specified collection is null or has a length of zero.
	/// </summary>
	/// <param name="list">The data to test.</param>
	/// <returns>true if the array parameter is null or has a length of zero; otherwise, false.</returns>
	[DebuggerStepThrough]
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
		=> list is null || !list.Any();

	[DebuggerStepThrough]
	public static bool HasDuplicates<T, TProp>(this IEnumerable<T> list, Func<T, TProp> selector)
		=> list.Any(t => !new HashSet<TProp>().Add(selector(t)));

	[DebuggerStepThrough]
	public static IEnumerable<IEnumerable<T>> Paginate<T>(this IEnumerable<T> items, int pageSize)
	{
		var page = new List<T>();

		foreach (var item in items ?? Enumerable.Empty<T>())
		{
			page.Add(item);
			if (page.Count >= pageSize)
			{
				yield return page;
				page = new List<T>();
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
	[DebuggerStepThrough]
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
	[DebuggerStepThrough]
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
	[DebuggerStepThrough]
	public static void Apply(this IDictionary items, Action<object, object> action)
	{
		if (items is null)
			return;

		foreach (var key in items.Keys)
			action(key, items[key]);
	}

	[DebuggerStepThrough]
	public static IDictionary<TKey, TValue> ToIDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> selector)
		=> source.ToDictionary(selector);

	[DebuggerStepThrough]
	public static IDictionary<TKey, TValue> ToIDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> selector, IEqualityComparer<TKey> comparer)
		=> source.ToDictionary(selector, comparer);
}