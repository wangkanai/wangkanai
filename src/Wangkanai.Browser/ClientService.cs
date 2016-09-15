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
        public Device Device => _info.Device;
        public Browser Browser => _info.Browser; //concept for implementation
        public Engine Engine => _info.Engine; //concept for implementation
        public Platform Platform => _info.Platform;  //concept for implementation

        private readonly ClientInfo _info;
        private readonly HttpContext _context;

        public ClientService(IServiceProvider services, IDeviceResolver deviceResolver)
        {
            if (services != null) _context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            if (_context == null) throw new ArgumentNullException(nameof(_context));
            if (deviceResolver == null) throw new ArgumentNullException(nameof(deviceResolver));

            var useragent = new UserAgent(_context.Request.Headers["User-Agent"].FirstOrDefault());
            var device = deviceResolver.Device;
            var browser = new Browser();   // waiting for implementation
            var engine = new Engine();     // waiting for implementation
            var platform = new Platform(); // waiting for implementation            
            _info = new ClientInfo(useragent, browser, device, engine, platform);
        }
    }
}
