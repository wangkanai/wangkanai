// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

global using System.Linq;

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

using Wangkanai.Detection;

BenchmarkRunner.Run<DetectionBenchmark>();
//BenchmarkRunner.Run<TrieBenchmark>();
