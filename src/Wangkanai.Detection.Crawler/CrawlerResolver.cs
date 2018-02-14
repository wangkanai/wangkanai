using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Detection
{
    public class CrawlerResolver : ICrawlerResolver
    {
        public ICrawler Crawler => _crawler;
        public IUserAgent UserAgent => _service.UserAgent;

        private HttpContext _context => _service.Context;

        private readonly IDetectionService _service;
        private readonly Crawer _crawler;

        public CrawlerResolver(IDetectionService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            _service = service;

            _crawler = GetCrawler();
        }

        private Crawer GetCrawler()
        {
            if (!IsCrawler()) return null;

            var split = UserAgent.ToString().Split(' ');
            var bot = split.FirstOrDefault()

            return new Crawer("test");
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
