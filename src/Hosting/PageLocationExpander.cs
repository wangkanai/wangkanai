using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsivePageLocationExpander : IViewLocationExpander{
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            throw new NotImplementedException();
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            throw new NotImplementedException();
        }
    }
}