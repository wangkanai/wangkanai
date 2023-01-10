// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Analytics.Services;

[MemoryDiagnoser]
public class AnalyticsBenchmark
{
	[Benchmark]
	public void Service()
	{
		var service = new AnalyticsService();
		service.ToString();
	}
}