using Microsoft.Extensions.DependencyInjection;
using System;
using Wangkanai.Detection.Builder;
using Xunit;

namespace Wangkanai.Detection.Test.Builder
{
    public class DetectionBuilderTests
    {
        [Fact]
        public void Cotr_IServiceCollection_Success()
        {
            var serviceCollection = new ServiceCollection();
            var builder = new DetectionBuilder(serviceCollection);

            Assert.NotNull(builder.Services);
        }

        [Fact]
        public void Cotr_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (new DetectionBuilder(null)));
        }
    }
}
