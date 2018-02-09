// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;

namespace Wangkanai.Detection
{
    public class BrowserResolver : IBrowserResolver
    {
        public IBrowser Browser => _browser;
        public IUserAgent UserAgent => _service.UserAgent;

        private HttpContext _context => _service.Context;

        private readonly Browser _browser;
        private readonly IDetectionService _service;

        public BrowserResolver(IDetectionService service)
        {
            if (_service == null) throw new ArgumentNullException(nameof(service));

            _service = service;

            _browser = new Browser(GetBrowserType());
        }

        private BrowserType GetBrowserType()
        {
            if (UserAgent == null)
                return BrowserType.Generic;

            return BrowserType.Generic;
        }
    }
}