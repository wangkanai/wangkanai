// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class BrowserService : IBrowserService
    {
        public Browser Name    { get; }
        public Version Version { get; }

        public BrowserService(IUserAgentService userAgentService, IPlatformService platformService, IEngineService engineService)
        {
            var agent  = userAgentService.UserAgent;
            var os     = platformService.Name;
            var engine = engineService.Name;
            Name    = GetBrowser(agent, os, engine);
            Version = GetVersion(agent, Name);
        }

        private static Browser GetBrowser(UserAgent agent, Platform os, Engine engine)
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

        private static Version GetVersion(UserAgent agent, Browser browser)
        {
            if (agent.IsNullOrEmpty())
                return new Version();

            if (agent.Contains("rv:11.0") || agent.Contains("ie 11.0"))
                return new Version(11, 0);
            if (agent.Contains("msie 10"))
                return new Version(10, 0);
            if (agent.Contains("msie 9"))
                return new Version(9, 0);

            if (browser == Browser.Edge && !agent.Contains("edge"))
                agent = agent.Replace("edg", "edge");

            var    name  = browser.ToString();
            var    first = agent.IndexOf(browser);
            string cut;
            try
            {
                cut = agent.Substring(first + name.Length + 1);
            }
            catch
            {
                cut = agent.Substring(first + name.Length);
            }

            var version = cut.Contains(" ") ? cut.Substring(0, cut.IndexOf(' ')) : cut;
            return version.ToVersion();
        }

        private static bool IsEdge(UserAgent agent)
            => agent.Contains(Browser.Edge)
               || (agent.Contains("Win64") && agent.Contains("Edg"));

        private static bool IsInternetExplorer(UserAgent agent, Platform os, Engine engine)
            => engine == Engine.Trident
               || agent.Contains("MSIE")
               && !agent.Contains(Browser.Opera);
    }
}