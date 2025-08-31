// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions.Strings;

public class StringEnsureEndTests
{
   [Fact]
   public void EnsureEndsWith_UseCases()
   {
      // Expected use-cases
      Assert.Equal("Test!", "Test".EnsureEndsWith('!'));
      Assert.Equal("Test!", "Test!".EnsureEndsWith('!'));

      Assert.Equal(@"c:\test\project\", @"c:\test\project".EnsureEndsWith('\\'));
      Assert.Equal(@"c:\test\project\", @"c:\test\project\".EnsureEndsWith('\\'));
   }

   [Fact]
   public void EnsureEndsWith_CaseDifference()
   {
      // Case differences
      Assert.Equal("TurkeYy", "TurkeY".EnsureEndsWith('y'));
      Assert.Equal("TurkeY",  "TurkeY".EnsureEndsWith('y', StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void EnsureEndsWith_EdgeCaseForTurkish()
   {
      // Edge cases for Turkish 'i'
      Assert.Equal("TAKSİ",  "TAKSİ".EnsureEndsWith('i', true,  new("tr-TR")));
      Assert.Equal("TAKSİi", "TAKSİ".EnsureEndsWith('i', false, new("tr-TR")));
   }
}