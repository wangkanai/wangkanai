global using System;

global using BenchmarkDotNet.Running;
global using BenchmarkDotNet.Attributes;

BenchmarkRunner.Run<TrackerServiceBenchmark>();