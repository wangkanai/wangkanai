// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Collections;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public class BrowserResolver : IBrowserResolver
    {
        private readonly IUserAgentService _service;

        public BrowserResolver(IUserAgentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            Browser = GetBrowser();
        }

        public UserAgent UserAgent => _service.UserAgent;
        public IBrowserFactory Browser { get; }

        private BrowserFactory GetBrowser()
        {
            var agent = UserAgent.ToString();

            var ie = new InternetExplorer(agent);
            if (ie.Type == Models.Browser.InternetExplorer)
                return ie;
            var firefox = new Firefox(agent);
            if (firefox.Type == Models.Browser.Firefox)
                return firefox;
            var edge = new Edge(agent);
            if (edge.Type == Models.Browser.Edge)
                return edge;
            var opera = new Opera(agent);
            if (opera.Type == Models.Browser.Opera)
                return opera;
            var chrome = new Chrome(agent);
            if (chrome.Type == Models.Browser.Chrome)
                return chrome;
            var safari = new Safari(agent);
            if (safari.Type == Models.Browser.Safari)
                return safari;

            return new BrowserFactory();
        }
    }
}
