// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;

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

        public DefaultCrawlerService(IUserAgentService useragent)
        {
            _useragent = useragent.UserAgent;
            Type = FindCrawlerInUserAgent(_useragent);
            IsCrawler = isCrawler(Type);
            Version = GetVersion(_useragent);
        }
        private static Crawler FindCrawlerInUserAgent(UserAgent agent)
        {
            if (agent.IsNullOrEmpty())
                return Crawler.Unknown;

            var crawler = SearchCrawler(agent);

            if (IsBot(agent, Crawler.Google))
                return Crawler.Google;
            if (IsBot(agent, Crawler.Facebook))
                return Crawler.Facebook;
            if (IsBot(agent, Crawler.Bing))
                return Crawler.Bing;
            if (IsBot(agent, Crawler.Twitter))
                return Crawler.Twitter;
            if (IsBot(agent, Crawler.Yahoo))
                return Crawler.Yahoo;
            if (IsBot(agent, Crawler.Baidu))
                return Crawler.Baidu;
            if (IsBot(agent, Crawler.WhatsApp))
                return Crawler.Baidu;
            if (IsBot(agent, Crawler.LinkedIn))
                return Crawler.LinkedIn;
            if (IsBot(agent, Crawler.Skype))
                return Crawler.Skype;
            if (agent.ToString().ToLower().Contains("bot"))
                return Crawler.Others;

            return Crawler.Unknown;
        }

        private static Crawler SearchCrawler(UserAgent useragent)
        {
            var agent = useragent.ToString().ToLower();
            foreach (var name in Enum.GetNames(typeof(Crawler)))
            {
                if (agent.Contains(name.ToLower()))
                    return Enum.Parse(typeof(Crawler), name);
            }

            return Crawler.Others;
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

        private static string FindBot(string agent)
        {
            var split = agent.ToString().Split(' ');
            return split.Where(x => Enum.GetNames(typeof(Crawler))
                                        .Count(y => x.ToLower().Contains(y.ToLower())) == 1)
                        .FirstOrDefault();
        }

        private static bool isCrawler(Crawler type)
            => type != Crawler.Unknown;

        private static bool IsBot(UserAgent agent, Crawler expected)
        {
            return agent.ToString()
                .ToLower()
                .Contains(expected.ToString().ToLower());
        }
    }
}
