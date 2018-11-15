using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    public static class DeviceHttpRequestExtensions
    {
        public static IDevice Device(this HttpRequest request)
        {
            var service = new UserAgentService(request.HttpContext);
            var resolver = new DeviceResolver(service);
            return resolver.Device;
        }
    }
}
