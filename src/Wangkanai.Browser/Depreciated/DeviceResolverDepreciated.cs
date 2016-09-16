// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Wangkanai.Browser.Depreciated;

namespace Wangkanai.Browser
{
    public class DeviceResolverDepreciated
    {
        private readonly HttpRequest _request;
        public DeviceInfoDepreciated DeviceInfoDepreciated { get; }

        public DeviceResolverDepreciated(HttpRequest request)
        {
            _request = request;
            DeviceInfoDepreciated = Resolve();
        }

        private DeviceInfoDepreciated Resolve()
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
                    return browser.DeviceInfoDepreciated;

            return DeviceBuilderDepreciated.Desktop();
        }
    }
}