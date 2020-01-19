// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Collections;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public class BrowserResolver : BaseResolver, IBrowserResolver
    {
        public IBrowserFactory Browser { get; }

        public BrowserResolver(IUserAgentService service) : base(service)
        {
            this.Browser = GetBrowser();
        }

        private BrowserFactory GetBrowser()
        {
            var agent = UserAgent.ToString();

            var ie = new InternetExplorer(agent);
            if (ie.Type == Models.Browser.IE)
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
