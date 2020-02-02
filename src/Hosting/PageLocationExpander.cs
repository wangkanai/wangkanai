using System;
using System.Collections.Generic;
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
            if (context == null) 
                throw new ArgumentNullException(nameof(context));

            context.Values[ValueKey] = context.ActionContext.HttpContext.GetDevice().ToString();
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null) 
                throw new ArgumentNullException(nameof(context));
            if (viewLocations == null) 
                throw new ArgumentNullException(nameof(viewLocations));

            context.Values.TryGetValue(ValueKey, out var value);

            if (string.IsNullOrEmpty(value))
                return viewLocations;

            Enum.TryParse(value, true, out Device device);

            // What should I manipulate? 
            return viewLocations;
        }
    }
}