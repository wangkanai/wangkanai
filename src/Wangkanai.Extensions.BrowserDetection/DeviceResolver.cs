// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Wangkanai.Extensions.BrowserDetection.Platforms;

namespace Wangkanai.Extensions.BrowserDetection
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
                new MobileKeywordBrowser()
            };
            foreach (var browser in browsers)
                if (browser.IsValid(_request))
                    return DeviceBuilder.Mobile();

            return DeviceBuilder.Desktop();
        }
    }
}