// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using BenchmarkDotNet.Attributes;

using Wangkanai.Helpers;

public class GravatarBenchmark
{
    private readonly Gravatar _gravatar = new Gravatar("john@don.com");

    [Benchmark]
    public string GetGravatarUrl()
    {
        return _gravatar.ToString();
    }
}