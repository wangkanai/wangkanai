// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Wangkanai.Browser.Platforms;

namespace Wangkanai.Browser
{
    public class DeviceResolver
    {
        private readonly HttpRequest _request;
        public DeviceInfo DeviceInfo { get; }

        public DeviceResolver(HttpRequest request)
        {
            _request = request;
            DeviceInfo = Resolve();
        }

        private DeviceInfo Resolve()
        {
            var browsers = new List<IDeviceBrowser>
            {
                new DesktopBrowser(),
                new TabletBrowser(),
                new MobileKeywordBrowser(),
                new MobilePrefixBrowser(),                
                new WapBrowser(),
                new UAProfBrowser(),
                new OperaMiniBrowser(),
                new CrawlerBrowser()
            };
            foreach (var browser in browsers)
                if (browser.IsValid(_request))
                    return browser.DeviceInfo;

            return DeviceBuilder.Desktop();
        }
    }
}