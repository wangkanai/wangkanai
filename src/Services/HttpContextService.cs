using System;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services
{
    public class HttpContextService : IHttpContextService
    {
        public HttpContext Context { get; }
        public HttpRequest Request => Context.Request;

        public HttpContextService(IHttpContextAccessor accessor) 
            => Context = accessor?.HttpContext 
                         ?? new DefaultHttpContext();

        public static HttpContextService CreateService()
            => new(null!);
    }
}