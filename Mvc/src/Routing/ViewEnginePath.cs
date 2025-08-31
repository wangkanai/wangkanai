// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;
using System.Text;

using Microsoft.Extensions.Primitives;

namespace Wangkanai.Mvc.Routing;

internal static class ViewEnginePath
{
   private const          string CurrentDirectoryToken = ".";
   private const          string ParentDirectoryToken  = "..";
   public static readonly char[] PathSeparators        = new[] { '/', '\\' };

   public static string CombinePath(string first, string second)
   {
      Debug.Assert(!string.IsNullOrEmpty(first));

      if (second.StartsWith('/'))
      {
         return second;
      }

      string result;

      // Get directory name (including final slash) but do not use Path.GetDirectoryName() to preserve path
      // normalization.
      var index = first.LastIndexOf('/');
      Debug.Assert(index >= 0);

      if (index == first.Length - 1)
      {
         result = first + second;
      }
      else
      {
         result = string.Concat(first.AsSpan(0, index + 1), second);
      }

      return ResolvePath(result);
   }

   public static string ResolvePath(string path)
   {
      Debug.Assert(!string.IsNullOrEmpty(path));
      var pathSegment = new StringSegment(path);
      if (path[0] == PathSeparators[0] || path[0] == PathSeparators[1])
      {
         // Leading slashes (e.g. "/Views/Index.cshtml") always generate an empty first token. Ignore these
         // for purposes of resolution.
         pathSegment = pathSegment.Subsegment(1);
      }

      var tokenizer          = new StringTokenizer(pathSegment, PathSeparators);
      var requiresResolution = false;
      foreach (var segment in tokenizer)
      {
         if (segment.Length == 0                                             ||
             segment.Equals(ParentDirectoryToken,  StringComparison.Ordinal) ||
             segment.Equals(CurrentDirectoryToken, StringComparison.Ordinal))
         {
            requiresResolution = true;
            break;
         }
      }

      if (!requiresResolution)
      {
         return path;
      }

      var pathSegments = new List<StringSegment>();
      foreach (var segment in tokenizer)
      {
         if (segment.Length == 0)
         {
            continue; // Ignore multiple directory separators
         }

         if (segment.Equals(ParentDirectoryToken, StringComparison.Ordinal))
         {
            // Don't resolve the path if we ever escape the file system root. We can't reason about it in a consistent way.
            if (pathSegments.Count == 0)
            {
               return path;
            }

            pathSegments.RemoveAt(pathSegments.Count - 1);
         }
         else if (segment.Equals(CurrentDirectoryToken, StringComparison.Ordinal))
         {
            continue; // We already have the current directory
         }
         else
         {
            pathSegments.Add(segment);
         }
      }

      var builder = new StringBuilder();
      for (var i = 0; i < pathSegments.Count; i++)
      {
         var segment = pathSegments[i];
         builder.Append('/');
         builder.Append(segment.Buffer, segment.Offset, segment.Length);
      }

      return builder.ToString();
   }
}