using System;

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Services;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var agent   = "useragent";
            var context = new DefaultHttpContext();
            context.Request.Headers["User-Agent"] = agent;

            var accessor       = new HttpContextAccessor {HttpContext = context};
            var contextService = new HttpContextService(accessor);
            var useragent      = new UserAgentService(contextService);
            Console.WriteLine(useragent.UserAgent);
        }
    }
}