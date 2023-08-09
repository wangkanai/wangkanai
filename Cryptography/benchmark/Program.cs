// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

global using System.Linq;

global using BenchmarkDotNet.Running;
global using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

using Wangkanai.Cryptography;

BenchmarkRunner.Run<Adler32Benchmark>(new DebugInProcessConfig());
//BenchmarkRunner.Run<HashBenchmark>(new DebugInProcessConfig());