// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Linq;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public class CrawlerResolver : BaseResolver
    {
        public ICrawlerFactory Crawler { get; }

        public CrawlerResolver(IUserAgentService service) : base(service)
        {
            Crawler = GetCrawler();
        }

        private CrawlerFactory GetCrawler()
        {
            if (!IsCrawler())
                return new CrawlerFactory();

            var agent = UserAgent.ToString();
            if (agent.Contains("Yahoo"))
                agent = agent.Replace("Yahoo! Slurp", "Yahoo!Slurp");

            var split = agent.ToString().Split(' ');
            var bot = split.Where(x => CrawlerCollection.Keywords.Count(y => x.ToLower().Contains(y)) == 1)
                           .FirstOrDefault();

            string name, version;
            var index = bot.IndexOf('/');

            if (index < 0)
                index = bot.IndexOf(';');

            if (index < 0)
            {
                name = bot;
                version = string.Empty;
            }
            else
            {
                name = bot.Substring(0, index);
                version = bot.Substring(index + 1).TrimEnd(';');
            }

            if (string.IsNullOrEmpty(version))
                return new CrawlerFactory(name);

            return new CrawlerFactory(name, version);
        }

        private bool IsCrawler()
        {
            var agent = _service.UserAgent.ToString().ToLower().ToLowerInvariant();

            return CrawlerCollection.Keywords.Any(keyword => agent.Contains(keyword));
        }
    }
}
