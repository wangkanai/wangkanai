// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Wangkanai.Detection;
using System.ComponentModel;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// a <see cref="IViewLocationExpander"/> that adds the lanuage as an extension prefix to view names.
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
        private const string DEVICE_KEY = "device";
        private readonly ResponsiveViewLocationFormat _format;

        public ResponsiveViewLocationExpander() : this(ResponsiveViewLocationFormat.Suffix)
        {

        }
        public ResponsiveViewLocationExpander(ResponsiveViewLocationFormat format)
        {
            if (!Enum.IsDefined(typeof(ResponsiveViewLocationFormat), (int)format))
                throw new InvalidEnumArgumentException(nameof(format));

            _format = format;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context == null) throw new ViewLocationExpanderPopulateValuesArgumentNullException(nameof(context));

            context.Values[DEVICE_KEY] = context.ActionContext.HttpContext.GetDevice().Preferred;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null) throw new ViewLocationExpanderContextArgumentNullException(nameof(context));
            if (viewLocations == null) throw new ViewLocationExpanderViewsArgumentNullException(nameof(viewLocations));

            string value;
            context.Values.TryGetValue(DEVICE_KEY, out value);

            if (!string.IsNullOrEmpty(value))
            {
                IDevice device;
                try
                {
                    device = new Device(value);
                }
                catch (DeviceNotFoundException)
                {
                    return viewLocations;
                }

                return ExpandViewLocationsCore(viewLocations, device);
            }

            return viewLocations;
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, IDevice device)
        {
            foreach (var location in viewLocations)
            {
                if (_format == ResponsiveViewLocationFormat.Subfolder)
                    yield return location.Replace("{0}", device.Type.ToString() + "/{0}");
                else
                    yield return location.Replace("{0}", "{0}." + device.Type.ToString());

                yield return location;
            }
        }
    }
}
