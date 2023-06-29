// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Services;

namespace Wangkanai.Detection;

[MemoryDiagnoser]
public class DetectionBenchmark
{
	private static readonly string[] RawUserAgents =
	{
		"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36",
		"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36",
		"Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36",
		"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36",
		"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36",
		"Mozilla/5.0 (iPhone; CPU iPhone OS 13_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/81.0.4044.124 Mobile/15E148 Safari/604.1",
		"Mozilla/5.0 (iPad; CPU OS 13_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/81.0.4044.124 Mobile/15E148 Safari/604.1",
		"Mozilla/5.0 (iPod; CPU iPhone OS 13_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/81.0.4044.124 Mobile/15E148 Safari/604.1",
		"Mozilla/5.0 (Linux; Android 10) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; SM-A205U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; SM-A102U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; SM-G960U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; SM-N960U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; LM-Q720) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; LM-X420) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36",
		"Mozilla/5.0 (Linux; Android 10; LM-Q710(FGN)) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.117 Mobile Safari/537.36"
	};

	private readonly HttpContextAccessor[] Accessors
		= RawUserAgents.Select(x => {
			               var accessor = new HttpContextAccessor
			               {
				               HttpContext = new DefaultHttpContext()
			               };
			               accessor.HttpContext.Request.Headers["User-Agent"] = x;
			               return accessor;
		               })
		               .ToArray();

	[Benchmark]
	public void Device()
	{
		foreach (var accessor in Accessors)
		{
			var contextService   = new HttpContextService(accessor);
			var userAgentService = new UserAgentService(contextService);
			var deviceService    = new DeviceService(userAgentService);
			_ = deviceService.Type;
		}
	}

	[Benchmark]
	public void Platform()
	{
		foreach (var accessor in Accessors)
		{
			var contextService   = new HttpContextService(accessor);
			var userAgentService = new UserAgentService(contextService);
			var platformService  = new PlatformService(userAgentService);
			_ = platformService.Name;
		}
	}

	[Benchmark]
	public void Engine()
	{
		foreach (var accessor in Accessors)
		{
			var contextService   = new HttpContextService(accessor);
			var userAgentService = new UserAgentService(contextService);
			var platformService  = new PlatformService(userAgentService);
			var engineService    = new EngineService(userAgentService, platformService);
			_ = engineService.Name;
		}
	}

	[Benchmark]
	public void Browser()
	{
		foreach (var accessor in Accessors)
		{
			var contextService   = new HttpContextService(accessor);
			var userAgentService = new UserAgentService(contextService);
			var platformService  = new PlatformService(userAgentService);
			var engineService    = new EngineService(userAgentService, platformService);
			var browserService   = new BrowserService(userAgentService, engineService);
			_ = browserService.Name;
			_ = browserService.Version;
		}
	}
}