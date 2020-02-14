using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Wangkanai.Detection.Mocks
{
    internal class MockPageRouteModel
    {
        private readonly        RazorPagesOptions _options;
        private static readonly string            IndexFileName = "Index" + RazorViewEngine.ViewExtension;
        private readonly        string            _normalizedRootDirectory;
        private readonly        string            _normalizedAreaRootDirectory;

        public MockPageRouteModel(RazorPagesOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _normalizedRootDirectory     = NormalizeDirectory(options.RootDirectory);
            _normalizedAreaRootDirectory = "/Areas/";
        }

        public PageRouteModel CreateRouteModel(string relativePath, string routeTemplate)
        {
            var viewEnginePath = GetViewEnginePath(_normalizedRootDirectory, relativePath);
            var model          = new PageRouteModel(relativePath, viewEnginePath);

            PopulateRouteModel(model, viewEnginePath, routeTemplate);
            
            return model;
        }

        public PageRouteModel CreateAreaRouteModel(string relativePath, string routeTemplate)
        {
            if (!TryParseAreaPath(relativePath, out var areaResult))
                return null;
            
            var model = new PageRouteModel(relativePath, areaResult.viewEnginePath, areaResult.areaName);
            var prefix = CreateAreaRoute(areaResult.areaName, areaResult.viewEnginePath);
            PopulateRouteModel(model, prefix, routeTemplate);
            model.RouteValues["area"] = areaResult.areaName;
            
            return model;
        }




        private static void PopulateRouteModel(PageRouteModel model, string pageRoute, string routeTemplate)
        {
            throw new NotImplementedException();
        }
        
        private bool TryParseAreaPath(string relativePath, out (string areaName, string viewEnginePath) result)
        {
            throw new NotImplementedException();
        }

        private string GetViewEnginePath(string rootDirectory, string path)
        {
            var start = rootDirectory.Length - 1;
            var end   = path.Length - RazorViewEngine.ViewExtension.Length;

            return path.Substring(start, end - start);
        }
        
        private static string CreateAreaRoute(string areaName, string viewEnginePath)
        {
            throw new NotImplementedException();
        }

        private static string NormalizeDirectory(string directory)
            => directory.Length > 1
               && !directory.EndsWith("/", StringComparison.Ordinal)
                   ? directory + "/"
                   : directory;
    }
}