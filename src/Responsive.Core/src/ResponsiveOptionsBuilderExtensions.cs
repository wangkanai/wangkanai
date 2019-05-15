using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;
using Wangkanai.Responsive;

namespace Microsoft.AspNetCore.Builder
{
    public static class ResponsiveOptionsBuilderExtensions
    {
        public static IResponsiveOptionsBuilder Default(
            this IResponsiveOptionsBuilder optionsBuilder,
            DeviceType expect,
            DeviceType actual)
        {
            throw new NotImplementedException();
        }
    }
}
