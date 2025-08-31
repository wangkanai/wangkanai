// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown;

internal readonly struct MarkdownViewLocationCacheKey : IEquatable<MarkdownViewLocationCacheKey>
{
   public string  ViewName       { get; }
   public string? ControllerName { get; }
   public string? AreaName       { get; }
   public string? PageName       { get; }
   public bool    IsMainPage     { get; }

   public IReadOnlyDictionary<string, string?>? ViewLocationExpanderValues { get; }

   public MarkdownViewLocationCacheKey(
      string viewName,
      bool   isMainPage)
      : this(viewName, null, null, null, isMainPage, null) { }

   public MarkdownViewLocationCacheKey(
      string                                viewName,
      string?                               controllerName,
      string?                               areaName,
      string?                               pageName,
      bool                                  isMainPage,
      IReadOnlyDictionary<string, string?>? values)
   {
      ViewName                   = viewName;
      ControllerName             = controllerName;
      AreaName                   = areaName;
      PageName                   = pageName;
      IsMainPage                 = isMainPage;
      ViewLocationExpanderValues = values;
   }


   /// <inheritdoc/>
   public bool Equals(MarkdownViewLocationCacheKey y)
   {
      if (IsMainPage != y.IsMainPage                                                 ||
          !string.Equals(ViewName,       y.ViewName,       StringComparison.Ordinal) ||
          !string.Equals(ControllerName, y.ControllerName, StringComparison.Ordinal) ||
          !string.Equals(AreaName,       y.AreaName,       StringComparison.Ordinal) ||
          !string.Equals(PageName,       y.PageName,       StringComparison.Ordinal))
      {
         return false;
      }

      if (ReferenceEquals(ViewLocationExpanderValues, y.ViewLocationExpanderValues))
      {
         return true;
      }

      if (ViewLocationExpanderValues       == null ||
          y.ViewLocationExpanderValues     == null ||
          ViewLocationExpanderValues.Count != y.ViewLocationExpanderValues.Count)
      {
         return false;
      }

      foreach (var item in ViewLocationExpanderValues)
      {
         if (!y.ViewLocationExpanderValues.TryGetValue(item.Key, out var yValue) ||
             !string.Equals(item.Value, yValue, StringComparison.Ordinal))
         {
            return false;
         }
      }

      return true;
   }

   /// <inheritdoc/>
   public override bool Equals(object? obj)
      => obj is MarkdownViewLocationCacheKey cacheKey && Equals(cacheKey);

   /// <inheritdoc/>
   public override int GetHashCode()
   {
      var hashCode = new HashCode();
      hashCode.Add(IsMainPage ? 1 : 0);
      hashCode.Add(ViewName,       StringComparer.Ordinal);
      hashCode.Add(ControllerName, StringComparer.Ordinal);
      hashCode.Add(AreaName,       StringComparer.Ordinal);
      hashCode.Add(PageName,       StringComparer.Ordinal);

      if (ViewLocationExpanderValues != null)
      {
         foreach (var item in ViewLocationExpanderValues)
         {
            hashCode.Add(item.Key,   StringComparer.Ordinal);
            hashCode.Add(item.Value, StringComparer.Ordinal);
         }
      }

      return hashCode.ToHashCode();
   }
}