// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PlatformService : IPlatformService
    {
        public PlatformService(IUserAgentService userAgentService)
        {
            var userAgent = userAgentService.UserAgent;
            Processor = TryParseProcessor(userAgent);
            OperatingSystem = TryParseOperatingSystem(userAgent);
        }

        public Processor Processor { get; }
        public OperatingSystem OperatingSystem { get; }

        private static OperatingSystem TryParseOperatingSystem(UserAgent agent)
        {
            return OperatingSystem.Others;
        }

        private static Processor TryParseProcessor(UserAgent agent)
        {
            return Processor.Others;
        }
    }
}
