// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    public static class BrowserHttpRequestExtensions
    {
        public static IBrowser Browser(this HttpRequest request)
        {
            var service = new UserAgentService(request.HttpContext);
            var resolver = new BrowserResolver(service);
            return resolver.Browser;
        }
    }
}
