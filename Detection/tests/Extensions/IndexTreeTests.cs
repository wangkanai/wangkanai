// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

// ReSharper disable InconsistentNaming
namespace Wangkanai.Detection.Extensions;

public class IndexTreeTests
{
	[Fact]
	public void KeywordsIsNull()
	{
		string[] _null  = null!;
		var      source = "abc";
		var      tree   = new IndexTree(_null);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsIsEmpty()
	{
		var empty  = Array.Empty<string>();
		var source = "abc";
		var tree   = new IndexTree(empty);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsIsNull_SourceExist_SeedIsOne()
	{
		string[] _null  = null!;
		var      source = "abc";
		var      tree   = new IndexTree(_null, 1);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsIsEmpty_SourceExist_SeedIsOne()
	{
		var empty  = Array.Empty<string>();
		var source = "abc";
		var tree   = new IndexTree(empty, 1);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsZero()
	{
		var    keywords = new[] { "abc" };
		string source   = null!;
		var    tree     = new IndexTree(keywords);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsOne()
	{
		var    keywords = new[] { "abc" };
		string source   = null!;
		var    tree     = new IndexTree(keywords, 1);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsTwo()
	{
		var     keywords = new[] { "abc" };
		string source   = null!;
		var     tree     = new IndexTree(keywords, 2);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsThree()
	{
		var     keywords = new[] { "abc" };
		string source   = null!;
		var     tree     = new IndexTree(keywords, 3);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsEmpty_SeedIsZero()
	{
		var keywords = new[] { "abc" };
		var source   = string.Empty;
		var tree     = new IndexTree(keywords);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsEmpty_SeedIsOne()
	{
		var keywords = new[] { "abc" };
		var source   = string.Empty;
		var tree     = new IndexTree(keywords, 1);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsEmpty_SeedIsTwo()
	{
		var keywords = new[] { "abc" };
		var source   = string.Empty;
		var tree     = new IndexTree(keywords, 2);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsEmpty_SeedIsThree()
	{
		var keywords = new[] { "abc" };
		var source   = string.Empty;
		var tree     = new IndexTree(keywords, 3);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsWhiteSpace_SeedIsZero()
	{
		var keywords = new[] { "abc" };
		var source   = " ";
		var tree     = new IndexTree(keywords);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsWhiteSpace_SeedIsOne()
	{
		var keywords = new[] { "abc" };
		var source   = " ";
		var tree     = new IndexTree(keywords, 1);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsWhiteSpace_SeedIsTwo()
	{
		var keywords = new[] { "abc" };
		var source   = " ";
		var tree     = new IndexTree(keywords, 2);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceIsWhiteSpace_SeedIsThree()
	{
		var keywords = new[] { "abc" };
		var source   = " ";
		var tree     = new IndexTree(keywords, 3);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsZero()
	{
		var keywords = new[] { "abc" };
		var source   = "abc";
		var tree     = new IndexTree(keywords);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsOne()
	{
		var keywords = new[] { "abc" };
		var source   = "abc";
		var tree     = new IndexTree(keywords, 1);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsTwo()
	{
		var keywords = new[] { "abc" };
		var source   = "abc";
		var tree     = new IndexTree(keywords, 2);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsThree()
	{
		var keywords = new[] { "abc" };
		var source   = "abc";
		var tree     = new IndexTree(keywords, 3);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceSubset_SeedIsZero()
	{
		var keywords = new[] { "abc" };
		var source   = "a";
		var tree     = new IndexTree(keywords);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceSubset_SeedIsOne()
	{
		var keywords = new[] { "abc" };
		var source   = "a";
		var tree     = new IndexTree(keywords, 1);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceSubset_SeedIsTwo()
	{
		var keywords = new[] { "abc" };
		var source   = "a";
		var tree     = new IndexTree(keywords, 2);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceSubset_SeedIsThree()
	{
		var keywords = new[] { "abc" };
		var source   = "a";
		var tree     = new IndexTree(keywords, 3);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceNotSub_SeedIsZero()
	{
		var keywords = new[] { "abc" };
		var source   = "d";
		var tree     = new IndexTree(keywords);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceNotSub_SeedIsOne()
	{
		var keywords = new[] { "abc" };
		var source   = "d";
		var tree     = new IndexTree(keywords, 1);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceNotSub_SeedIsTwo()
	{
		var keywords = new[] { "abc" };
		var source   = "d";
		var tree     = new IndexTree(keywords, 2);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void KeywordsExist_SourceNotSub_SeedIsThree()
	{
		var keywords = new[] { "abc" };
		var source   = "d";
		var tree     = new IndexTree(keywords, 3);
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}
}
