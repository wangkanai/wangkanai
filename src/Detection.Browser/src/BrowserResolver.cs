// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using Wangkanai.Detection.Collections;

namespace Wangkanai.Detection
{
    public class BrowserResolver : BaseResolver, IBrowserResolver
    {
        public IBrowser Browser { get; }

        public BrowserResolver(IUserAgentService service) : base(service)
        {
            this.Browser = GetBrowser();
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
            var chrome = new Chrome(agent);
            if (chrome.Type == BrowserType.Chrome)
                return chrome;
            var safari = new Safari(agent);
            if (safari.Type == BrowserType.Safari)
                return safari;

            return new Browser();
        }
    }
}
