// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Wangkanai.Extensions;

public static class ObservableCollectionExtensions
{
	public static void Observe<T>(this ObservableCollection<T> collection, Action<T> addAction, Action<T> removeAction)
	{
		collection.CollectionChanged += (sender, args) => {
			if (args.Action == NotifyCollectionChangedAction.Add)
				foreach (var newItem in args.NewItems!)
					addAction((T)newItem);

			if (args.Action == NotifyCollectionChangedAction.Remove)
				foreach (var removeItem in args.OldItems!)
					removeAction((T)removeItem);
		};
	}
}
