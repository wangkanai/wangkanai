// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DeviceCrawlerTest : DeviceTestAbstract
    {
        [Theory]
        [InlineData("Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)")]
        [InlineData("Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)")]
        [InlineData("Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)")]
        [InlineData("Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)")]
        public void Keyword(string agent)
        {
            // arrange 
            var service = CreateService(agent);
            // act
            var device = new DeviceResolver(service).Device;
            // assert
            Assert.True(device.Crawler);
        }
    }
}