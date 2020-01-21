using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class EngineServiceTest
    {
        [Theory]
        [InlineData(Engine.Trident,"Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko")]
        [InlineData(Engine.Trident,"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)")]
        [InlineData(Engine.Trident,"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)")]
        [InlineData(Engine.Trident,"Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko")]
        public void Trident(Engine engine, string agent)
        {
            var resolver = MockEngineService(agent);
            Assert.Equal(engine, resolver.Type);
        }



        [Theory]
        [InlineData(Engine.WebKit,"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0 Safari/605.1.15")]
        [InlineData(Engine.WebKit,"Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1")]
        [InlineData(Engine.WebKit,"")]
        public void WebKit(Engine engine, string agent)
        {
            var resolver = MockEngineService(agent);
            Assert.Equal(engine, resolver.Type);
        }

        [Theory]
        [InlineData(Engine.Blink,"")]
        [InlineData(Engine.Blink,"")]
        [InlineData(Engine.Blink,"")]
        public void Blink(Engine engine, string agent)
        {
            var resolver = MockEngineService(agent);
            Assert.Equal(engine, resolver.Type);
        }

        [Theory]
        [InlineData(Engine.Gecko,"Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData(Engine.Gecko,"Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)")]
        [InlineData(Engine.Gecko,"")]
        public void Gecko(Engine engine, string agent)
        {
            var resolver = MockEngineService(agent);
            Assert.Equal(engine, resolver.Type);
        }

        [Theory]
        [InlineData(Engine.EdgeHTML,"")]
        [InlineData(Engine.EdgeHTML,"")]
        [InlineData(Engine.EdgeHTML,"")]
        public void Edge(Engine engine, string agent)
        {
            var resolver = MockEngineService(agent);
            Assert.Equal(engine, resolver.Type);
        }

        [Theory]
        [InlineData(Engine.Servo,"")]
        [InlineData(Engine.Servo,"")]
        [InlineData(Engine.Servo,"")]
        public void Servo(Engine engine, string agent)
        {
            var resolver = MockEngineService(agent);
            Assert.Equal(engine, resolver.Type);
        }

        private static EngineService MockEngineService(string agent)
        {
            var service = MockService.CreateService(agent);
            var platform = new PlatformService(service);
            var resolver = new EngineService(service, platform);
            return resolver;
        }
    }
}
