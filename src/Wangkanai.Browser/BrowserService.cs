// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    /// <summary>
    /// Provides the APIs for query client access device.
    /// </summary>
    public class BrowserService : IBrowserService
    {
        public UserAgent UserAgent => _info.UserAgent;
        public Device Device => _info.Device;
        public string Platform => "concept";
        public string Engine => "concept";

        private readonly HttpContext _context;
        private readonly BrowserInfo _info;
        //private readonly DeviceInfo _info;
        //private readonly string _useragent;

        public BrowserService(IServiceProvider services)
        {
            if (services != null) _context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            if (_context == null) throw new ArgumentNullException(nameof(_context));

            var resolver = new BrowserResolver(_context);
            _info = resolver.BrowserInfo();

            //var agent = _context.Request.Headers["User-Agent"].FirstOrDefault();
            //if (agent != null)
            //    _useragent = agent.ToLowerInvariant();

            //_info = new DeviceResolver(_context.Request).DeviceInfo;
        }
    }
}
