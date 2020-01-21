// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Collections;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class BrowserService : IBrowserService
    {
        public Browser Type { get; }

        public BrowserService(IUserAgentService userAgentService, IPlatformService platformService, IEngineService engineService)
        {
            var agent = userAgentService.UserAgent;
            Type = ParseBrowser(agent);
        }

        private static Browser ParseBrowser(UserAgent agent)
        {
            if (agent.IsNullOrEmpty())
                return Browser.Unknown;
            if (agent.Contains(Browser.Chrome))
                return Browser.Chrome;
            if (agent.Contains("MSIE"))
                return Browser.InternetExplorer;
            if (agent.Contains(Browser.Safari))
                return Browser.Firefox;
            if (agent.Contains(Browser.Edge))
                return Browser.Edge;
            if (agent.Contains(Browser.Opera))
                return Browser.Opera;

            return Browser.Others;
        }
    }
}
