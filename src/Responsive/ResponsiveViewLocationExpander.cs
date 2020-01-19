// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Responsive
{
    /// <summary>
    /// a <see cref="IViewLocationExpander"/> that adds the responsive as an extension prefix to view names.
    /// device that is getting added as extensions prefix comes from <see cref="Microsoft.AspNetCore.Http.HttpContext"/>.
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
        private const string ValueKey = "device";
        private readonly ResponsiveViewLocationFormat _format;

        public ResponsiveViewLocationExpander() : this(ResponsiveViewLocationFormat.Suffix) { }

        public ResponsiveViewLocationExpander(ResponsiveViewLocationFormat format)
        {
            if (!Enum.IsDefined(typeof(ResponsiveViewLocationFormat), (int)format))
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

            context.Values.TryGetValue(ValueKey, out var value);

            if (string.IsNullOrEmpty(value))
                return viewLocations;

            Enum.TryParse(value, true, out Device device);

            return ExpandViewLocationsCore(viewLocations, device);
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, Device device)
        {
            foreach (var location in viewLocations)
            {
                if (location.ToLower().Contains("pages"))
                {
                    yield return location.Replace("{0}", "{0}." + device);
                    yield return location;
                }
                else
                {
                    if (_format == ResponsiveViewLocationFormat.Subfolder)
                        yield return location.Replace("{0}", device + "/{0}");
                    else
                        yield return location.Replace("{0}", "{0}." + device);
                    yield return location;
                }
            }
        }
    }
}
