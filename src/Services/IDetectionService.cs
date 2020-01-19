// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface IDetectionService
    {
        public UserAgent UserAgent { get; }
        public IDeviceService Device { get; }
        public ICrawlerService Crawler { get; }

        public IPlatformService Platform { get; }
        //public IBrowserService Browser { get; }
        //public IEngineService Engine { get; }
    }
}
