// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Security.Cryptography;
using System.Text;

namespace Wangkanai.Cryptography;

public static class Hash
{
   public static string HashMd5(this string value)
      => MD5.HashData(value.GetAsciiBytes())
            .HashDataToString();

   public static string HashSha512(this string value)
      => SHA512.HashData(value.GetAsciiBytes())
               .HashDataToString();

   public static string HashSha384(this string value)
      => SHA384.HashData(value.GetAsciiBytes())
               .HashDataToString();

   public static string HashSha256(this string value)
      => SHA256.HashData(value.GetAsciiBytes())
               .HashDataToString();

   #region Internal

   private static byte[] GetAsciiBytes(this string value)
      => Encoding.ASCII.GetBytes(value.ThrowIfNull().ThrowIfEmpty().ThrowIfWhitespace());

   private static string HashDataToString(this IEnumerable<byte> data)
   {
      var builder = new StringBuilder();
      foreach (var index in data)
         builder.Append(index.ToString("x2"));

      return builder.ToString();
   }

   #endregion
}