using System;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace Wangkanai.Detection.Services
{
    public class ParserServiceTest
    {
        [Fact]
        public void Ctor_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ParserService(null!));
        }

        [Fact]
        public void Ctor_Empty_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ParserService(string.Empty));
        }

        [Fact]
        public void Ctor_IServiceProvider_Success()
        {
            var agent   = "Agent";
            var service = new ParserService(agent);

            Assert.NotNull(service.Context);
            Assert.NotNull(service.UserAgent);
            Assert.Equal(agent, service.UserAgent.ToString());
        }
    }
}