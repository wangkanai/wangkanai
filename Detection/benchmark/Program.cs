// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

global using System.Linq;
global using BenchmarkDotNet.Running;
global using BenchmarkDotNet.Attributes;

using Wangkanai.Detection;

BenchmarkRunner.Run<DetectionBenchmark>();