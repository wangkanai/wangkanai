using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Wangkanai.Detection.UI
{
    internal class ResponsiveDefaultUIConfigurationOptions : IPostConfigureOptions<RazorPagesOptions>
    {
        private const string              ResponsiveUIDefaultAreaName = "Responsive";
        public        IWebHostEnvironment Environment { get; }

        public ResponsiveDefaultUIConfigurationOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void PostConfigure(string name, RazorPagesOptions options)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var convention = new ResponsivePageModelConvention();
            options.Conventions.AddAreaFolderApplicationModelConvention(
                ResponsiveUIDefaultAreaName,
                "/",
                pam => convention.Apply(pam));
        }
    }
}