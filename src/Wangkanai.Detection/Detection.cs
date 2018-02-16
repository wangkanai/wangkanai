using System;
using System.Collections.Generic;
using System.Text;

namespace Wangkanai.Detection
{
    public class Detection : IDetection
    {
        public IUserAgent UserAgent => _service.UserAgent;
        public IDevice Device => _deviceResolver.Device;
        public IBrowser Browser => _browserResolver.Browser;
        public IPlatform Platform => _platformResolver.Platform;
        public IEngine Engine => _engineResolver.Engine;
        public ICrawler Crawler => _crawlerResolver.Crawler;

        private readonly IDetectionService _service;
        private readonly IDeviceResolver _deviceResolver;
        private readonly IBrowserResolver _browserResolver;
        private readonly IPlatformResolver _platformResolver;
        private readonly IEngineResolver _engineResolver;
        private readonly ICrawlerResolver _crawlerResolver;

        public Detection(IDetectionService service,
            IDeviceResolver deviceResolver,
            IBrowserResolver browserResolver,
            IPlatformResolver platformResolver,
            IEngineResolver engineResolver,
            ICrawlerResolver crawlerResolver)
        {
            this._service = service;
            this._deviceResolver = deviceResolver;
            this._browserResolver = browserResolver;
            this._platformResolver = platformResolver;
            this._engineResolver = engineResolver;
            this._crawlerResolver = crawlerResolver;
        }
    }
}
