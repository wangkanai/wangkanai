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
    public class ClientService : IClientService
    {
        public UserAgent UserAgent => _info.Useragent;
        public string Browser => "concept";
        public Device Device => _info.Device;
        public string Engine => "concept";
        public string Platform => "concept";        

        private readonly HttpContext _context;
        private readonly ClientInfo _info;
        //private readonly DeviceInfoDepreciated _info;
        //private readonly string _useragent;

        public ClientService(IServiceProvider services)
        {
            if (services != null) _context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            if (_context == null) throw new ArgumentNullException(nameof(_context));

            //var resolver = new ClientResolver(_context);
            var useragent = new UserAgent(_context.Request.Headers["User-Agent"].FirstOrDefault());
            var browser = new Browser();   // waiting for implementation
            var device = new Device();     // waiting for implementation
            var engine = new Engine();     // waiting for implementation
            var platform = new Platform(); // waiting for implementation            
            _info = new ClientInfo(useragent, browser, device, engine, platform);
            
            //if (useragent != null)
            //    _useragent = useragent.ToLowerInvariant();

            //_info = new DeviceResolverDepreciated(_context.Request).DeviceInfoDepreciated;
        }
    }
}
