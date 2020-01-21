// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class EngineService : IEngineService
    {
        public Engine Type { get; }

        public EngineService(IUserAgentService userAgentService, IPlatformService platformService)
        {
            var agent = userAgentService.UserAgent;
            var os = platformService.OperatingSystem;
            var cpu = platformService.Processor;
            Type = ParseEngine(agent, os, cpu);
        }

        private static Engine ParseEngine(UserAgent agent, OperatingSystem os, Processor cpu)
        {
            return Engine.Unknown;
        }
    }
}
