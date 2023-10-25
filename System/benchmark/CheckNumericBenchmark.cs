// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using BenchmarkDotNet.Jobs;

using Xunit;

namespace Wangkanai;

[SimpleJob(RuntimeMoniker.Net80)]
[RPlotExporter]
[MemoryDiagnoser]
public class CheckNumericBenchmark
{
	private byte?    _byte1     = null;
	private short?   _short2    = null;
	private int?     _int4      = null;
	private long?    _long8     = null;
	private float?   _float16   = null;
	private double?  _double32  = null;
	private decimal? _decimal32 = null;

	[Benchmark] public void Byte()    => Assert.Throws<ArgumentNullException>(() => _byte1.ThrowIfNull());
	[Benchmark] public void Short()   => Assert.Throws<ArgumentNullException>(() => _short2.ThrowIfNull());
	[Benchmark] public void Int()     => Assert.Throws<ArgumentNullException>(() => _int4.ThrowIfNull());
	[Benchmark] public void Long()    => Assert.Throws<ArgumentNullException>(() => _long8.ThrowIfNull());
	[Benchmark] public void Float()   => Assert.Throws<ArgumentNullException>(() => _float16.ThrowIfNull());
	[Benchmark] public void Double()  => Assert.Throws<ArgumentNullException>(() => _double32.ThrowIfNull());
	[Benchmark] public void Decimal() => Assert.Throws<ArgumentNullException>(() => _decimal32.ThrowIfNull());
}
