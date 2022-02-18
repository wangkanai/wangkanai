using System;
using Xunit;

namespace Wangkanai.Universal
{
    public class RequireTests
    {
        [Fact]
        public void TestDisplayFeature()
        {
            var config = (Configuration)System.Configuration.ConfigurationManager.GetSection("AnalyticConfigurationSettings/AnalyticConfiguration");
            var require = new DisplayFeatures(config);
            Assert.Equal("ga('require', 'displayfeatures');", require.ToString());
        }
    }
}
