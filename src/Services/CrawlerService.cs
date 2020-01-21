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
        public bool IsCrawler { get; }
        public Crawler Type { get; }
        public Version Version { get; }

        public CrawlerService(IUserAgentService useragent, DetectionOptions options)
        {
            var agent = useragent.UserAgent;

            Type = CrawlerFromUserAgent(agent, options?.Crawler?.Others);
            IsCrawler = !IsUnknown(Type);
            Version = GetVersion(agent);
        }

        private static Version GetVersion(UserAgent useragent)
        {
            var agent = useragent.ToString();

            var bot = FindBot(agent);
            if (bot.IsNullOrEmpty()) return new Version();

            var index = bot.IndexOf('/');
            if (index < 0)
                index = bot.IndexOf(';');

            var version = string.Empty;
            if (index > 0)
                version = bot.Substring(index + 1).TrimEnd(';');

            return version.ToVersion();
        }

        private static Crawler CrawlerFromUserAgent(UserAgent agent, IEnumerable<string> others)
        {
            if (agent.IsNullOrEmpty())
                return Crawler.Unknown;

            foreach (var name in Crawlers)
                if (agent.Contains(name))
                    return ParseCrawler(name);

            if (agent.Contains(others))
                return Crawler.Others;

            if (agent.Contains("bot"))
                return Crawler.Others;

            return Crawler.Unknown;
        }

        private static Crawler ParseCrawler(string name)
            => (Crawler) Enum.Parse(typeof(Crawler), name, true);

        private static string FindBot(string agent)
            => agent.Split(' ')
                .FirstOrDefault(x => CrawlerCount(x) > 0);

        private static int CrawlerCount(string x)
            => Crawlers.Count(y => x.ToLower().Contains(y.ToLower()));

        private static string[] Crawlers
            => Enum.GetNames(typeof(Crawler))
                .Select(s => s.ToLower())
                .ToArray();

        private static bool IsUnknown(Crawler type)
            => type == Crawler.Unknown;
    }
}
