// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using BenchmarkDotNet.Attributes;

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Services;
using Wangkanai.Responsive.Services;

namespace Wangkanai.Responsive;

[MemoryDiagnoser()]
public class ResponsiveServiceBenchmark
{
	private static readonly string[] UserAgents =
	{
		"Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A406 Safari/8536.25",
		"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36",
		"Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0",
		"Mozilla/5.0 (iPhone; U; CPU like Mac OS X; en) AppleWebKit/420+ (KHTML, like Gecko) Version/3.0 Mobile/1A543 Safari/419.3",
		"Mozilla/5.0 (Linux; Android 4.4.2); Nexus 5 Build/KOT49H) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Mobile Safari/537.36 OPR/20.0.1396.72047"
	};

	private readonly HttpContextAccessor[] _accessors
		= UserAgents.Select(x =>
		{
			var accessor = new HttpContextAccessor()
			{
				HttpContext = new DefaultHttpContext()
			};
			accessor.HttpContext.Request.Headers["User-Agent"] = x;
			return accessor;
		})
					.ToArray();

	[Benchmark]
	public void Responsive()
	{
		foreach (var accessor in _accessors)
		{
			var context = new HttpContextService(accessor);
			var useragent = new UserAgentService(context);
			var device = new DeviceService(useragent);
			var options = new ResponsiveOptions();
			var responsive = new ResponsiveService(accessor, device, options);
			_ = responsive.View;
		}
	}
}
