using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class PlatformServiceTest
    {
        [Theory]
        [InlineData(Processor.x64, "Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0")]
        [InlineData(Processor.x86, "Mozilla/5.0 (Windows x86; rv:19.0) Gecko/20100101 Firefox/19.0")]
        [InlineData(Processor.ARM, "Mozilla/5.0 (Windows NT 10.0; ARM; RM-1096) AppleWebKit/537.36 (KHTML like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393")]
        public void Windows(Processor processor, string agent)
        {
            var os = OperatingSystem.Windows;
            var service = MockService.CreateService(agent);
            var resolver = new PlatformService(service);
            Assert.Equal(os, resolver.OperatingSystem);
            Assert.Equal(processor, resolver.Processor);
        }

        [Theory]
        [InlineData(Processor.x64, "")]
        [InlineData(Processor.x86, "")]
        [InlineData(Processor.ARM, "")]
        public void Android(Processor processor, string agent)
        {
            var os = OperatingSystem.Android;
            var service = MockService.CreateService(agent);
            var resolver = new PlatformService(service);
            Assert.Equal(os, resolver.OperatingSystem);
            Assert.Equal(processor, resolver.Processor);
        }

        [Theory]
        [InlineData(Processor.x64, "")]
        [InlineData(Processor.x86, "")]
        [InlineData(Processor.ARM, "")]
        public void Mac(Processor processor, string agent)
        {
            var os = OperatingSystem.Mac;
            var service = MockService.CreateService(agent);
            var resolver = new PlatformService(service);
            Assert.Equal(os, resolver.OperatingSystem);
            Assert.Equal(processor, resolver.Processor);
        }

        [Theory]
        [InlineData(Processor.x64, "")]
        [InlineData(Processor.x86, "")]
        [InlineData(Processor.ARM, "")]
        public void Linux(Processor processor, string agent)
        {
            var os = OperatingSystem.Linux;
            var service = MockService.CreateService(agent);
            var resolver = new PlatformService(service);
            Assert.Equal(os, resolver.OperatingSystem);
            Assert.Equal(processor, resolver.Processor);
        }

        [Theory]
        [InlineData(Processor.x64, "")]
        [InlineData(Processor.x86, "")]
        [InlineData(Processor.ARM, "")]
        public void Others(Processor processor, string agent)
        {
            var os = OperatingSystem.Others;
            var service = MockService.CreateService(agent);
            var resolver = new PlatformService(service);
            Assert.Equal(os, resolver.OperatingSystem);
            Assert.Equal(processor, resolver.Processor);
        }
    }
}
