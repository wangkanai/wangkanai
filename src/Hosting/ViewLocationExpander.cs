// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Wangkanai.Detection.Hosting
{
    /// <summary>
    /// A <see cref="IViewLocationExpander" /> that adds the responsive as an extension prefix to view names.
    /// device that is getting added as extensions prefix comes from <see cref="Microsoft.AspNetCore.Http.HttpContext" />.
    /// </summary>
    /// <example>
    /// For the default case with no areas, views are generated with the following patterns
    /// (assuming controller is "Home", action is "Index" and device is "mobile")
    /// Views/Home/mobile/Action
    /// Views/Home/Action
    /// Views/Shared/mobile/Action
    /// Views/Shared/Action
    /// </example>
    public class ResponsiveViewLocationExpander : IViewLocationExpander
    {
        private const    string                       ValueKey = "device";
        private readonly ResponsiveViewLocationFormat _format;

        public ResponsiveViewLocationExpander(ResponsiveViewLocationFormat format)
        {
            if (!Enum.IsDefined(typeof(ResponsiveViewLocationFormat), (int) format))
                throw new InvalidEnumArgumentException(nameof(format));

            _format = format;
        }

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

            context.Values.TryGetValue(ValueKey, out var device);

            if (string.IsNullOrEmpty(device))
                return viewLocations;

            var filterLocations = ViewOnly(viewLocations);
            var resultLocations = ExpandViewLocationsCore(filterLocations, device).Concat(viewLocations);

            return resultLocations;
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, string device)
        {
            foreach (var location in viewLocations)
            {
                yield return _format == ResponsiveViewLocationFormat.Suffix
                                 ? location.Replace("{0}", "{0}." + device)
                                 : location.Replace("{0}", device + "/{0}");
                yield return location;
            }
        }

        private static IEnumerable<string> ViewOnly(IEnumerable<string> viewLocations)
            => viewLocations.Where(location => location.Contains("views", StringComparison.OrdinalIgnoreCase));

        private static IEnumerable<string> PageOnly(IEnumerable<string> viewLocations)
            => viewLocations.Where(location => location.Contains("pages", StringComparison.OrdinalIgnoreCase));
    }
}