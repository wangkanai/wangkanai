// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

using Wangkanai.Federation;

BenchmarkRunner.Run<FederationBenchmark>();