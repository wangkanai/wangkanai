// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Collections.Generic;
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

            if (string.IsNullOrEmpty(context.PageName) || string.IsNullOrEmpty(device))
                return viewLocations;
            
            return ExpandPageHierarchy();

            IEnumerable<string> ExpandPageHierarchy()
            {
                foreach (var location in viewLocations)
                {
                    if (!location.Contains("/{1}/") 
                        && !location.Contains("/Shared/") 
                        && !location.Contains("/Areas/")
                        || location.Contains("/Views/"))
                    {
                        yield return location;
                        continue;
                    }

                    // Device View if exist on disk
                    yield return location.Replace("{0}", "{0}." + device);
                    // Fallback to the original default view
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