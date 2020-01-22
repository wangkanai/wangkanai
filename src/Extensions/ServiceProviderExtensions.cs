using System;

namespace Wangkanai.Detection.Extensions
{
    internal static class ServiceProviderExtensions
    {
        public static bool IsNull(this IServiceProvider services)
            => services == null;
    }
}
