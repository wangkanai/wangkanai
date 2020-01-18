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
        public bool IsCrawler { get; } = false;
        public Crawler Type { get; } = Crawler.Unknown;
        public Version Version { get; }

        private readonly UserAgent _useragent;
        private readonly DetectionOptions _options;

        public CrawlerService(IUserAgentService useragent, DetectionOptions options)
        {
            _useragent = useragent.UserAgent;
            _options = options;

            Type = CrawlerFromUserAgent(_useragent, _options?.Crawler.Others);
            IsCrawler = !IsUnknown(Type);
            Version = GetVersion(_useragent);
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

        private static Crawler CrawlerFromUserAgent(UserAgent useragent, List<string> others)
        {
            if (useragent.IsNullOrEmpty())
                return Crawler.Unknown;

            var agent = useragent.ToString().ToLower();
            foreach (var name in Crawlers)
                if (agent.Contains(name))
                    return TryParseCrawler(name);

            if (others != null)
                foreach (var name in others)
                    if (agent.Contains(name.ToLower()))
                        return Crawler.Others;

            if (useragent.Contains("bot"))
                return Crawler.Others;

            return Crawler.Unknown;
        }

        private static Crawler TryParseCrawler(string name)
            => (Crawler)Enum.Parse(typeof(Crawler), name, true);

        private static string FindBot(string agent)
            => agent.ToString().Split(' ')
                    .Where(x => CrawlerCount(x) > 0)
                    .FirstOrDefault();

        private static int CrawlerCount(string x)
            => Crawlers.Count(y => x.ToLower().Contains(y.ToLower()));

        private static string[] Crawlers
            => Enum.GetNames(typeof(Crawler)).Select(s => s.ToLower()).ToArray();

        private static bool IsUnknown(Crawler type)
            => type == Crawler.Unknown;
    }
}
