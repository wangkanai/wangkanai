// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Math = Wangkanai.Math;

[MemoryDiagnoser]
public class MathBenchmark
{
    [Benchmark]
    public void Divider()
    {
        for (int i = 100; i < 10000; i++)
        {
            for (int j = 0; j < 10000; j++) 
                Math.Divider(i, j);
        }
    }
}