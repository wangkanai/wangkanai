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

            if (!(context.ActionContext.ActionDescriptor is PageActionDescriptor)) 
                return viewLocations;
            
            if (string.IsNullOrEmpty(context.PageName)) 
                return viewLocations;

            var expandLocations = ExpandPageHierarchy().ToList();
            return expandLocations;
            

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
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values[ValueKey] = context.ActionContext.HttpContext.GetDevice().ToString().ToLower();
        }
    }
}