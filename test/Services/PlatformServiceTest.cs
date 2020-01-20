using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Services
{
    public class PlatformServiceTest
    {
        [Theory]
        [InlineData("Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0", OperatingSystem.Windows, Processor.x64)]
        [InlineData("Mozilla/5.0 (Windows x86; rv:19.0) Gecko/20100101 Firefox/19.0", OperatingSystem.Windows, Processor.x86)]
        [InlineData("Mozilla/5.0 (Windows NT 10.0; ARM; RM-1096) AppleWebKit/537.36 (KHTML like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393", OperatingSystem.Windows, Processor.ARM)]
        public void Windows(string agent, OperatingSystem os, Processor processor)
        {
            var service = MockService.CreateService(agent);
            var resolver = new PlatformService(service);
            Assert.Equal(os, resolver.OperatingSystem);
            Assert.Equal(processor, resolver.Processor);
        }
    }
}
