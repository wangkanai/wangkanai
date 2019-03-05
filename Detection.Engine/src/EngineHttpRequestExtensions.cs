// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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
