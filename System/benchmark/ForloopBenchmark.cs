// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[MemoryDiagnoser]
public class ForloopBenchmark
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