using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Browser.Abstractions;

namespace Wangkanai.Browser
{
    public class BrowserDetector : IBrowserDetector
    {        
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DeviceInfo _info;        

        public BrowserDetector(IHttpContextAccessor contextAccessor)
        {
            if(contextAccessor == null) throw new ArgumentNullException(nameof(contextAccessor));

            _contextAccessor = contextAccessor;
            _info = Resolve(contextAccessor.HttpContext);
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
