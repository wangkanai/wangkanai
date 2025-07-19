// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

[MemoryDiagnoser]
public class ForLoopBenchmark
{
	[Benchmark]
	public void ForEnumeratorList()
	{
		var list = new List<string>();
		for (var i = 0; i < 100; i++)
			list.Add(i.ToString());
	}

	[Benchmark]
	public void ForEnumeratorArray()
	{
		var array = new string[100];
		for (var i = 0; i < 100; i++)
			array[i] = i.ToString();
	}
}
