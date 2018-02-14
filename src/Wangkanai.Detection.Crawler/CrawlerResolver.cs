using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
