// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using Wangkanai.Detection.Collections;

namespace Wangkanai.Detection
{
    public class BrowserResolver : BaseResolver, IBrowserResolver
    {
        public IBrowser Browser => _browser;

        private readonly Browser _browser;

        public BrowserResolver(IUserAgentService service) : base(service)
        {
            _browser = GetBrowser();
        }

        private Browser GetBrowser()
        {
            var agent = UserAgent.ToString();

            var ie = new InternetExplorer(agent);
            if (ie.Type == BrowserType.IE)
                return ie;
            var firefox = new Firefox(agent);
            if (firefox.Type == BrowserType.Firefox)
                return firefox;
            var edge = new Edge(agent);
            if (edge.Type == BrowserType.Edge)
                return edge;
            var opera = new Opera(agent);
            if (opera.Type == BrowserType.Opera)
                return opera;
            var safari = new Safari(agent);
            if (safari.Type == BrowserType.Safari)
                return safari;
            var chrome = new Chrome(agent);
            if (chrome.Type == BrowserType.Chrome)
                return chrome;

            return new Browser();
        }
    }
}