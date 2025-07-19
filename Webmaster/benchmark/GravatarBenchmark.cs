// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using BenchmarkDotNet.Attributes;

using Wangkanai.Helpers;

public class GravatarBenchmark
{
	private readonly Gravatar _gravatar = new Gravatar("john@don.com");

	[Benchmark]
	public string GetGravatarUrl() => _gravatar.ToString();
}
