// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;

using Xunit;

namespace Wangkanai;

public class StaticRandomTests
{
	[Fact]
	public void CheckDifferentSources()
	{
		var grabbers = new RandomGenerator[100];
		var threads  = new Thread[grabbers.Length];

		for (var i = 0; i < grabbers.Length; i++)
		{
			grabbers[i] = new RandomGenerator(30, true);
			threads[i]  = new Thread(grabbers[i].Generate);
		}

		for (var i = 0; i < grabbers.Length; i++) 
			threads[i].Start();

		for (var i = 0; i < grabbers.Length; i++) 
			threads[i].Join();

		for (var i = 0; i < grabbers.Length -1; i++)
		{
			for (var j = i +1; j < grabbers.Length; j++)
				if (grabbers[i].Equals(grabbers[j]))
					Assert.Fail("Random number generator is not random");
		}
	}

	[Fact]
	public void NextDoubleShouldNotAlwaysReturnInts()
	{
		for (var i = 0; i < 10; i++)
		{
			var d = StaticRandom.NextDouble();
			if((int)d != d)
				return;
		}
		Assert.Fail("NextDouble shouldn't return integer");
	}
}