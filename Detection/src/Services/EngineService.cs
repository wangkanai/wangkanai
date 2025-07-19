// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Services;

public sealed class EngineService : IEngineService
{
	private readonly IPlatformService _platformService;
	private readonly IUserAgentService _userAgentService;

	private Engine? _name;

	public EngineService(IUserAgentService userAgentService, IPlatformService platformService)
	{
		_userAgentService = userAgentService;
		_platformService = platformService;
	}

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
		if (agent.ContainsLower(Engine.WebKit))
			return Engine.WebKit;
		if (agent.ContainsLower(Engine.Trident))
			return Engine.Trident;
		if (agent.ContainsLower(Engine.Gecko))
			return Engine.Gecko;
		if (agent.ContainsLower(Engine.Servo))
			return Engine.Servo;
		return Engine.Others;
	}

	private static bool IsBlink(string agent)
	{
		return agent.ContainsLower(Browser.Chrome) &&
			   agent.ContainsLower(Engine.WebKit);
	}

	private static bool IsEdge(string agent, Platform os)
	{
		return agent.ContainsLower(Engine.Edge) ||
			   agent.Contains("edg", StringComparison.Ordinal) &&
			   Platform.Windows.HasFlag(os);
	}
}
