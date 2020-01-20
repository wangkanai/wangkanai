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
            if (agent.Contains(OperatingSystem.Windows))
                return OperatingSystem.Windows;
            if (agent.Contains(OperatingSystem.Mac))
                return OperatingSystem.Mac;
            if (agent.Contains(OperatingSystem.iOS))
                return OperatingSystem.iOS;
            if (agent.Contains(OperatingSystem.Linux))
                return OperatingSystem.Linux;

            return OperatingSystem.Others;
        }

        private static Processor ParseProcessor(UserAgent agent)
        {
            if (agent.Contains(Processor.ARM))
                return Processor.ARM;
            if (agent.Contains(Processor.x86))
                return Processor.x86;
            if (agent.Contains(Processor.x64))
                return Processor.x64;

            return Processor.Others;
        }
    }
}
