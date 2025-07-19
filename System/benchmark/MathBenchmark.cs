// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

[MemoryDiagnoser]
public class MathBenchmark
{
	[Benchmark]
	public void Divider()
	{
		for (var i = 100; i < 10000; i++)
		{
			for (var j = 0; j < 10000; j++)
				Mathematical.Divider(i, j);
		}
	}
}
