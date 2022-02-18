using System;
using Xunit;

namespace Wangkanai.Universal
{
    public class CreateConfigOptionTests
    {
        [Fact]
        public void TestSessionWithTrackerName()
        {
            var session = new Session();
            session.Name = "info";
            var config = new Configuration();
            var create = new Create(config, new ConfigOption(config, session));
            Assert.Equal("ga('create', 'UA-XXXX-Y', 'auto', {'Name':'info'});", create.ToString());
        }
    }
}
