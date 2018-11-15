using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    public static class PlatformHttpRequestExtensions
    {
        public static IPlatform Platform(this HttpRequest request)
        {
            var service = new UserAgentService(request.HttpContext);
            var resolver = new PlatformResolver(service);
            return resolver.Platform;
        }
    }
}
