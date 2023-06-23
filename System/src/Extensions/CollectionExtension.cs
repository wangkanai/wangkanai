// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Wangkanai.Extensions;

/// <summary>
/// Class that provides extension method to collection
/// </summary>
[DebuggerStepThrough]
public static class CollectionExtension
{
	
	public static bool IsNullOrEmpty<T>(this ICollection<T> source)
		=> source is null || source.Count <= 0;

	/// <summary>
	/// Add a range of items to a collection.
	/// </summary>
	/// <param name="collection">Type of objects within the collection.</param>
	/// <param name="items">The collection to add items to.</param>
	/// <typeparam name="T">the collection.</typeparam>
	/// <returns>The collection.</returns>
	/// <exception cref="System.ArgumentNullException">An <see cref="System.ArgumentNullException"/> is thrown if <paramref name="collection"/> or <paramref name="items"/> is <see langword="null"/>.</exception>
	public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
	{
		collection.ThrowIfNull()
		          .ThrowIfEmpty();
		
		items.ThrowIfNull();

		foreach (var each in items)
			collection.Add(each);

		return collection;
	}

	public static void AddDistinct<T>(this ICollection<T> obj, params T[] items)
		=> AddDistinct(obj, null, items);

	public static void AddDistinct<T>(this ICollection<T> obj, IEqualityComparer<T> comparer, params T[] items)
	{
		obj.ThrowIfNull()
		   .ThrowIfEmpty();
		
		items.ThrowIfNull();

		foreach (var item in items)
		{
			var contains = comparer != null ? obj.Contains(item, comparer) : obj.Contains(item);

			if (!contains)
				obj.Add(item);
		}
	}

	public static void Replace<T>(this ICollection<T> obj, IEnumerable<T> newItems)
	{
		obj.ThrowIfNull()
		   .ThrowIfEmpty();

		obj.Clear();
		obj.AddRange(newItems);
	}

	public static void ObserveCollection<T>(this ObservableCollection<T> collection, Action<T> addAction, Action<T> removeAction)
	{
		collection.CollectionChanged += (sender, args) => {
			if (args.Action == NotifyCollectionChangedAction.Add && addAction != null)
				foreach (var newItem in args.NewItems)
					addAction((T)newItem);

			if (args.Action == NotifyCollectionChangedAction.Remove && removeAction != null)
				foreach (var removeItem in args.OldItems)
					removeAction((T)removeItem);
		};
	}
}