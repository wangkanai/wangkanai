// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Extensions;

namespace Wangkanai.Detection;

[MemoryDiagnoser]
public class TrieBenchmark
{
   // Search targets
   private const    string        SmallText = "I like to eat apples and bananas";
   private const    string        LargeText = "This is a longer text that contains multiple words to search for including apple, orange and many other terms that might not be in our dictionary";
   private readonly KmpPrefixTrie _kmpTrieLarge;
   private readonly KmpPrefixTrie _kmpTriePatterns;
   private readonly KmpPrefixTrie _kmpTrieSmall;
   private readonly string[]      _largeKeywordSet;
   private readonly string        _longText;

   private readonly string[] _repeatingPatternKeywords;

   // Test data
   private readonly string[]   _smallKeywordSet = { "apple", "banana", "orange", "pear", "grape" };
   private readonly PrefixTrie _standardTrieLarge;
   private readonly PrefixTrie _standardTriePatterns;

   // Trie instances
   private readonly PrefixTrie _standardTrieSmall;

   public TrieBenchmark()
   {
      // Initialize large keyword set (100 keywords)
      _largeKeywordSet = new string[100];
      for (var i = 0; i < 100; i++)
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
      _longText = new('x', 10000);

      // Initialize tries
      _standardTrieSmall    = new(_smallKeywordSet);
      _kmpTrieSmall         = new(_smallKeywordSet);
      _standardTrieLarge    = new(_largeKeywordSet);
      _kmpTrieLarge         = new(_largeKeywordSet);
      _standardTriePatterns = new(_repeatingPatternKeywords);
      _kmpTriePatterns      = new(_repeatingPatternKeywords);
   }

   [Benchmark]
   public bool StandardTrie_SmallKeywordSet_SmallText() => SmallText.SearchContains(_standardTrieSmall);

   [Benchmark]
   public bool KmpTrie_SmallKeywordSet_SmallText() => SmallText.SearchContains(_kmpTrieSmall);

   [Benchmark]
   public bool StandardTrie_LargeKeywordSet_LargeText() => LargeText.SearchContains(_standardTrieLarge);

   [Benchmark]
   public bool KmpTrie_LargeKeywordSet_LargeText() => LargeText.SearchContains(_kmpTrieLarge);

   [Benchmark]
   public bool StandardTrie_RepeatingPatterns_LargeText() => LargeText.SearchContains(_standardTriePatterns);

   [Benchmark]
   public bool KmpTrie_RepeatingPatterns_LargeText() => LargeText.SearchContains(_kmpTriePatterns);

   [Benchmark]
   public bool StandardTrie_SmallKeywordSet_LongText() => _longText.SearchContains(_standardTrieSmall);

   [Benchmark]
   public bool KmpTrie_SmallKeywordSet_LongText() => _longText.SearchContains(_kmpTrieSmall);

   [Benchmark]
   public bool StandardTrie_StartsWith_SmallKeywordSet() =>
      // Test the StartsWith functionality
      "apple and orange".SearchStartsWith(_standardTrieSmall);

   [Benchmark]
   public bool KmpTrie_StartsWith_SmallKeywordSet() =>
      // Test the StartsWith functionality
      "apple and orange".SearchStartsWith(_kmpTrieSmall);
}