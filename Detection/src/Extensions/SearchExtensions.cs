// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public static class SearchExtensions
{
	public static bool SearchStartsWith(this string search, IndexTree tree)
		=> search.AsSpan().SearchStartsWith(tree);

	private static bool SearchStartsWith(this ReadOnlySpan<char> search, IndexTree tree)
		=> tree.StartsWithAnyIn(search);

	public static bool SearchContains(this string search, IndexTree tree)
		=> search.AsSpan().SearchContains(tree);

	private static bool SearchContains(this ReadOnlySpan<char> search, IndexTree tree)
		=> tree.ContainsWithAnyIn(search);

	public static IndexTree BuildIndexTree(this string[] keywords)
		=> new(keywords);

	public static IndexTree BuildIndexTree(this IEnumerable<string> keywords)
		=> new(keywords.Distinct().ToArray());
}
