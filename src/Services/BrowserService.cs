// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using OperatingSystem = Wangkanai.Detection.Models.OperatingSystem;

namespace Wangkanai.Detection.Services
{
    public class BrowserService : IBrowserService
    {
        public Browser Type    { get; }
        public Version Version { get; }

        public BrowserService(IUserAgentService userAgentService, IPlatformService platformService, IEngineService engineService)
        {
            var agent  = userAgentService.UserAgent;
            var os     = platformService.OperatingSystem;
            var engine = engineService.Type;
            Type    = GetBrowser(agent, os, engine);
            Version = GetVersion(agent.ToLower(), Type.ToString());
        }

        private static Browser GetBrowser(UserAgent agent, OperatingSystem os, Engine engine)
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

        private static Version GetVersion(string agent, string browser)
        {
            if (agent.IsNullOrEmpty())
                return new Version();

            if (agent.Contains("rv:11.0") || agent.Contains("ie 11.0"))
                return new Version(11, 0);
            if (agent.Contains("msie 10"))
                return new Version(10, 0);
            if (agent.Contains("msie 9"))
                return new Version(9, 0);

            var    first = agent.IndexOf(browser.ToLower(), StringComparison.Ordinal);
            string cut;
            try
            {
                cut = agent.Substring(first + browser.Length + 1);
            }
            catch
            {
                cut = agent.Substring(first + browser.Length);
            }

            var version = cut.Contains(" ") ? cut.Substring(0, cut.IndexOf(' ')) : cut;
            return version.ToVersion();
        }

        private static bool IsEdge(UserAgent agent)
            => agent.Contains(Browser.Edge)
               || (agent.Contains("Win64") && agent.Contains("Edg"));

        private static bool IsInternetExplorer(UserAgent agent, OperatingSystem os, Engine engine)
            => engine == Engine.Trident
               || agent.Contains("MSIE")
               && !agent.Contains(Browser.Opera);
    }
}