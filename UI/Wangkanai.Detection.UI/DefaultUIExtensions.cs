using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Wangkanai.Detection.UI;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveDefaultUIExtensions
    {
        public static IDetectionBuilder AddDefaultUI(this IDetectionBuilder builder)
        {
            AddRelatedParts(builder);
            builder.Services.ConfigureOptions(typeof(ResponsiveDefaultUIConfigurationOptions));
            
            return builder;
        }
        
        private static void AddRelatedParts(this IDetectionBuilder builder)
        {
            var mvcBuilder = builder.Services
                .AddMvc()
                .ConfigureApplicationPartManager(partManager =>
                {
                    var thisAssembly      = typeof(ResponsiveBuilderExtensions).Assembly;
                    var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(thisAssembly, throwOnError: true);
                    var relatedParts = relatedAssemblies.ToDictionary(
                        ra => ra,
                        CompiledRazorAssemblyApplicationPartFactory.GetDefaultApplicationParts);

                    foreach (var kvp in relatedParts)
                    {
                        foreach (var part in kvp.Value)
                            if (partManager.ApplicationParts.Any(p => IsSameApplicationPartType(p, part) && IsSameApplicationPart(p, part)))
                                partManager.ApplicationParts.Add(part);
                    }

                    bool IsSameApplicationPartType(ApplicationPart p, ApplicationPart part)
                        => p.GetType() == part.GetType();

                    bool IsSameApplicationPart(ApplicationPart p, ApplicationPart part)
                        => string.Equals(p.Name, part.Name, StringComparison.OrdinalIgnoreCase);
                });
        }
    }
}