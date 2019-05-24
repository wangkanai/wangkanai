using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;
using Wangkanai.Responsive;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponsiveOptionsBuilderExtensions
    {
        public static IResponsiveOptionsBuilder MapView(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType target,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public static IResponsiveOptionsBuilder DefaultTablet(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }
        [Obsolete]
        public static IResponsiveOptionsBuilder DefaultMobile(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }
        [Obsolete]
        public static IResponsiveOptionsBuilder DefaultDesktop(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType preferred)
        {
            throw new NotImplementedException();
        }
    }
}
