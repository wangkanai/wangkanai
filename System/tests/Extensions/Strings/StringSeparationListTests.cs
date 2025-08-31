// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringSeparationListTests
{
   private readonly List<string> _emptyList = new();
   private readonly List<string> _nullList  = null;
   private readonly List<string> _spaceList = new() { " " };

   [Fact] public void ToSpaceNull()  => Assert.Throws<ArgumentNullException>(() => _nullList.SeparateToSpace());
   [Fact] public void ToSpaceEmpty() => Assert.Throws<ArgumentEmptyException>(() => _emptyList.SeparateToSpace());
   [Fact] public void ToSpaceSpace() => Assert.Equal(" ", _spaceList.SeparateToSpace());

   [Fact]
   public void ToSpaceAlphabet()
   {
      var list = new List<string> { "a", "b", "c" };
      Assert.Equal("a b c", list.SeparateToSpace());
   }

   [Fact]
   public void ToSpaceNumber()
   {
      var list = new List<string> { "1", "2", "3" };
      Assert.Equal("1 2 3", list.SeparateToSpace());
   }

   [Fact]
   public void ToSpaceMix()
   {
      var list = new List<string> { "a", "1", "b", "2", "c", "3" };
      Assert.Equal("a 1 b 2 c 3", list.SeparateToSpace());
   }
}