// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PlatformService : IPlatformService
    {
        public Processor       Processor       { get; }
        public OperatingSystem OperatingSystem { get; }

        public PlatformService(IUserAgentService userAgentService)
        {
            var userAgent = userAgentService.UserAgent;
            OperatingSystem = ParseOperatingSystem(userAgent);
            Processor       = ParseProcessor(userAgent, OperatingSystem);
        }

        private static OperatingSystem ParseOperatingSystem(UserAgent agent)
        {
            // Unknown
            if (agent.IsNullOrEmpty())
                return OperatingSystem.Unknown;

            // Google Android
            if (agent.Contains(OperatingSystem.Android))
                return OperatingSystem.Android;
            // Microsoft Windows
            if (agent.Contains(OperatingSystem.Windows))
                return OperatingSystem.Windows;
            // Apple iOS
            if (IsiOS(agent))
                return OperatingSystem.iOS;
            // Apple Mac
            if (agent.Contains(OperatingSystem.Mac))
                return OperatingSystem.Mac;
            // Linux Distribution
            if (agent.Contains(OperatingSystem.Linux))
                return OperatingSystem.Linux;

            return OperatingSystem.Others;
        }

        private static Processor ParseProcessor(UserAgent agent, OperatingSystem os)
        {
            if (IsArm(agent, os))
                return Processor.ARM;
            if (IsX64(agent))
                return Processor.x64;
            if (IsX86(agent))
                return Processor.x86;
            if (IsPowerPC(agent, os))
                return Processor.x64;

            return Processor.Others;
        }

        private static bool IsArm(UserAgent agent, OperatingSystem os)
            => agent.Contains(Processor.ARM)
               || agent.Contains(OperatingSystem.Android)
               || os == OperatingSystem.iOS;

        private static bool IsPowerPC(UserAgent agent, OperatingSystem os)
            => os == OperatingSystem.Mac
               && !agent.Contains("PPC");

        private static bool IsX86(UserAgent agent)
            => agent.Contains(Processor.x86)
               || agent.Contains(new[] {"i86", "i686"});

        private static bool IsX64(UserAgent agent)
            => agent.Contains(Processor.x64)
               || agent.Contains("x86_64")
               || agent.Contains("wow64");

        private static bool IsiOS(UserAgent agent)
            => agent.Contains(OperatingSystem.iOS)
               || agent.Contains(new[] {"iPad", "iPhone", "iPod"});
    }
}
