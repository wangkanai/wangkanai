// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Services;

[MemoryDiagnoser]
public class FederationBenchmark
{
	[Benchmark]
	public void MarkerServices() => new FederationMarkerService();
}