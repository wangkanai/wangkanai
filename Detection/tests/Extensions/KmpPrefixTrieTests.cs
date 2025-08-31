// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Detection.Extensions;

public class KmpPrefixTrieTests : BasePrefixTrieTests<KmpPrefixTrie>
{
   protected override KmpPrefixTrie Create(string[]? keywords) => new(keywords);

   [Fact]
   public void KeywordsIsNull_Test() => KeywordsIsNull();

   [Fact]
   public void KeywordsIsEmpty_Test() => KeywordsIsEmpty();

   [Fact]
   public void KeywordsExist_SourceIsNull_SeedIsZero_Test() => KeywordsExist_SourceIsNull_SeedIsZero();

   [Fact]
   public void KeywordsExist_SourceIsEmpty_SeedIsZero_Test() => KeywordsExist_SourceIsEmpty_SeedIsZero();

   [Fact]
   public void KeywordsExist_SourceIsWhiteSpace_SeedIsZero_Test() => KeywordsExist_SourceIsWhiteSpace_SeedIsZero();

   [Fact]
   public void KeywordsExist_SourceExist_SeedIsZero_Test() => KeywordsExist_SourceExist_SeedIsZero();

   [Fact]
   public void KeywordsExist_SourceSubset_SeedIsZero_Test() => KeywordsExist_SourceSubset_SeedIsZero();

   [Fact]
   public void KeywordsExist_SourceNotSub_SeedIsZero_Test() => KeywordsExist_SourceNotSub_SeedIsZero();
}