// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Detection.Extensions;

public readonly struct IndexTree
{
	private readonly IndexTree[]? _lookup;
	private readonly int          _offset;

	private bool IsEnd => _lookup?.Length == 0;

	private static IEnumerable<(char Key, IGrouping<char, string> x)> KeywordsGroupBySeed(string[] keywords, int seed)
		=> keywords.GroupBy(k => k[seed])
		           .Select(x => (x.Key, x));

	public IndexTree(string[]? keywords, int seed = 0)
	{
		if (seed > 0)
			keywords = keywords?.Where(k => k.Length > seed).ToArray();

		if (keywords.IsNullOrEmpty())
		{
			_lookup = Array.Empty<IndexTree>();
			_offset = 0;
			return;
		}

		var lower = keywords.Min(k => k[seed]);
		var upper = keywords.Max(k => k[seed]);

		_offset = lower;
		_lookup = new IndexTree[upper - lower + 1];

		foreach (var (key, list) in KeywordsGroupBySeed(keywords, seed))
		{
			var newKeys = list.ToArray();
			_lookup[key - lower] = newKeys.Any(k => seed + 1 >= k.Length)
				                       ? new IndexTree(null, seed + 1)
				                       : new IndexTree(newKeys, seed + 1);
		}
	}

	public bool StartsWithAnyIn(ReadOnlySpan<char> source)
	{
		var current = this;

		while (!current.IsEnd)
		{
			var lookup = current._lookup;

			if (source.Length == 0 || lookup == null)
				return false;

			var c      = source[0];
			var offset = current._offset;

			if (c - offset < 0 || c - offset >= lookup.Length)
				return false;

			current = lookup[c - offset];

			if (current._lookup == null)
				return false;

			source = source.Slice(1);
		}

		return true;
	}

	public bool ContainsWithAnyIn(ReadOnlySpan<char> source)
	{
		while (source.Length > 0)
		{
			var slice   = source;
			var current = this;
			var found   = SearchCurrentSource(current, slice);

			if (found)
				return true;

			source = source.Slice(1);
		}

		return false;
	}

	private static bool SearchCurrentSource(IndexTree current, ReadOnlySpan<char> slice)
	{
		var found = true;

		while (!current.IsEnd)
		{
			var lookup = current._lookup;

			if (slice.Length == 0 || lookup == null)
			{
				found = false;
				break;
			}

			var c      = slice[0];
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

			slice = slice.Slice(1);
		}

		return found;
	}
}