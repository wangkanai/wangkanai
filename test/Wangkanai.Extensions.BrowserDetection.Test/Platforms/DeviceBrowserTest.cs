using Microsoft.AspNetCore.Http;

namespace Wangkanai.Extensions.Browser.Platforms
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