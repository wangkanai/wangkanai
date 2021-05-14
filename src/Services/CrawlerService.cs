using System;
using System.Collections.Generic;
using System.Linq;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Runtime.Extensions;

namespace Wangkanai.Detection.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly IUserAgentService _useragent;
        private readonly DetectionOptions  _options;

        public CrawlerService(IUserAgentService useragent, DetectionOptions options)
        {
            _useragent    = useragent;
            _options = options;
        }

        public  bool     IsCrawler => Name != Crawler.Unknown;
        private Crawler? _name;
        private Version? _version;
        public  Crawler  Name    => _name ??= GetCrawler();
        public  Version  Version => _version ??= GetVersion();

        private Crawler GetCrawler()
        {
            var agent = _useragent.UserAgent.ToLower();

            if (string.IsNullOrEmpty(agent))
                return Crawler.Unknown;

            foreach (var crawler in Crawlers)
                if (agent.Contains(crawler.Item1))
                    return crawler.Item2;

            return HasOthers(agent, _options.Crawler.Others)
                ? Crawler.Others
                : Crawler.Unknown;
        }

        private Version GetVersion()
        {
            var agent = _useragent.UserAgent.ToLower();
            var bot = FindBot(agent);
            if (string.IsNullOrEmpty(bot))
                return new Version();

            var index = bot.IndexOf('/');
            if (index < 0)
                index = bot.IndexOf(';');

            var version = string.Empty;
            if (index > 0 && bot.Length > index + 1)
            {
                version = bot.Substring(index + 1).TrimEnd(';');
            }

            return version.ToVersion();
        }

        private static bool HasOthers(string agent, IEnumerable<string> others)
            => agent.Contains("bot", StringComparison.Ordinal)
               || others.Any(x => agent.Contains(x, StringComparison.Ordinal));

        private static string FindBot(string agent)
            => agent.Split(' ')
                .FirstOrDefault(x => x.SearchContains(CrawlerIndex)) ?? string.Empty;

        private static readonly (string, Crawler)[] Crawlers =
            EnumValues<Crawler>.Values.Select(x => (x.ToStringInvariant(), x)).ToArray();

        private static readonly IndexTree CrawlerIndex = Crawlers.Select(x => x.Item1).BuildIndexTree();
    }
}