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
        public HttpContext Context { get; }
        public UserAgent UserAgent => _info.Useragent;
        public string Browser => "concept";
        public Device Device => _info.Device;
        public string Engine => "concept";
        public string Platform => "concept";        
        
        private readonly ClientInfo _info;

        public ClientService(IServiceProvider services)
        {
            if (services != null) Context = services.GetService<IHttpContextAccessor>()?.HttpContext;
            if (Context == null) throw new ArgumentNullException(nameof(Context));

            //var resolver = new ClientResolver(_context);
            var useragent = new UserAgent(Context.Request.Headers["User-Agent"].FirstOrDefault());
            var browser = new Browser();   // waiting for implementation
            var device = new Device();     // waiting for implementation
            var engine = new Engine();     // waiting for implementation
            var platform = new Platform(); // waiting for implementation            
            _info = new ClientInfo(useragent, browser, device, engine, platform);                        
        }
    }
}
