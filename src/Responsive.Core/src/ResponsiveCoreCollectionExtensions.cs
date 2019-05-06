using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Responsive;
using Wangkanai.Responsive.Builders;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ResponsiveCoreCollectionExtensions
    {
        public static IResponsiveCoreBuilder AddResponsiveCore(this IServiceCollection services)
        {
            if (services == null)
                throw new AddResponsiveArgumentNullException(nameof(services));

            services.AddDetectionCore().AddDevice();

            return new ResponsiveCoreBuilder(services);
        }
    }
}
