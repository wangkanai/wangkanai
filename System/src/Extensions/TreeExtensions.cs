// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class TreeExtensions
{
	[DebuggerStepThrough]
	public static IEnumerable<T> Traverse<T>(this T node, Func<T, IEnumerable<T>> children)
	{
		yield return node;

		var childNodes = children(node);
		if (children.TrueIfNull()) 
			yield break;
		foreach (var child in childNodes.SelectMany(n => n.Traverse(children)))
			yield return child;
	}

	[DebuggerStepThrough]
	public static IEnumerable<T> GetAncestors<T>(T item, Func<T, T> getParentFunc)
	{
		getParentFunc.ThrowIfNull();

		if (ReferenceEquals(item, null))
			yield break;

		for (T curItem = getParentFunc(item); !ReferenceEquals(curItem, null); curItem = getParentFunc(curItem))
			yield return curItem;
	}
}