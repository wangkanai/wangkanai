// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Extensions;

public class BinaryScalingExtensionsTests
{
   [Fact]
   public void ConvertsBytesToHumanReadableFormat()
   {
      var value    = 1024; // 1 KB in bytes
      var expected = "1 KB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertsLargeNumberToHumanReadableFormat()
   {
      var value    = 1048576; // 1 MB in bytes
      var expected = "1 MB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertsZeroBytesToHumanReadableFormat()
   {
      var value    = 0;
      var expected = "0 B";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertsNegativeNumber()
   {
      var value    = -1024;
      var expected = "-1 KB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertsNegativeLargeNumber()
   {
      var value    = -1048576;
      var expected = "-1 MB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertLongGiga()
   {
      var value    = 1024L * 1024L * 1024L; // 1 GB in bytes
      var expected = "1 GB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertLongTera()
   {
      var value    = 1024L * 1024L * 1024L * 1024L; // 1 TB in bytes
      var expected = "1 TB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertLongPeta()
   {
      var value    = 1024L * 1024L * 1024L * 1024L * 1024L; // 1 PB in bytes
      var expected = "1 PB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertLongExa()
   {
      var value    = 1024L * 1024L * 1024L * 1024L * 1024L * 1024L; // 1 EB in bytes
      var expected = "1 EB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertULongGiga()
   {
      var value    = 1024UL * 1024UL * 1024UL; // 1 GB in bytes
      var expected = "1 GB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertULongTera()
   {
      var value    = 1024UL * 1024UL * 1024UL * 1024UL; // 1 TB in bytes
      var expected = "1 TB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertULongPeta()
   {
      var value    = 1024UL * 1024UL * 1024UL * 1024UL * 1024UL; // 1 PB in bytes
      var expected = "1 PB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertULongExa()
   {
      var value    = 1024UL * 1024UL * 1024UL * 1024UL * 1024UL * 1024UL; // 1 EB in bytes
      var expected = "1 EB";
      Assert.Equal(expected, value.ToHumanReadable());
   }

   [Fact]
   public void ConvertShortKilo()
   {
      var value    = (short)1024; // 1 KB in bytes
      var expected = "1 KB";
      Assert.Equal(expected, value.ToHumanReadable());
   }
}