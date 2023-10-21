// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
namespace Wangkanai.Detection.Extensions;

public class IndexTreeTests
{
	[Fact]
	public void KeywordsIsNull()
	{
		string[] _null = null!;
		var      abc   = "abc";
		var      tree  = new IndexTree(_null);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsIsEmpty()
	{
		var _empty = Array.Empty<string>();
		var abc    = "abc";
		var tree   = new IndexTree(_empty);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsIsNull_SourceExist_SeedIsOne()
	{
		string[] _null = null!;
		var      abc   = "abc";
		var      tree  = new IndexTree(_null, 1);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsIsEmpty_SourceExist_SeedIsOne()
	{
		var _empty = Array.Empty<string>();
		var abc    = "abc";
		var tree   = new IndexTree(_empty, 1);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsZero()
	{
		var     keywords = new[] { "abc" };
		string? source   = null!;
		var     tree     = new IndexTree(keywords);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}
	
	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsOne()
	{
		var     keywords = new[] { "abc" };
		string? source   = null!;
		var     tree     = new IndexTree(keywords, 1);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}
	
	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsTwo()
	{
		var     keywords = new[] { "abc" };
		string? source   = null!;
		var     tree     = new IndexTree(keywords, 2);
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}
	
	[Fact]
	public void KeywordsExist_SourceIsNull_SeedIsThree()
	{
		var     keywords = new[] { "abc" };
		string? source   = null!;
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
		var abc      = "abc";
		var tree     = new IndexTree(keywords);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsOne()
	{
		var keywords = new[] { "abc" };
		var abc      = "abc";
		var tree     = new IndexTree(keywords, 1);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.False(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsTwo()
	{
		var keywords = new[] { "abc" };
		var abc      = "abc";
		var tree     = new IndexTree(keywords, 2);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.False(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void KeywordsExist_SourceExist_SeedIsThree()
	{
		var keywords = new[] { "abc" };
		var abc      = "abc";
		var tree     = new IndexTree(keywords, 3);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}
}