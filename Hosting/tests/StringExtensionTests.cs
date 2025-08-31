// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Hosting;

public class StringExtensionTests
{
   [Fact]
   public void UrlIsNull()
   {
      string url = null!;
      Assert.Equal("/", url.CleanUrlPath());
   }

   [Fact]
   public void UrlIsEmpty()
   {
      var url = string.Empty;
      Assert.Equal("/", url.CleanUrlPath());
   }

   [Fact]
   public void UrlIsWhiteSpace()
   {
      var url = " ";
      Assert.Equal("/", url.CleanUrlPath());
   }

   [Fact]
   public void UrlIsSlash()
   {
      var url = "/";
      Assert.Equal("/", url.CleanUrlPath());
   }

   [Fact]
   public void UrlIsSlashEnd()
   {
      var url = "/test/";
      Assert.Equal("/test", url.CleanUrlPath());
   }
}