// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

global using System.Linq;

using BenchmarkDotNet.Running;

using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<DeviceServiceBenchmark>();
        }
    }
}