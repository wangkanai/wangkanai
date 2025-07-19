// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Detection.Extensions;

namespace Wangkanai.Detection;

[MemoryDiagnoser]
public class TrieBenchmark
{
	// Test data
	private readonly string[] _smallKeywordSet = { "apple", "banana", "orange", "pear", "grape" };
	private readonly string[] _largeKeywordSet;
	private readonly string[] _repeatingPatternKeywords;

	// Search targets
	private const string SmallText = "I like to eat apples and bananas";
	private const string LargeText = "This is a longer text that contains multiple words to search for including apple, orange and many other terms that might not be in our dictionary";
	private readonly string _longText;

	// Trie instances
	private readonly PrefixTrie _standardTrieSmall;
	private readonly KmpPrefixTrie _kmpTrieSmall;
	private readonly PrefixTrie _standardTrieLarge;
	private readonly KmpPrefixTrie _kmpTrieLarge;
	private readonly PrefixTrie _standardTriePatterns;
	private readonly KmpPrefixTrie _kmpTriePatterns;

	public TrieBenchmark()
	{
		// Initialize large keyword set (100 keywords)
		_largeKeywordSet = new string[100];
		for (int i = 0; i < 100; i++)
		{
			_largeKeywordSet[i] = $"keyword{i}";
		}

		// Initialize keywords with repeating patterns that benefit from KMP
		_repeatingPatternKeywords = new[]
		{
			"ababababc",
			"aabaabaac",
			"aaaabaaaac",
			"abcabcabcd"
		};

		// Generate a 10,000 character long text
		_longText = new string('x', 10000);

		// Initialize tries
		_standardTrieSmall = new PrefixTrie(_smallKeywordSet);
		_kmpTrieSmall = new KmpPrefixTrie(_smallKeywordSet);
		_standardTrieLarge = new PrefixTrie(_largeKeywordSet);
		_kmpTrieLarge = new KmpPrefixTrie(_largeKeywordSet);
		_standardTriePatterns = new PrefixTrie(_repeatingPatternKeywords);
		_kmpTriePatterns = new KmpPrefixTrie(_repeatingPatternKeywords);
	}

	[Benchmark]
	public bool StandardTrie_SmallKeywordSet_SmallText()
	{
		return SmallText.SearchContains(_standardTrieSmall);
	}

	[Benchmark]
	public bool KmpTrie_SmallKeywordSet_SmallText()
	{
		return SmallText.SearchContains(_kmpTrieSmall);
	}

	[Benchmark]
	public bool StandardTrie_LargeKeywordSet_LargeText()
	{
		return LargeText.SearchContains(_standardTrieLarge);
	}

	[Benchmark]
	public bool KmpTrie_LargeKeywordSet_LargeText()
	{
		return LargeText.SearchContains(_kmpTrieLarge);
	}

	[Benchmark]
	public bool StandardTrie_RepeatingPatterns_LargeText()
	{
		return LargeText.SearchContains(_standardTriePatterns);
	}

	[Benchmark]
	public bool KmpTrie_RepeatingPatterns_LargeText()
	{
		return LargeText.SearchContains(_kmpTriePatterns);
	}

	[Benchmark]
	public bool StandardTrie_SmallKeywordSet_LongText()
	{
		return _longText.SearchContains(_standardTrieSmall);
	}

	[Benchmark]
	public bool KmpTrie_SmallKeywordSet_LongText()
	{
		return _longText.SearchContains(_kmpTrieSmall);
	}

	[Benchmark]
	public bool StandardTrie_StartsWith_SmallKeywordSet()
	{
		// Test the StartsWith functionality
		return ("apple and orange").SearchStartsWith(_standardTrieSmall);
	}

	[Benchmark]
	public bool KmpTrie_StartsWith_SmallKeywordSet()
	{
		// Test the StartsWith functionality
		return ("apple and orange").SearchStartsWith(_kmpTrieSmall);
	}
}
