// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public static class StaticRandom
{
	static readonly Random _random = new Random();
	static readonly object _lock = new object();
	
	public static int Next()
	{
		lock (_lock) return _random.Next();
	}
	
	public static int Next(int max)
	{
		lock (_lock) return _random.Next(max);
	}
	
	public static int Next(int min, int max)
	{
		lock (_lock) return _random.Next(min, max);
	}
	
	public static double NextDouble()
	{
		lock (_lock) return _random.NextDouble();
	}

	public static void NextBytes(byte[] buffer)
	{
		lock (_lock) _random.NextBytes(buffer);
	}
	public static void NextBytes(Span<byte> buffer)
	{
		lock (_lock) _random.NextBytes(buffer);
	}
}