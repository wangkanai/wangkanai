using System;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services.Interfaces;

namespace Wangkanai.Detection.Services
{
    public class EngineService : IEngineService
    {
        private readonly IUserAgentService _userAgentService;
        private readonly IPlatformService _platformService;

        public EngineService(IUserAgentService userAgentService, IPlatformService platformService)
        {
            _userAgentService = userAgentService;
            _platformService = platformService;
        }

        private Engine? _name;
        public Engine Name => _name ??= GetEngine();

        private Engine GetEngine()
        {
            var agent = _userAgentService.UserAgent.ToLower();
            var os = _platformService.Name;

            if (string.IsNullOrEmpty(agent))
                return Engine.Unknown;
            if (IsEdge(agent, os))
                return Engine.Edge;
            if (IsBlink(agent))
                return Engine.Blink;
            if (agent.Contains(Engine.WebKit))
                return Engine.WebKit;
            if (agent.Contains(Engine.Trident))
                return Engine.Trident;
            if (agent.Contains(Engine.Gecko))
                return Engine.Gecko;
            if (agent.Contains(Engine.Servo))
                return Engine.Servo;
            return Engine.Others;
        }

        private static bool IsBlink(string agent)
            => agent.Contains(Browser.Chrome)
               && agent.Contains(Engine.WebKit);

        private static bool IsEdge(string agent, Platform os)
            => agent.Contains(Engine.Edge)
               || agent.Contains("edg", StringComparison.Ordinal)
               && (Platform.Windows | Platform.Android).HasFlag(os);
    }
}