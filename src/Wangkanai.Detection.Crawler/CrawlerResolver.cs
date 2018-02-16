// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;

namespace Wangkanai.Detection
{
    public class CrawlerResolver : ICrawlerResolver
    {
        public ICrawler Crawler => _crawler;
        public IUserAgent UserAgent => _service.UserAgent;

        private HttpContext _context => _service.Context;

        private readonly IUserAgentService _service;
        private readonly Crawler _crawler;

        public CrawlerResolver(IUserAgentService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;

            _crawler = GetCrawler();
        }

        private Crawler GetCrawler()
        {
            if (!IsCrawler()) return null;

            var agent = UserAgent.ToString();
            if (agent.Contains("Yahoo"))
                agent = agent.Replace("Yahoo! Slurp", "Yahoo!Slurp");

            var split = agent.ToString().Split(' ');
            var bot = split.Where(x => CrawlerCollection.Keywords.Count(y => x.ToLower().Contains(y)) == 1)
                           .FirstOrDefault();

            var index = bot.IndexOf('/');
            if (index < 0)
                index = bot.IndexOf(';');

            var name = bot.Substring(0, index);
            var version = bot.Substring(index + 1).TrimEnd(';');
            if (version == string.Empty)
                return new Crawler(name);

            return new Crawler(name, version);
        }

        private bool IsCrawler()
        {
            var agent = GetUserAgent();
            if (agent == null) return false;
            if (CrawlerCollection.Keywords.Any(keyword => agent.Contains(keyword))) return true;
            return false;
        }

        private string GetUserAgent()
        {
            if (_context == null) return "";
            if (!_context.Request.Headers["User-Agent"].Any()) return "";
            return new UserAgent(_context.Request.Headers["User-Agent"].FirstOrDefault()).ToString().ToLowerInvariant();
        }
    }
}
