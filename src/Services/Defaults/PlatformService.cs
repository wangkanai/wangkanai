// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PlatformService : IPlatformService
    {
        public Processor Processor { get; }
        public OperatingSystem OperatingSystem { get; }

        public PlatformService(IUserAgentService userAgentService)
        {
            var userAgent = userAgentService.UserAgent;
            Processor = ParseProcessor(userAgent);
            OperatingSystem = ParseOperatingSystem(userAgent);
        }

        private static OperatingSystem ParseOperatingSystem(UserAgent agent)
        {
            if (agent.Contains(OperatingSystem.Android))
                return OperatingSystem.Android;

            return OperatingSystem.Others;
        }

        private static Processor ParseProcessor(UserAgent agent)
        {
            return Processor.Others;
        }
    }
}
