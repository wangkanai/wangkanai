// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class CrawlerService : ICrawlerService
    {
        public bool    IsCrawler { get; }
        public Crawler Name      { get; }
        public Version Version   { get; }

        public CrawlerService(IUserAgentService useragent, DetectionOptions options)
        {
            var agent = useragent.UserAgent;

            Name      = GetCrawler(agent, options.Crawler.Others);
            IsCrawler = Name != Crawler.Unknown;
            Version   = GetVersion(agent);
        }

        private static Crawler GetCrawler(UserAgent agent, IEnumerable<string> others)
        {
            if (agent.IsNullOrEmpty())
                return Crawler.Unknown;

            foreach (var crawler in Crawlers)
                if (agent.Contains(crawler))
                    return crawler;

            return HasOthers(agent, others)
                       ? Crawler.Others
                       : Crawler.Unknown;
        }

        private static Version GetVersion(UserAgent useragent)
        {
            var agent = useragent.ToString();

            var bot = FindBot(agent);
            if (bot.IsNullOrEmpty())
                return new Version();

            var index = bot.IndexOf('/');
            if (index < 0)
                index = bot.IndexOf(';');

            var version = string.Empty;
            if (index > 0)
                version = bot.Substring(index + 1).TrimEnd(';');

            return version.ToVersion();
        }

        private static bool HasOthers(UserAgent agent, IEnumerable<string> others)
            => agent.Contains(others) || agent.Contains("bot");

        private static string FindBot(string agent)
            => agent.Split(' ').FirstOrDefault(x => CrawlerCount(x) > 0);

        private static int CrawlerCount(string x)
            => Crawlers.Count(y => x.ToLower().Contains(y.ToString().ToLower()));

        private static IEnumerable<Crawler> Crawlers
            => Enum.GetValues(typeof(Crawler)).Cast<Crawler>();
    }
}