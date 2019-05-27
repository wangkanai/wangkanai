using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;
using Wangkanai.Responsive;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponsiveOptionsBuilderExtensions
    {
        public static IResponsiveOptionsBuilder DefaultView(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType target,
            DeviceType prefer)
        {
            throw new NotImplementedException();
        }

        [Obsolete("This is an experimental API, its might change when finalize.")]
        public static IResponsiveOptionsBuilder DefaultTablet(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }
        [Obsolete("This is an experimental API, its might change when finalize.")]
        public static IResponsiveOptionsBuilder DefaultMobile(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }
        [Obsolete("This is an experimental API, its might change when finalize.")]
        public static IResponsiveOptionsBuilder DefaultDesktop(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }
    }
}
