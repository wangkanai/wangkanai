using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Wangkanai.Detection
{
    public class CrawlerResolverTest
    {
        [Fact]
        public void Get_bot_key_lookup()
        {
            // arrange
            var list1 = new string[] { "bot/1.0", "+http://www.google.com/bot.html)" };
            var list2 = new string[] { "bot", "spinder" };

            var result = list1.Where(x => list2.Count(y => x.Contains(y)) == 1);

            Assert.Equal("bot/1.0", result.FirstOrDefault());
        }
    }
}
