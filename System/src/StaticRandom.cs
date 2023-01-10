// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public static class StaticRandom
{
	static Random _random = new Random();
	static object _lock = new object();
	
	public static int Next()
	{
		lock (_lock)
			return _random.Next();
	}
}