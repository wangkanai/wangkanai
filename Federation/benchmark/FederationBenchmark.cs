// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Federation.Services;

namespace Wangkanai.Federation;

[MemoryDiagnoser]
public class FederationBenchmark
{
	[Benchmark]
	public void MarkerServices() => new FederationMarkerService();
}
