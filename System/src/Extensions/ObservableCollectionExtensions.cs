// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Wangkanai.Extensions;

/// <summary>
/// Contains extension methods for ObservableCollection class.
/// </summary>
[DebuggerStepThrough]
public static class ObservableCollectionExtensions
{
	/// <summary>
	/// Adds event handlers to the <see cref="ObservableCollection{T}.CollectionChanged"/> event to observe changes in the collection.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	/// <param name="collection">The collection to observe.</param>
	/// <param name="addAction">The action to perform when an item is added to the collection.</param>
	/// <param name="removeAction">The action to perform when an item is removed from the collection.</param>
	public static void Observe<T>(this ObservableCollection<T> collection, Action<T> addAction, Action<T> removeAction)
	{
		collection.CollectionChanged += (sender, args) =>
		{
			if (args.Action == NotifyCollectionChangedAction.Add)
				foreach (var newItem in args.NewItems!)
					addAction((T)newItem);

			if (args.Action == NotifyCollectionChangedAction.Remove)
				foreach (var removeItem in args.OldItems!)
					removeAction((T)removeItem);
		};
	}
}
