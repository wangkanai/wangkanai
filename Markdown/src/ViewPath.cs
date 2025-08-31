// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown;

internal static class ViewPath
{
   public static string NormalizePath(string path)
   {
      var addLeadingSlash  = path[0] != '\\' && path[0] != '/';
      var transformSlashes = path.IndexOf('\\') != -1;

      if (!addLeadingSlash && !transformSlashes)
      {
         return path;
      }

      var length = path.Length;
      if (addLeadingSlash)
      {
         length++;
      }

      return string.Create(length, (path, addLeadingSlash), (span, tuple) =>
      {
         var (pathValue, addLeadingSlashValue) = tuple;
         var spanIndex = 0;

         if (addLeadingSlashValue)
            span[spanIndex++] = '/';

         foreach (var ch in pathValue)
            span[spanIndex++] = ch == '\\' ? '/' : ch;
      });
   }
}