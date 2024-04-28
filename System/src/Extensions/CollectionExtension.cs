// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

namespace Wangkanai.Extensions;

/// <summary>
/// Class that provides extension method to collection
/// </summary>
[DebuggerStepThrough]
public static class CollectionExtension
{
	public static bool IsNull<T>(this ICollection<T>? list)
		=> list is null;

	public static bool IsEmpty<T>(this ICollection<T>? list)
		=> list is null || list.Count <= 0;

	public static bool IsNullOrEmpty<T>(this ICollection<T>? list)
		=> list is null || list.Count <= 0;

	/// <summary>
	/// Add a range of items to a collection.
	/// </summary>
	/// <param name="list">Type of objects within the collection.</param>
	/// <param name="items">The collection to add items to.</param>
	/// <typeparam name="T">the collection.</typeparam>
	/// <returns>The collection.</returns>
	/// <exception cref="System.ArgumentNullException">An <see cref="System.ArgumentNullException"/> is thrown if <paramref name="list"/> or <paramref name="items"/> is <see langword="null"/>.</exception>
	public static ICollection<T> AddRangeSafe<T>(this ICollection<T> list, ICollection<T> items)
	{
		list.ThrowIfNull()
		    .ThrowIfEmpty();
		items.ThrowIfNull()
		     .ThrowIfEmpty();

		foreach (var each in items)
			list.Add(each);

		return list;
	}

	public static ICollection<T> AddDistinct<T>(this ICollection<T> list, params T[] items)
		=> AddDistinct(list, null!, items);

	public static ICollection<T> AddDistinct<T>(this ICollection<T> list, IEqualityComparer<T> comparer, params T[] items)
	{
		list.ThrowIfNull()
		    .ThrowIfEmpty();
		items.ThrowIfNull()
		     .ThrowIfEmpty();

		foreach (var item in items)
		{
			var contains = comparer != null ? list.Contains(item, comparer) : list.Contains(item);

			if (!contains)
				list.Add(item);
		}

		return list;
	}

	public static ICollection<T> Replace<T>(this ICollection<T> list, ICollection<T> items)
	{
		//list.ThrowIfNull();

		//items.ThrowIfNull();

		list.Clear();
		list.AddRangeSafe(items);

		return list;
	}
}
