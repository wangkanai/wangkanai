// See https://aka.ms/new-console-template for more information

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

global using Wangkanai;

BenchmarkRunner.Run<HashBenchmark>();
// BenchmarkRunner.Run<MathBenchmark>();
// BenchmarkRunner.Run<ForloopBenchmark>();
// BenchmarkRunner.Run<StaticRandomTests>();