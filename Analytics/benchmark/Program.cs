// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;

using Wangkanai.Analytics;

BenchmarkRunner.Run<AnalyticsBenchmark>();