// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Cryptography;

[MemoryDiagnoser]
public class Adler32Benchmark
{
	private const string text = "F41k7dPvkYUpA5zYUwtWMkV4gwzxGcw7Oh9x9PXIif6o9C7oBSoaJw3c4I5bRyokmp3AMa";

	[Benchmark]
	public void Adler32Bytes()
	{
		for (int i = 0; i < 100_000; i++)
			Adler32.Checksum(text);
	}
}
