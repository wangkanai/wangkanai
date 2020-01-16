// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class DefaultDetectionService : IDetectionService
    {
        public UserAgent UserAgent { get; }
        public IDeviceService Device { get; }
        public ICrawlerService Crawler { get; }
        public IBrowserService Browser { get; }
        public IEngineService Engine { get; }
        public IPlatformService Platform { get; }

        public DefaultDetectionService(
            IUserAgentService userAgentService,
            IDeviceService device,
            ICrawlerService crawler,
            IBrowserService browser,
            IEngineService engine,
            IPlatformService platform)
        {
            UserAgent = userAgentService.UserAgent;
            Device = device;
            Crawler = crawler;
            Browser = browser;
            Engine = engine;
            Platform = platform;
        }
    }
}
