// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;


BenchmarkRunner.Run<FirstBenchmark>();

[MemoryDiagnoser]
public class FirstBenchmark
{
	[Benchmark]
	public void Benchmark()
	{
		// Method intentionally left empty.
	}
}
