using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Hosting
{
    internal class ResponsivePageRouteModelConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            // Very interesting use of C# 8.0 array index from end operator
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
            var fileNameWithoutExtension = model.ViewEnginePath.Split('/')[^1];
            // Normal page, nothing to do!
            if (!fileNameWithoutExtension.Contains('.'))
                return;

            // This just implements the 'suffix' strategy
            var split = fileNameWithoutExtension.Split('.');
            if (split.Length != 2)
                throw new InvalidOperationException($"Page '{model.RelativePath}' does not follow the required format.");

            var areaName   = model.AreaName;
            var pageName   = split[0];
            var deviceName = split[1];

            if (!Enum.TryParse<Device>(deviceName, ignoreCase: true, out var device))
                throw new InvalidOperationException($"Device name could not be parsed for page '{model.RelativePath}'.");

            // Since the page name has something like `.mobile` in it, the special cased rules for Index.cshtml
            // don't apply. We know there's going to be one selector.
            var selector = model.Selectors.Single();

            // If the pages in an area, then add it to the route template too.
            var area = areaName.IsNullOrEmpty() ? "" : areaName + "/";

            // Remove the device name from the route template. This is complicated because the route template
            // can additional parameters defined in the page itself.
            //
            // Ex: we need to turn `About/Help.mobile/{id?}` into `About/Help/{id?}`

            // prefix = `About/`
            var viewEnginePath = model.ViewEnginePath;
            var relativePath   = model.RelativePath;

            var prefix = model.ViewEnginePath.Substring(1, model.ViewEnginePath.LastIndexOf('/'));

            // We can get the route parameter that the user put in after `@page` by substringing.
            //
            // suffix = '/{id?}'
            var templateOld = selector.AttributeRouteModel.Template;

            var suffixStartIndex = area.Length + prefix.Length + fileNameWithoutExtension.Length;
            var suffix = templateOld.Substring(suffixStartIndex);

            if (pageName.Equals("Index", StringComparison.OrdinalIgnoreCase))
            {
                // Pages like About/Index.cshtml have special behavior. When the page is called Index, it will
                // have two selectors, one with `Index` as part of the template and one without.

                // Add another selector with matches the URL without `Index`
                var another = new SelectorModel(selector);
                model.Selectors.Add(another);
                var templateIndex = area + prefix.TrimEnd('/') + suffix;
                another.AttributeRouteModel.Template = templateIndex;

                // Disable link generation for the original selector, to prefer the shorter URL.
                selector.AttributeRouteModel.SuppressLinkGeneration = true;

                // Allow routing to filter by device type.
                another.EndpointMetadata.Add(new ResponsiveAttribute(device));
            }

            // Now rewrite the original selector
            var templateNew = area + prefix + pageName + suffix;

            if (!templateNew.Equals(templateOld))
                Console.WriteLine($"{templateNew} != {templateOld}");

            selector.AttributeRouteModel.Template = templateNew;

            // Allow routing to filter by device type.
            selector.EndpointMetadata.Add(new ResponsiveAttribute(device));
        }
    }
}