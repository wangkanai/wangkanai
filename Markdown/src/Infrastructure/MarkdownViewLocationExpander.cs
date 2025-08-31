// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown.Infrastructure;

public class MarkdownViewLocationExpander : IMarkdownViewLocationExpander
{
   public IEnumerable<string> ExpandViewLocations(MarkdownViewLocationExpanderContext context, IEnumerable<string> viewLocations)
   {
      if (context.ActionContext.ActionDescriptor is MarkdownActionDescriptor && !string.IsNullOrEmpty(context.PageName))
      {
         return ExpandPageHierarchy();
      }

      // Not a page - just act natural.
      return viewLocations;

      IEnumerable<string> ExpandPageHierarchy()
      {
         foreach (var location in viewLocations)
         {
            // For pages, we only handle the 'page' token when it's surrounded by slashes.
            //
            // Explanation:
            //      We need the ability to 'collapse' the segment which requires us to understand slashes.
            //      Imagine a path like /{1}/{0} - we might end up with //{0} if we don't do *something* with
            //      the slashes. Instead of picking on (leading or trailing), we choose both. This seems
            //      less arbitrary.
            //
            //
            // So given a Page like /Account/Manage/Index using /Pages as the root, and the default set of
            // search paths, this will produce the expanded paths:
            //
            //  /Pages/Account/Manage/{0}.cshtml
            //  /Pages/Account/{0}.cshtml
            //  /Pages/{0}.cshtml
            //  /Views/Shared/{0}.cshtml

            if (!location.Contains("/{1}/"))
            {
               // If the location doesn't have the 'page' replacement token just return it as-is.
               yield return location;
               continue;
            }

            // For locations with the 'page' token - expand them into an ascending directory search,
            // but only up to the pages root.
            //
            // This is easy because the 'page' token already trims the root directory.
            var end = context.PageName.Length;

            // PageName always starts with `/`
            while (end > 0 && (end = context.PageName.LastIndexOf('/', end              - 1)) != -1)
               yield return location.Replace("/{1}/", context.PageName.Substring(0, end + 1));
         }
      }
   }

   /// <inheritdoc/>
   public void PopulateValues(MarkdownViewLocationExpanderContext context)
   {
      // The value we care about - 'page' is already part of the system. We don't need to add it manually.
   }
}