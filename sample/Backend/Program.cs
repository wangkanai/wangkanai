using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

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

            var accessor  = new HttpContextAccessor {HttpContext = context};
            var useragent = new UserAgentService(accessor);
            Console.WriteLine(useragent.UserAgent);
        }
    }
}