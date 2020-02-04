using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsivePageLocationExpander : IViewLocationExpander
    {
        private const string ValueKey = "device";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue(ValueKey, out var device);

            if (context.ActionContext.ActionDescriptor is PageActionDescriptor
                && !string.IsNullOrEmpty(context.PageName))
            {
                var expandLocations = ExpandPageHierarchy().ToList();
                return expandLocations;
            }

            // Not a page - just act natural.
            return viewLocations;

            IEnumerable<string> ExpandPageHierarchy()
            {
                foreach (var location in viewLocations)
                {
                    if (!location.Contains("/Shared/") && !location.Contains("/{1}/") || location.Contains("/Views/"))
                    {
                        yield return location;
                        continue;
                    }

                    yield return location.Replace("{0}", "{0}." + device);
                    yield return location;
                }
            }
            
            static string ReplacePageName(ViewLocationExpanderContext context, string location, int end) 
                => location.Replace("/{1}/", context.PageName.Substring(0, end + 1));
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[ValueKey] = context.ActionContext.HttpContext.GetDevice().ToString().ToLower();
        }
    }
}