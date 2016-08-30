using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Browser.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    public class BrowserDetector : IBrowserDetector
    {        
        private readonly HttpContext _context;
        private readonly DeviceInfo _info;        

        public BrowserDetector(IServiceProvider services)
        {
            if (services != null)
                _context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            _info = Resolve(_context);
        }

        private DeviceInfo Resolve(HttpContext context)
        {
            var request = context.Request;
            var resolver = new DeviceResolver(request);
            return resolver.DeviceInfo;
        }

        public string Device() => _info.Device.ToString();
        public string Platform() => "test";
        public string Engine() => "test";
    }
}
