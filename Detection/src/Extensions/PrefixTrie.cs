// Copyright (c) 2014-2025 Sarin Na Wangkanai and Aliaksandr Kukrash, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Detection.Extensions;

public readonly struct PrefixTrie : IPrefixTrie
{
	private readonly PrefixTrie[]? _lookup;
	private readonly int _offset;

	/// <summary>
	/// Creates optimized search Trie using static keyword list, uses prefix search Trie as base algorithm
	/// <see href="https://en.wikipedia.org/wiki/Trie"/>
	/// </summary>
	/// <param name="keywords">Collection of static keywords</param>
	public PrefixTrie(string[]? keywords) : this(keywords, 0) { }

	/// <summary>
	/// Creates optimized search Trie using static keyword list, uses prefix search Trie as base algorithm
	/// <see href="https://en.wikipedia.org/wiki/Trie"/>
	/// </summary>
	/// <param name="keywords">Collection of static keywords</param>
	/// <param name="pos">Internal position indicator</param>
	private PrefixTrie(string[]? keywords, int pos)
	{
		if (pos > 0)
			keywords = keywords?.Where(k => k.Length > pos)
							   .ToArray();

		if (keywords.IsNullOrEmpty())
		{
			_lookup = [];
			_offset = 0;
			return;
		}

		var lower = keywords.Min(k => k[pos]);
		var upper = keywords.Max(k => k[pos]);

		_offset = lower;
		_lookup = new PrefixTrie[upper - lower + 1];

		foreach (var (key, list) in KeywordsGroupByCharAtPosition(keywords, pos))
		{
			var newKeys = list.ToArray();
			_lookup[key - lower] = newKeys.Any(k => pos + 1 >= k.Length)
									   ? new PrefixTrie(null, pos + 1)
									   : new PrefixTrie(newKeys, pos + 1);
		}
	}

	public bool IsEnd => _lookup?.Length == 0;

	public bool StartsWithAnyIn(ReadOnlySpan<char> source)
	{
		var current = this;

		if (source.Length == 0 || current.IsEnd)
			return false;

		while (!current.IsEnd)
		{
			var lookup = current._lookup;

			if (source.Length == 0 || lookup == null)
				return false;

			var c = source[0];
			var offset = current._offset;

			if (c - offset < 0 || c - offset >= lookup.Length)
				return false;

			current = lookup[c - offset];

			if (current._lookup == null)
				return false;

			source = source[1..];
		}

		return true;
	}

	public bool ContainsWithAnyIn(ReadOnlySpan<char> source)
	{
		while (source.Length > 0)
		{
			var slice = source;
			var current = this;
			var found = SearchCurrentSource(current, slice);

			if (found)
				return true;

			source = source[1..];
		}

		return false;
	}

	private static bool SearchCurrentSource(PrefixTrie current, ReadOnlySpan<char> slice)
	{
		var found = true;

		if (slice.Length == 0 || current.IsEnd)
			return false;

		while (!current.IsEnd)
		{
			var lookup = current._lookup;

			if (slice.Length == 0 || lookup == null)
			{
				found = false;
				break;
			}

			var c = slice[0];
			var offset = current._offset;

			if (c - offset < 0 || c - offset >= lookup.Length)
			{
				found = false;
				break;
			}

			current = lookup[c - offset];

			if (current._lookup == null)
			{
				found = false;
				break;
			}

			slice = slice[1..];
		}

		return found;
	}

	private static IEnumerable<(char Key, IGrouping<char, string> x)> KeywordsGroupByCharAtPosition(IEnumerable<string> keywords, int position)
		=> keywords.GroupBy(k => k[position])
				   .Select(x => (x.Key, x));
}
