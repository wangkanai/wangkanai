// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Exceptions;

namespace Wangkanai.Extensions.Strings;

public class StringSeparationSpaceTests
{
   private readonly string  _emptyString = string.Empty;
   private readonly string? _nullString  = null;
   private readonly string  _spaceString = " ";

   [Fact] public void FromSpaceNull()  => Assert.Throws<ArgumentNullException>(() => _nullString!.SeparateFromSpace());
   [Fact] public void FromSpaceEmpty() => Assert.Throws<ArgumentEmptyException>(() => _emptyString.SeparateFromSpace());
   [Fact] public void FromSpaceSpace() => Assert.Equal(new List<string>(), _spaceString.SeparateFromSpace());

   [Fact]
   public void FromSpaceAlphabet()
   {
      var list = new List<string> { "a", "b", "c" };
      Assert.Equal(list, "a b c".SeparateFromSpace());
   }

   [Fact]
   public void FromSpaceNumber()
   {
      var list = new List<string> { "1", "2", "3" };
      Assert.Equal(list, "1 2 3".SeparateFromSpace());
   }

   [Fact]
   public void FromSpaceMix()
   {
      var list = new List<string> { "a", "1", "b", "2", "c", "3" };
      Assert.Equal(list, "a 1 b 2 c 3".SeparateFromSpace());
   }
}