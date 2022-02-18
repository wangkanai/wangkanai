using System;
using Xunit;

namespace Wangkanai.Universal
{
    public class EventTests
    {
        [Fact]
        public void TestEventCategoryOnly()
        {
            Event categoryevent = new Event("button","click","submit","1");
            Assert.Equal("'event','button','click','submit','1'", categoryevent.ToString());
        }
    }
}
