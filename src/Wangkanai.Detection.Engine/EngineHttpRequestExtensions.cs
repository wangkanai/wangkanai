using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    public static class EngineHttpRequestExtensions
    {
        public static IEngine Engine(this HttpRequest request)
        {
            var service = new UserAgentService(request.HttpContext);
            var resolver = new EngineResolver(service);
            return resolver.Engine;
        }
    }
}
