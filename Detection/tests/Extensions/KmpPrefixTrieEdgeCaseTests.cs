// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public class KmpPrefixTrieEdgeCaseTests
{
    [Fact]
    public void Unicode_Characters()
    {
        // Arrange
        var keywords = new[] { "こんにちは", "你好", "안녕하세요" };
        var source = "Hello 你好 World";
        var tree = new KmpPrefixTrie(keywords);
        
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
        
        var tree = new KmpPrefixTrie(keywords);
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
        var tree = new KmpPrefixTrie(keywords);
        
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
        var tree = new KmpPrefixTrie(keywords);
        
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
        var tree = new KmpPrefixTrie(keywords);
        
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
        var tree = new KmpPrefixTrie(keywords);
        
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
        var tree = new KmpPrefixTrie(keywords);
        
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
    
    // Additional KMP-specific tests that exercise the failure function
    
    [Fact]
    public void Repeating_Prefixes()
    {
        // Arrange - patterns with repeating prefixes exercise the KMP failure function
        var keywords = new[] { "abababc", "ababababd" };
        var tree = new KmpPrefixTrie(keywords);
        
        // Act & Assert
        var source = "abababababcxyz"; // Contains "abababc"
        Assert.True(tree.ContainsWithAnyIn(source));
        
        source = "ababababdxyz"; // Contains "ababababd" 
        Assert.True(tree.ContainsWithAnyIn(source));
        
        source = "abababab"; // Partial match that should trigger failure function jumps
        Assert.False(tree.ContainsWithAnyIn(source));
    }
    
    [Fact]
    public void Backtracking_Patterns()
    {
        // Arrange - these patterns require optimal backtracking in KMP
        var keywords = new[] { "AAACAAAA", "AAABAAA" };
        var tree = new KmpPrefixTrie(keywords);
        
        // Act & Assert
        var source = "AAACAAAAXYZ";
        Assert.True(tree.ContainsWithAnyIn(source));
        Assert.True(tree.StartsWithAnyIn(source));
        
        source = "AAABAAAXYZ";
        Assert.True(tree.ContainsWithAnyIn(source));
        Assert.True(tree.StartsWithAnyIn(source));
        
        // This should test the failure function behavior
        source = "AAABAAC";
        Assert.False(tree.ContainsWithAnyIn(source));
    }
    
    [Fact]
    public void Multiple_Matches_In_Source()
    {
        // Arrange - test KMP algorithm's ability to continue searching after a match
        var keywords = new[] { "abc", "def" };
        var tree = new KmpPrefixTrie(keywords);
        
        // Act & Assert
        var source = "abcdefabcdef"; // Contains both patterns multiple times
        Assert.True(tree.ContainsWithAnyIn(source));
        
        // Verify it finds a match even with text between keywords
        source = "abc---def";
        Assert.True(tree.ContainsWithAnyIn(source));
    }
    
    [Fact]
    public void Complex_Failure_Function_Test()
    {
        // This tests a complex pattern that requires sophisticated failure function handling
        var keywords = new[] { "ABABCABABCABDCABD" };
        var tree = new KmpPrefixTrie(keywords);
        
        // Exact match
        var source = "ABABCABABCABDCABD";
        Assert.True(tree.ContainsWithAnyIn(source));
        Assert.True(tree.StartsWithAnyIn(source));
        
        // Near miss (should cause complex failure function behavior)
        source = "ABABCABABCABDCABE";
        Assert.False(tree.ContainsWithAnyIn(source));
        
        // Double pattern with character in between
        source = "ABABCABABCABDCABD-ABABCABABCABDCABD";
        Assert.True(tree.ContainsWithAnyIn(source));
    }
    
    [Fact]
    public void High_Character_Distribution()
    {
        // Arrange - keywords with characters distributed across the ASCII range
        // This tests the offset calculations in the trie
        var keywords = new[]
        {
            "a" + (char)1 + "c",
            "a" + (char)200 + "c",
            "a" + (char)50 + "c",
            "a" + (char)150 + "c"
        };
        
        var tree = new KmpPrefixTrie(keywords);
        
        // Act & Assert
        var source = "xa" + (char)1 + "cy";
        Assert.True(tree.ContainsWithAnyIn(source));
        
        source = "xa" + (char)200 + "cy";
        Assert.True(tree.ContainsWithAnyIn(source));
        
        source = "a" + (char)50 + "c";
        Assert.True(tree.ContainsWithAnyIn(source));
        Assert.True(tree.StartsWithAnyIn(source));
        
        source = "a" + (char)151 + "c"; // Not in keywords
        Assert.False(tree.ContainsWithAnyIn(source));
    }
}