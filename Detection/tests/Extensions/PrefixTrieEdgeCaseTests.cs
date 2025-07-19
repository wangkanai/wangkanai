// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public class PrefixTrieEdgeCaseTests
{
	[Fact]
	public void Unicode_Characters()
	{
		// Arrange
		var keywords = new[] { "こんにちは", "你好", "안녕하세요" };
		var source = "Hello 你好 World";
		var tree = new PrefixTrie(keywords);

		// Act & Assert
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));

		// Source starts with keyword
		source = "你好 World";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void Large_Branching_Factor()
	{
		// Arrange - create many words with same prefix to test branching
		var keywords = new string[100];
		for (int i = 0; i < 100; i++)
		{
			keywords[i] = $"prefix{i}";
		}

		var tree = new PrefixTrie(keywords);
		var source = "This contains prefix42 in the middle";

		// Act & Assert
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));

		source = "prefix99 is at the start";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void Overlapping_Keywords()
	{
		// Arrange
		var keywords = new[] { "apple", "pineapple", "pen", "applepen" };
		var tree = new PrefixTrie(keywords);

		// Act & Assert
		// Test for strings containing multiple matches
		var source = "I have a pen, I have an apple, applepen";
		Assert.True(tree.ContainsWithAnyIn(source));

		// Test for strings with overlapping matches
		source = "pineapple";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void Very_Long_Keywords()
	{
		// Arrange
		var longKeyword = new string('a', 1000) + "target" + new string('b', 1000);
		var keywords = new[] { longKeyword };
		var tree = new PrefixTrie(keywords);

		// Act & Assert
		// Test for partial match (should fail)
		var partialSource = new string('a', 1000) + "not-a-match" + new string('b', 1000);
		Assert.False(tree.ContainsWithAnyIn(partialSource));

		// Test for exact match
		Assert.True(tree.ContainsWithAnyIn(longKeyword));
		Assert.True(tree.StartsWithAnyIn(longKeyword));

		// Test for keyword embedded in longer string
		var embeddedSource = "prefix" + longKeyword + "suffix";
		Assert.True(tree.ContainsWithAnyIn(embeddedSource));
		Assert.False(tree.StartsWithAnyIn(embeddedSource));
	}

	[Fact]
	public void Boundary_Conditions()
	{
		// Arrange
		var keywords = new[] { "start", "end", "isolated" };
		var tree = new PrefixTrie(keywords);

		// Test match at the very beginning
		var source = "start of the string";
		Assert.True(tree.StartsWithAnyIn(source));

		// Test match at the very end
		source = "string at the end";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));

		// Test isolated match with boundaries
		source = "an isolated word in the middle";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void Special_Characters()
	{
		// Arrange
		var keywords = new[] { "c++", "c#", "f#", ".net", "node.js" };
		var tree = new PrefixTrie(keywords);

		// Act & Assert
		var source = "I program in c++ and c#";
		Assert.True(tree.ContainsWithAnyIn(source));

		source = ".net framework";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));

		source = "I prefer node.js for web development";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}

	[Fact]
	public void Mixed_Case_Sensitivity()
	{
		// Arrange - case should matter by default
		var keywords = new[] { "Case", "CASE", "case" };
		var tree = new PrefixTrie(keywords);

		// Act & Assert
		var source = "This is Case sensitive";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));

		source = "case matters";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));

		source = "CASE is different";
		Assert.True(tree.ContainsWithAnyIn(source));
		Assert.True(tree.StartsWithAnyIn(source));

		// Should not match due to case sensitivity
		source = "CasE is different spelling";
		Assert.False(tree.ContainsWithAnyIn(source));
		Assert.False(tree.StartsWithAnyIn(source));
	}
}
