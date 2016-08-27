// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    public abstract class DeviceBrowserTest
    {
        protected HttpRequest CreateRequest(string value)
        {
            var request = new DefaultHttpContext().Request;
            var header = "User-Agent";
            var headerValue = value;
            request.Headers.Add(header, new[] { headerValue });

            return request;
        }
    }
}