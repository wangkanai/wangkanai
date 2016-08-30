using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Browser.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    public class BrowserService : IBrowserService
    {        
        private readonly HttpContext _context;
        private readonly DeviceInfo _info;
        private readonly string _useragent;

        public BrowserService(IServiceProvider services)
        {
            if (services != null)
                _context = services.GetService<IHttpContextAccessor>()?.HttpContext;

            if (_context == null) throw new ArgumentNullException(nameof(_context));

            var agent = _context.Request.Headers["User-Agent"].FirstOrDefault();
            if (agent != null)
                _useragent = agent.ToLowerInvariant();

            _info = new DeviceResolver(_context.Request).DeviceInfo;
        }

        public string UserAgent() => _useragent;
        public string Device() => _info.Device.ToString();
        public string Platform() => "test";
        public string Engine() => "test";
    }
}
