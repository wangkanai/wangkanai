// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class BrowserService : IBrowserService
    {
        public Browser Type { get; }

        public BrowserService(IUserAgentService userAgentService)
        {
            var agent = userAgentService.UserAgent;
            Type = ParseBrowser(agent);
        }

        private static Browser ParseBrowser(UserAgent agent)
        {
            return Browser.Chrome;
        }
    }
}
