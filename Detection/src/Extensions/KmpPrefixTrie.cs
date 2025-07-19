// Copyright (c) 2014-2025 Sarin Na Wangkanai and Aliaksandr Kukrash, All Rights Reserved. Apache License, Version 2.0

using System.Runtime.CompilerServices;

using Wangkanai.Extensions;

namespace Wangkanai.Detection.Extensions;

public class KmpPrefixTrie : IPrefixTrie
{
	private readonly KmpPrefixTrie?[]? _lookup;
	private readonly int _offset;
	private KmpPrefixTrie? _fallback;
	private readonly List<char> _variants;

	/// <summary>
	/// Creates optimized prefix Trie using static keyword list,
	/// combines prefix search Trie and Knuth–Morris–Pratt algorithm optimizations for each separate Trie path separately by creating a tables for each possible search path
	/// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
	/// <see href="https://en.wikipedia.org/wiki/Trie"/>
	/// </summary>
	/// <param name="keywords">Collection of static keywords</param>
	public KmpPrefixTrie(string[]? keywords) : this(keywords, 0) { }

	/// <summary>
	/// Creates optimized prefix Trie using static keyword list,
	/// combines prefix search Trie and Knuth–Morris–Pratt algorithm optimizations for each separate Trie path separately by creating a tables for each possible search path
	/// <see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>
	/// <see href="https://en.wikipedia.org/wiki/Trie"/>
	/// </summary>
	/// <param name="keywords">Collection of static keywords</param>
	/// <param name="pos">Internal position indicator</param>
	private KmpPrefixTrie(string[]? keywords, int pos)
	{
		if (pos > 0)
			keywords = keywords?.Where(k => k.Length > pos)
							   .ToArray();

		_variants = [];
		if (keywords.IsNullOrEmpty())
		{
			_lookup = null;
			_offset = 0;
			return;
		}

		var lowerBound = keywords.Min(k => k[pos]);
		var upperBound = keywords.Max(k => k[pos]);

		_offset = lowerBound;
		_lookup = new KmpPrefixTrie[upperBound - lowerBound + 1];

		foreach (var (key, group) in keywords.GroupBy(k => k[pos])
											 .Select(x => (x.Key, x)))
		{
			_variants.Add(key);
			var newKeys = group.ToArray();
			if (newKeys.Any(k => pos + 1 >= k.Length))
				_lookup[key - lowerBound] = new KmpPrefixTrie(null, pos + 1);
			else
				_lookup[key - lowerBound] = new KmpPrefixTrie(newKeys, pos + 1);
		}

		if (pos == 0)
		{
			_fallback = null;
			foreach (var variant in _variants)
				BuildKmpTable(this, GetNext(variant), this);
		}
	}

	public bool ContainsWithAnyIn(ReadOnlySpan<char> source)
	{
		var current = this;
		var length = source.Length;
		var seed = 0;

		while (length - seed > 0)
		{
			var c = source[seed];
			KmpPrefixTrie? next;
			if (current._lookup != null && c - current._offset >= 0 && c - current._offset < current._lookup.Length)
				next = current._lookup[c - current._offset];
			else
				next = null;

			if (next != null)
			{
				seed++;
				current = next;
				if (next._lookup.TrueIfNull())
					return true;
			}
			else
			{
				current = current._fallback;
				if (current.TrueIfNull())
				{
					seed++;
					current = this;
				}
			}
		}

		return false;
	}

	public bool StartsWithAnyIn(ReadOnlySpan<char> source)
	{
		var current = this;
		var length = source.Length;
		var seed = 0;

		while (length - seed > 0)
		{
			current = current.GetNext(source[seed]);

			if (current.TrueIfNull())
				return false;
			if (current is { _lookup: null })
				return true;

			seed++;
		}

		return false;
	}

	public bool IsEnd => _lookup == null;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private KmpPrefixTrie? GetNext(char c)
	{
		if (_lookup.TrueIfNull())
			return null;

		if (c - _offset < 0 || c - _offset >= _lookup.Length)
			return null;

		return _lookup[c - _offset];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private bool HasNext(char c)
	{
		if (_lookup.TrueIfNull())
			return false;

		if (c - _offset < 0 || c - _offset >= _lookup.Length)
			return false;

		return _lookup[c - _offset]?._lookup != null;
	}

	private static void BuildKmpTable(KmpPrefixTrie start, KmpPrefixTrie? nextPos = null, KmpPrefixTrie? currentCnd = null)
	{
		if (nextPos.TrueIfNull())
			return;

		if (currentCnd.TrueIfNull())
		{
			nextPos._fallback = null;
			return;
		}

		if (nextPos._variants.Count == 0)
			nextPos._fallback = currentCnd;

		foreach (var variant in nextPos._variants)
		{
			var pos = nextPos;
			var cnd = currentCnd;
			if (cnd.HasNext(variant))
				pos._fallback = cnd._fallback;
			else
			{
				pos._fallback = cnd;
				while (cnd != null && !cnd.HasNext(variant))
					cnd = cnd._fallback;
			}

			cnd = cnd?.GetNext(variant) ?? start;
			pos = pos.GetNext(variant);
			BuildKmpTable(start, pos, cnd);
		}
	}
}
