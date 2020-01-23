// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class EngineService : IEngineService
    {
        public Engine Type { get; }

        public EngineService(IUserAgentService userAgentService, IPlatformService platformService)
        {
            var agent = userAgentService.UserAgent;
            var os    = platformService.OperatingSystem;
            var cpu   = platformService.Processor;
            Type = ParseEngine(agent, os, cpu);
        }

        private static Engine ParseEngine(UserAgent agent, OperatingSystem os, Processor cpu)
        {
            // Null check
            if (agent.IsNullOrEmpty())
                return Engine.Unknown;
            
            // Microsoft Edge
            if (IsEdge(agent, os))
                return Engine.EdgeHTML;
            // Google Blink
            if (IsBlink(agent))
                return Engine.Blink;
            // Apple WebKit
            if (agent.Contains(Engine.WebKit))
                return Engine.WebKit;
            // Microsoft Trident
            if (agent.Contains(Engine.Trident))
                return Engine.Trident;
            // Mozilla Gecko
            if (agent.Contains(Engine.Gecko))
                return Engine.Gecko;
            // Sumsang Servo
            if (agent.Contains(Engine.Servo))
                return Engine.Servo;

            return Engine.Others;
        }

        private static bool IsBlink(UserAgent agent)
            => agent.Contains(Browser.Chrome)
               && agent.Contains(Engine.WebKit);

        private static bool IsEdge(UserAgent agent, OperatingSystem os)
            => agent.Contains(Engine.EdgeHTML)
               || agent.Contains("Edg")
               && (OperatingSystem.Windows | OperatingSystem.Android).HasFlag(os);
    }
}
