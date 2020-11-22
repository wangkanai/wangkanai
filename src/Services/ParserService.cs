using System;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class ParserService : IUserAgentService
    {
        public HttpContext Context   { get; }
        public UserAgent   UserAgent { get; }
        
        public ParserService(string useragent)
        {
            if (useragent is null)
                throw new ArgumentNullException(nameof(useragent));

            Context = new DefaultHttpContext();

            UserAgent = new UserAgent(useragent);
        }
    }
}