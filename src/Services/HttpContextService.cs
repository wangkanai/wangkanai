using System;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services
{
    public class HttpContextService : IHttpContextService
    {
        public HttpContext Context { get; }
        public HttpRequest Request => Context.Request;

        public HttpContextService(IHttpContextAccessor accessor)
        {
            if (accessor is null)
                throw new ArgumentNullException(nameof(accessor));
            
            Context = accessor.HttpContext ?? new DefaultHttpContext();
        }
    }
}