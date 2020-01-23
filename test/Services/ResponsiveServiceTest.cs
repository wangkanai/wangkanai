using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class ResponsiveServiceTest
    {
        [Fact]
        public void Ctor_Null()
        {
            var resolver = MockResponsiveService(null);
            Assert.NotNull(resolver);
            Assert.Equal(Device.Desktop, resolver.View);
        }

        [Fact]
        public void Ctor_Null_Options_Null()
        {
            var resolver = MockResponsiveService(null, null);
            Assert.NotNull(resolver);
            Assert.Equal(Device.Desktop, resolver.View);
        }

        [Fact]
        public void DefaultDesktop()
        {
        }

        private static ResponsiveService MockResponsiveService(string agent, DetectionOptions options = null)
        {
            var service    = MockService.CreateService(agent);
            var device     = new DeviceService(service);
            var preference = new PreferenceService();
            var resolver   = new ResponsiveService(device, preference, options);
            return resolver;
        }
    }
}