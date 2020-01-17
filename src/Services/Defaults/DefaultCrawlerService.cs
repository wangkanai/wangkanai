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
    public class DefaultCrawlerService : ICrawlerService
    {
        public bool IsCrawler { get; } = false;
        public Crawler Type { get; } = Crawler.Unknown;
        public Version Version { get; }

        private readonly UserAgent _useragent;
        private readonly DetectionOptions _options;

        public DefaultCrawlerService(IUserAgentService useragent, DetectionOptions options)
        {
            _useragent = useragent.UserAgent;
            _options = options;

            Type = FindCrawlerInUserAgent(_useragent, _options?.Crawler.Others);
            IsCrawler = IsUnknown(Type);
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

        private static Crawler FindCrawlerInUserAgent(UserAgent useragent, List<string> others)
        {
            if (useragent.IsNullOrEmpty())
                return Crawler.Unknown;

            var agent = useragent.ToString().ToLower();
            foreach (var name in GetCrawlerNames())
                if (agent.Contains(name.ToLower()))
                    return TryParseCrawler(name);
            if (others != null)
                foreach (var name in others)
                    if (agent.Contains(name.ToLower()))
                        return Crawler.Others;

            if (useragent.ToString().ToLower().Contains("bot"))
                return Crawler.Others;

            return Crawler.Unknown;
        }

        private static string[] GetCrawlerNames()
            => Enum.GetNames(typeof(Crawler));

        private static Crawler TryParseCrawler(string name)
            => (Crawler)Enum.Parse(typeof(Crawler), name);

        private static string FindBot(string agent)
        {
            var split = agent.ToString().Split(' ');
            return split.Where(x => CrawlerLookup(x))
                        .FirstOrDefault();
        }

        private static bool CrawlerLookup(string x)
        {
            var count = Enum.GetNames(typeof(Crawler))
                       .Count(y => x.ToLower().Contains(y.ToLower()));
            return count > 0;
        }

        private static bool IsUnknown(Crawler type)
            => type != Crawler.Unknown;
    }
}
