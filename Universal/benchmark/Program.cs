global using System;

global using BenchmarkDotNet.Running;
global using BenchmarkDotNet.Attributes;

using Wangkanai.Universal;

BenchmarkRunner.Run<TrackerServiceBenchmark>();