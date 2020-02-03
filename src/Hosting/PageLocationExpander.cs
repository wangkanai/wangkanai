using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsivePageLocationExpander : IViewLocationExpander
    {
        private const string ValueKey = "device-page";
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context is null) 
                throw new ArgumentNullException(nameof(context));

            context.Values[ValueKey] = context.ActionContext.HttpContext.GetDevice().ToString();
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context is null) 
                throw new ArgumentNullException(nameof(context));
            if (viewLocations is null) 
                throw new ArgumentNullException(nameof(viewLocations));

            context.Values.TryGetValue(ValueKey, out var value);

            if (string.IsNullOrEmpty(value))
                return viewLocations;

            Enum.TryParse(value, true, out Device device);

            viewLocations = viewLocations.Where(l => l.Contains("pages", StringComparison.OrdinalIgnoreCase));
            
            return ExpandViewLocationCore(viewLocations, device);
        }

        private IEnumerable<string> ExpandViewLocationCore(IEnumerable<string> viewLocations, Device device)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Replace("{0}", "{0}." + device);
                yield return location;
            }
        }
    }
}