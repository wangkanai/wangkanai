// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Hosting;

public class ResponsivePageLocationExpander : IViewLocationExpander
{
    private const string ValueKey = "device";

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        context.Values.TryGetValue(ValueKey, out var device);

        if (context.ActionContext.ActionDescriptor is not PageActionDescriptor)
            return viewLocations;

        if (string.IsNullOrEmpty(context.PageName) || string.IsNullOrEmpty(device))
            return viewLocations;

        var expand = ExpandPageHierarchy();

        return expand;

        IEnumerable<string> ExpandPageHierarchy()
        {
            foreach (var location in viewLocations)
            {
                // If the location doesn't have the 'page' replacement token just return it as-is.
                if (!location.Contains("/Pages/", StringComparison.OrdinalIgnoreCase))
                {
                    yield return location;
                    continue;
                }

                // Device ResponsiveWeb if exist on disk
                // yield return location.Replace("{0}", "{0}." + device);
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