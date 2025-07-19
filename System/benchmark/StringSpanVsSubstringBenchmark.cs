// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using BenchmarkDotNet.Jobs;

using Wangkanai.Extensions;

namespace Wangkanai;

[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class StringSpanVsSubstringBenchmark
{
	private const string Str = "Hello World";

	[Benchmark] public void AsSpan() => Str.ToTitleCase();
}
