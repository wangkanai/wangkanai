// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions.Strings;

public class ReplaceOrdinalTests
{
   private readonly string  _empty = string.Empty;
   private readonly string? _null  = null;
   private readonly string  _space = " ";
   private readonly string  _text  = "abcde";

   [Fact]
   public void ReplaceOrdinalSingle()
   {
      Assert.Equal("bcde", _text.ReplaceOrdinal("a"));
      Assert.Equal("acde", _text.ReplaceOrdinal("b"));
      Assert.Equal("abde", _text.ReplaceOrdinal("c"));
      Assert.Equal("abce", _text.ReplaceOrdinal("d"));
      Assert.Equal("abcd", _text.ReplaceOrdinal("e"));
   }

   [Fact]
   public void ReplaceOrdinalTwo()
   {
      Assert.Equal("cde", _text.ReplaceOrdinal("ab"));
      Assert.Equal("ade", _text.ReplaceOrdinal("bc"));
      Assert.Equal("abe", _text.ReplaceOrdinal("cd"));
      Assert.Equal("abc", _text.ReplaceOrdinal("de"));
   }

   [Fact]
   public void ReplaceOrdinalRandom()
   {
      Assert.Equal("abcde", _text.ReplaceOrdinal("ac"));
      Assert.Equal("abcde", _text.ReplaceOrdinal("bd"));
      Assert.Equal("abcde", _text.ReplaceOrdinal("ce"));
   }

   [Fact]
   public void ReplaceOrdinalNull()
   {
      Assert.Throws<ArgumentNullException>(() => _null!.ReplaceOrdinal("a"));
      Assert.Throws<ArgumentNullException>(() => _null!.ReplaceOrdinal("ab"));
      Assert.Throws<ArgumentNullException>(() => _null!.ReplaceOrdinal("abc"));
   }

   [Fact]
   public void ReplaceOrdinalEmpty()
   {
      Assert.Equal(_empty, _empty.ReplaceOrdinal("a"));
      Assert.Equal(_empty, _empty.ReplaceOrdinal("ab"));
      Assert.Equal(_empty, _empty.ReplaceOrdinal("abc"));
   }

   [Fact]
   public void ReplaceOrdinalSpace()
   {
      Assert.Equal(_space, _space.ReplaceOrdinal("a"));
      Assert.Equal(_space, _space.ReplaceOrdinal("ab"));
      Assert.Equal(_space, _space.ReplaceOrdinal("abc"));
   }
}