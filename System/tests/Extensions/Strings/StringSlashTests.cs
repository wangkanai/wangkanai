// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions.Strings;

public class StringSlashTests
{
   private readonly string  _empty = string.Empty;
   private readonly string  _end   = "end";
   private readonly string? _null  = null;

   private readonly string _slash = "/";
   private readonly string _space = " ";
   private readonly string _start = "start";

   // Ensure leading slash

   [Fact] public void EnsureLeadingNoSlash() => Assert.Equal("/start", _start.EnsureLeadingSlash());

   [Fact]
   public void EnsureLeadingHasSlash()
   {
      var value = _slash + _start;
      Assert.Equal("/start", value.EnsureLeadingSlash());
   }

   [Fact]
   public void EnsureLeadingDoNothingWhenNull()
   {
      Assert.Null(_null!.EnsureLeadingSlash());
      Assert.Empty(_empty.EnsureLeadingSlash());
      Assert.Equal(_space, _space.EnsureLeadingSlash());
   }

   // Ensure trailing slash

   [Fact]
   public void EnsureTrailingHasSlash()
   {
      var value = _end + _slash;
      Assert.Equal("end/", value.EnsureTrailingSlash());
   }

   [Fact]
   public void EnsureTrailingNoSlash() => Assert.Equal("end/", _end.EnsureTrailingSlash());

   [Fact]
   public void EnsureTrailingDoNothingWhenNull()
   {
      Assert.Null(_null!.EnsureTrailingSlash());
      Assert.Empty(_empty.EnsureTrailingSlash());
      Assert.Equal(_space, _space.EnsureTrailingSlash());
   }

   // Remove leading slash

   [Fact] public void RemoveLeadingNoSlash()   => Assert.Equal(_start, _start.RemoveLeadingSlash());
   [Fact] public void RemoveTrailingNoSlash()  => Assert.Equal(_end,   _end.RemoveTrailingSlash());
   [Fact] public void RemoveTrailingHasSlash() => Assert.Equal(_end,   (_end + _slash).RemoveTrailingSlash());

   [Fact]
   public void RemoveLeadingHasSlash()
   {
      var value = _slash + _start;
      Assert.Equal(_start, value.RemoveLeadingSlash());
   }


   [Fact]
   public void RemoveLeadingDoNothingWhenNull()
   {
      Assert.Null(_null!.RemoveLeadingSlash());
      Assert.Empty(_empty.RemoveLeadingSlash());
      Assert.Equal(_space, _space.RemoveLeadingSlash());
   }

   [Fact]
   public void RemoveTrailingDoNothingWhenNull()
   {
      Assert.Null(_null!.RemoveTrailingSlash());
      Assert.Empty(_empty.RemoveTrailingSlash());
      Assert.Equal(_space, _space.RemoveTrailingSlash());
   }
}