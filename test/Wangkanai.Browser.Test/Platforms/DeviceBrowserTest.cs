// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser.Test.Platforms
{
    public abstract class DeviceBrowserTest
    {
        private HttpRequest CreateRequest() => new DefaultHttpContext().Request;
        protected HttpRequest CreateRequest(string value)
        {
            var request = CreateRequest();
            var header = "User-Agent";            
            request.Headers.Add(header, new[] { value });

            return request;
        }

        protected HttpRequest CreateRequest(string header, string value)
        {
            var request = CreateRequest();
            request.Headers.Add(header, new[] {value});

            return request;
        }

    }
}