// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0



using Wangkanai.Universal.Services;

[MemoryDiagnoser]
public class TrackerServiceBenchmark
{
    [Benchmark]
    public void Track()
    {
        var tracker = new TrackerService();
        tracker.ToString();
    }
}