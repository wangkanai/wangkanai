// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Detection.Models;

public class UserAgentTest
{
   [Fact]
   public void Ctor_Default_Success()
   {
      var userAgent = new UserAgent();

      Assert.NotNull(userAgent.ToString());
      Assert.Equal(string.Empty, userAgent.ToString());
   }

   [Fact]
   public void Ctor_String_Success()
   {
      var name      = "Agent";
      var userAgent = new UserAgent(name);

      Assert.Equal(name, userAgent.ToString());
   }

   [Fact]
   public void Ctor_Null_Success()
   {
      var userAgent = new UserAgent(null!);

      Assert.NotNull(userAgent.ToString());
   }
}