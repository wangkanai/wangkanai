// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using BenchmarkDotNet.Engines;

using Perfolizer.Mathematics.OutlierDetection;

using Wangkanai.Cryptography;

[MemoryDiagnoser]
[Outliers(OutlierMode.DontRemove)]
[SimpleJob(RunStrategy.Monitoring)]
public class HashBenchmark
{
    private const string text = "F41k7dPvkYUpA5zYUwtWMkV4gwzxGcw7Oh9x9PXIif6o9C7oBSoaJw3c4I5bRyokmp3AMa";

    [Benchmark]
    public void Md5()
    {
        for (int i = 0; i < 100_000; i++)
            text.HashMd5();
    }

    [Benchmark]
    public void Sha1()
    {
        for (int i = 0; i < 100_000; i++)
            text.HashSha1();
    }

    [Benchmark]
    public void Sha256()
    {
        for (int i = 0; i < 100_000; i++)
            text.HashSha256();
    }

    [Benchmark]
    public void Sha384()
    {
        for (int i = 0; i < 100_000; i++)
            text.HashSha384();
    }

    [Benchmark]
    public void Sha512()
    {
        for (int i = 0; i < 100_000; i++)
            text.HashSha512();
    }
}