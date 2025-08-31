// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

// ReSharper disable InconsistentNaming

namespace Wangkanai.Detection.Extensions;

public abstract class BasePrefixTrieTests<T>
   where T : IPrefixTrie
{
   protected abstract T Create(string[]? keywords);

   protected void KeywordsIsNull()
   {
      string[] _null  = null!;
      var      source = "abc";
      var      tree   = Create(_null);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsIsEmpty()
   {
      var empty  = Array.Empty<string>();
      var source = "abc";
      var tree   = Create(empty);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsExist_SourceIsNull_SeedIsZero()
   {
      var    keywords = new[] { "abc" };
      string source   = null!;
      var    tree     = Create(keywords);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsExist_SourceIsEmpty_SeedIsZero()
   {
      var keywords = new[] { "abc" };
      var source   = string.Empty;
      var tree     = Create(keywords);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsExist_SourceIsWhiteSpace_SeedIsZero()
   {
      var keywords = new[] { "abc" };
      var source   = " ";
      var tree     = Create(keywords);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsExist_SourceExist_SeedIsZero()
   {
      var keywords = new[] { "abc" };
      var source   = "abc";
      var tree     = Create(keywords);
      Assert.True(tree.ContainsWithAnyIn(source));
      Assert.True(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsExist_SourceSubset_SeedIsZero()
   {
      var keywords = new[] { "abc" };
      var source   = "a";
      var tree     = Create(keywords);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }

   protected void KeywordsExist_SourceNotSub_SeedIsZero()
   {
      var keywords = new[] { "abc" };
      var source   = "d";
      var tree     = Create(keywords);
      Assert.False(tree.ContainsWithAnyIn(source));
      Assert.False(tree.StartsWithAnyIn(source));
   }
}