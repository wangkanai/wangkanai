// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PlatformService : IPlatformService
    {
        public Processor Processor { get; }
        public Platform  Name      { get; }

        public PlatformService(IUserAgentService userAgentService)
        {
            var userAgent = userAgentService.UserAgent;
            Name      = ParseOperatingSystem(userAgent);
            Processor = ParseProcessor(userAgent, Name);
        }

        private static Platform ParseOperatingSystem(UserAgent agent)
        {
            // Unknown
            if (agent.IsNullOrEmpty())
                return Platform.Unknown;

            // Google Android
            if (agent.Contains(Platform.Android))
                return Platform.Android;
            // Microsoft Windows
            if (agent.Contains(Platform.Windows))
                return Platform.Windows;
            // Apple iOS
            if (IsiOS(agent))
                return Platform.iOS;
            // Apple Mac
            if (agent.Contains(Platform.Mac))
                return Platform.Mac;
            // Linux Distribution
            if (agent.Contains(Platform.Linux))
                return Platform.Linux;

            return Platform.Others;
        }

        private static Processor ParseProcessor(UserAgent agent, Platform os)
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

        private static bool IsArm(UserAgent agent, Platform os)
            => agent.Contains(Processor.ARM)
               || agent.Contains(Platform.Android)
               || os == Platform.iOS;

        private static bool IsPowerPC(UserAgent agent, Platform os)
            => os == Platform.Mac
               && !agent.Contains("PPC");

        private static bool IsX86(UserAgent agent)
            => agent.Contains(Processor.x86)
               || agent.Contains(new[] {"i86", "i686"});

        private static bool IsX64(UserAgent agent)
            => agent.Contains(Processor.x64)
               || agent.Contains("x86_64")
               || agent.Contains("wow64");

        private static bool IsiOS(UserAgent agent)
            => agent.Contains(Platform.iOS)
               || agent.Contains(new[] {"iPad", "iPhone", "iPod"});
    }
}