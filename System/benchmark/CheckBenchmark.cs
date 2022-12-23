// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[MemoryDiagnoser]
public class CheckBenchmark
{
    [Benchmark]
    public void CheckNull()
    {
        string i = null;
        i.IfNullThrow();
        "".IfNullThrow();
        Check.NotNullOrEmpty(i);
        Check.NotNullOrEmpty("");
    }
}