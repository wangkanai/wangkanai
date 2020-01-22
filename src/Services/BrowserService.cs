// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class BrowserService : IBrowserService
    {
        public Browser Type { get; }

        public BrowserService(IUserAgentService userAgentService, IPlatformService platformService, IEngineService engineService)
        {
            var agent  = userAgentService.UserAgent;
            var os     = platformService.OperatingSystem;
            var cpu    = platformService.Processor;
            var engine = engineService.Type;
            Type = ParseBrowser(agent, os, engine);
        }

        private static Browser ParseBrowser(UserAgent agent, OperatingSystem os, Engine engine)
        {
            // fail and return fast
            if (agent.IsNullOrEmpty())
                return Browser.Unknown;
            // Microsoft Edge
            if (IsEdge(agent))
                return Browser.Edge;
            // Google chrome
            if (agent.Contains(Browser.Chrome))
                return Browser.Chrome;
            // Microsoft Internet Explorer
            if (IsInternetExplorer(agent, os, engine))
                return Browser.InternetExplorer;
            // Apple Safari
            if (agent.Contains(Browser.Safari))
                return Browser.Safari;
            // Firefox
            if (agent.Contains(Browser.Firefox))
                return Browser.Firefox;

            // Opera
            if (agent.Contains(Browser.Opera))
                return Browser.Opera;

            return Browser.Others;
        }

        private static bool IsEdge(UserAgent agent)
            => agent.Contains(Browser.Edge) || (agent.Contains("Win64") && agent.Contains("Edg"));

        private static bool IsInternetExplorer(UserAgent agent, OperatingSystem os, Engine engine)
            => engine == Engine.Trident || agent.Contains("MSIE") && !agent.Contains(Browser.Opera);
    }
}
