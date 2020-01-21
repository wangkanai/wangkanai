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
            OperatingSystem = ParseOperatingSystem(userAgent);
            Processor = ParseProcessor(userAgent, OperatingSystem);
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

        private static Processor ParseProcessor(UserAgent agent, OperatingSystem os)
        {
            if (agent.Contains(Processor.ARM) || agent.Contains(OperatingSystem.Android))
                return Processor.ARM;
            if (agent.Contains(Processor.x64) || agent.Contains("x86_64") || agent.Contains("wow64"))
                return Processor.x64;
            var x86 = new[] {"i86", "i686"};
            if (agent.Contains(Processor.x86) || agent.Contains(x86))
                return Processor.x86;
            if (os == OperatingSystem.Mac && !agent.Contains("PPC"))
                return Processor.x64;

            return Processor.Others;
        }
    }
}
