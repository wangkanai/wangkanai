// Copyright (c) 2014-2025 Sarin Na Wangkanai and Aliaksandr Kukrash, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public static class SearchExtensions
{
	public static bool SearchStartsWith(this string search, IPrefixTrie tree)
		=> search.AsSpan().SearchStartsWith(tree);

	private static bool SearchStartsWith(this ReadOnlySpan<char> search, IPrefixTrie tree)
		=> tree.StartsWithAnyIn(search);

	public static bool SearchContains(this string search, IPrefixTrie tree)
		=> search.AsSpan().SearchContains(tree);

	private static bool SearchContains(this ReadOnlySpan<char> search, IPrefixTrie tree)
		=> tree.ContainsWithAnyIn(search);

	public static KmpPrefixTrie BuildSearchTrie(this string[] keywords)
		=> new(keywords);

	public static KmpPrefixTrie BuildSearchTrie(this IEnumerable<string> keywords)
		=> new(keywords.Distinct().ToArray());

	public static PrefixTrie BuildLegacySearchTrie(this string[] keywords)
		=> new(keywords);

	public static PrefixTrie BuildLegacySearchTrie(this IEnumerable<string> keywords)
		=> new(keywords.Distinct().ToArray());

}
