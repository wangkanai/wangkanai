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
        public CrawlerService(IUserAgentService useragent, DetectionOptions options)
        {
            var useragent1 = useragent.UserAgent;

            Type = CrawlerFromUserAgent(useragent1, options?.Crawler?.Others);
            IsCrawler = !IsUnknown(Type);
            Version = GetVersion(useragent1);
        }

        private static string[] Crawlers
            => Enum.GetNames(typeof(Crawler)).Select(s => s.ToLower()).ToArray();

        public bool IsCrawler { get; }
        public Crawler Type { get; }
        public Version Version { get; }

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

        private static Crawler CrawlerFromUserAgent(UserAgent agent, List<string> others)
        {
            if (agent.IsNullOrEmpty())
                return Crawler.Unknown;

            foreach (var name in Crawlers)
                if (agent.Contains(name))
                    return TryParseCrawler(name);

            if (others != null)
                foreach (var name in others)
                    if (agent.Contains(name))
                        return Crawler.Others;

            if (agent.Contains("bot"))
                return Crawler.Others;

            return Crawler.Unknown;
        }

        private static Crawler TryParseCrawler(string name)
        {
            return (Crawler) Enum.Parse(typeof(Crawler), name, true);
        }

        private static string FindBot(string agent)
        {
            return agent.Split(' ')
                .FirstOrDefault(x => CrawlerCount(x) > 0);
        }

        private static int CrawlerCount(string x)
        {
            return Crawlers.Count(y => x.ToLower().Contains(y.ToLower()));
        }

        private static bool IsUnknown(Crawler type)
        {
            return type == Crawler.Unknown;
        }
    }
}
