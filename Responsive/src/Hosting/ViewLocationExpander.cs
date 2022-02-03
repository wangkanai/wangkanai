// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.ComponentModel;

using Microsoft.AspNetCore.Mvc.Razor;

using Wangkanai.Detection.Extensions;
using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Hosting;

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

        context.Values.TryGetValue(ValueKey, out var device);

        if (string.IsNullOrEmpty(device))
            return viewLocations;

        return ExpandViewLocationsCore(viewLocations, device); //.Concat(viewLocations);
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
}