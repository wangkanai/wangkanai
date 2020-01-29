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
            var resolver = MockService.CreateResponsiveService(null);
            Assert.NotNull(resolver);
            Assert.Equal(Device.Desktop, resolver.View);
        }

        [Fact]
        public void Ctor_Null_Options_Null()
        {
            var resolver = MockService.CreateResponsiveService(null, null);
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

            Assert.Equal(Device.Desktop, MockService.CreateResponsiveService("mobile", options).View);
            Assert.Equal(Device.Desktop, MockService.CreateResponsiveService("tablet", options).View);
            Assert.Equal(Device.Desktop, MockService.CreateResponsiveService("desktop", options).View);
        }

        [Fact]
        public void DefaultTablet()
        {
            var options = new DetectionOptions();
            options.Responsive.DefaultDesktop = Device.Tablet;
            options.Responsive.DefaultTablet  = Device.Tablet;
            options.Responsive.DefaultMobile  = Device.Tablet;

            Assert.Equal(Device.Tablet, MockService.CreateResponsiveService("mobile", options).View);
            Assert.Equal(Device.Tablet, MockService.CreateResponsiveService("tablet", options).View);
            Assert.Equal(Device.Tablet, MockService.CreateResponsiveService("desktop", options).View);
        }

        [Fact]
        public void DefaultMobile()
        {
            var options = new DetectionOptions();
            options.Responsive.DefaultDesktop = Device.Mobile;
            options.Responsive.DefaultTablet  = Device.Mobile;
            options.Responsive.DefaultMobile  = Device.Mobile;

            Assert.Equal(Device.Mobile, MockService.CreateResponsiveService("mobile", options).View);
            Assert.Equal(Device.Mobile, MockService.CreateResponsiveService("tablet", options).View);
            Assert.Equal(Device.Mobile, MockService.CreateResponsiveService("desktop", options).View);
        }
    }
}