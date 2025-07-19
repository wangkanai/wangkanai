// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Analytics.Services;

namespace Wangkanai.Analytics;

[MemoryDiagnoser]
public class AnalyticsBenchmark
{
	[Benchmark]
	public void Service()
	{
		var service = new AnalyticsService();
		service.ThrowIfNull();
	}
}
