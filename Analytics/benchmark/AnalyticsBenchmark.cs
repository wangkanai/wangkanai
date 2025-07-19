// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
