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
            var options = new DetectionOptions();
            options.Responsive.DefaultDesktop = Device.Desktop;
            options.Responsive.DefaultTablet  = Device.Desktop;
            options.Responsive.DefaultMobile  = Device.Desktop;

            Assert.Equal(Device.Desktop, MockResponsiveService("mobile", options).View);
            Assert.Equal(Device.Desktop, MockResponsiveService("tablet", options).View);
            Assert.Equal(Device.Desktop, MockResponsiveService("desktop", options).View);
        }

        [Fact]
        public void DefaultTablet()
        {
            var options = new DetectionOptions();
            options.Responsive.DefaultDesktop = Device.Tablet;
            options.Responsive.DefaultTablet  = Device.Tablet;
            options.Responsive.DefaultMobile  = Device.Tablet;

            Assert.Equal(Device.Tablet, MockResponsiveService("mobile", options).View);
            Assert.Equal(Device.Tablet, MockResponsiveService("tablet", options).View);
            Assert.Equal(Device.Tablet, MockResponsiveService("desktop", options).View);
        }

        [Fact]
        public void DefaultMobile()
        {
            var options = new DetectionOptions();
            options.Responsive.DefaultDesktop = Device.Mobile;
            options.Responsive.DefaultTablet  = Device.Mobile;
            options.Responsive.DefaultMobile  = Device.Mobile;

            Assert.Equal(Device.Mobile, MockResponsiveService("mobile", options).View);
            Assert.Equal(Device.Mobile, MockResponsiveService("tablet", options).View);
            Assert.Equal(Device.Mobile, MockResponsiveService("desktop", options).View);
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