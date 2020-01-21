using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class BrowserServiceTest
    {
        [Fact]
        public void Null()
        {
            var resolver = MockBrowserService(null);
            Assert.NotNull(resolver);
            Assert.Equal(Browser.Unknown, resolver.Type);
        }

        private static BrowserService MockBrowserService(string agent)
        {
            var service  = MockService.CreateService(agent);
            var platform = new PlatformService(service);
            var engine   = new EngineService(service, platform);
            var resolver = new BrowserService(service, platform, engine);
            return resolver;
        }
    }
}
