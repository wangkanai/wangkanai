// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

global using Wangkanai;

// BenchmarkRunner.Run<StringSpanVsSubstringBenchmark>();
// BenchmarkRunner.Run<CheckNumericBenchmark>();
// BenchmarkRunner.Run<HashBenchmark>();
BenchmarkRunner.Run<MathBenchmark>();
// BenchmarkRunner.Run<ForLoopBenchmark>();
// BenchmarkRunner.Run<StaticRandomTests>();