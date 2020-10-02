// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class EngineService : IEngineService
    {
        public Engine  Name    { get; }
        public Version Version { get; }
        
        public EngineService(IUserAgentService userAgentService, IPlatformService platformService)
        {
            var agent = userAgentService.UserAgent;
            var os    = platformService.Name;
            var cpu   = platformService.Processor;
            Name    = GetEngine(agent, os, cpu);
            Version = GetVersion(agent.ToString(), Name);
        }

        private static Engine GetEngine(UserAgent agent, Platform os, Processor cpu)
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

        private static Version GetVersion(string agent, Engine engine)
        {
            // Not yet implemented
            return new Version();
        } 

        private static bool IsBlink(UserAgent agent)
            => agent.Contains(Browser.Chrome)
               && agent.Contains(Engine.WebKit);

        private static bool IsEdge(UserAgent agent, Platform os)
            => agent.Contains(Engine.EdgeHTML)
               || agent.Contains("Edg")
               && (Platform.Windows | Platform.Android).HasFlag(os);
    }
}