// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

global using Wangkanai;

// BenchmarkRunner.Run<StringSpanVsSubstringBenchmark>();
// BenchmarkRunner.Run<CheckNumericBenchmark>();
BenchmarkRunner.Run<CheckStringBenchmark>();
// BenchmarkRunner.Run<HashBenchmark>();
// BenchmarkRunner.Run<MathBenchmark>();
// BenchmarkRunner.Run<ForLoopBenchmark>();
// BenchmarkRunner.Run<StaticRandomTests>();
