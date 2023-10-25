// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using BenchmarkDotNet.Jobs;

using Wangkanai.Extensions;

namespace Wangkanai;

[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class StringSpanVsSubstringBenchmark
{
	private const string Str = "Hello World";

	[Benchmark] public void AsSpan()    => Str.ToTitleCase();
}
