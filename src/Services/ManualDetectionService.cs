using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class ManualDetectionService : IDetectionService
    {
        public UserAgent UserAgent { get; }
        public IDeviceService Device { get; }
        public ICrawlerService Crawler { get; }
        public IBrowserService Browser { get; }
        public IEngineService Engine { get; }
        public IPlatformService Platform { get; }

        public ManualDetectionService(string userAgent, DetectionOptions options = null!)
        {
            var contextAccessor = new HttpContextAccessor()
            {
                HttpContext = new DefaultHttpContext()
                {
                    Request = { Headers = { { "User-Agent", new[] { userAgent } } } }
                }
            };
            var userAgentService = new UserAgentService(contextAccessor);
            UserAgent = userAgentService.UserAgent;
            Device = new DeviceService(userAgentService);
            Crawler = new CrawlerService(userAgentService, options);
            Platform = new PlatformService(userAgentService);
            Engine = new EngineService(userAgentService, Platform);
            Browser = new BrowserService(userAgentService, Platform, Engine);
        }
    }
}