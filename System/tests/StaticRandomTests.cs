// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public class StaticRandomTests
{
	[Fact]
	public void CheckDifferentSources()
	{
		var grabbers = new RandomGenerator[100];

		Parallel.ForEach(grabbers, (grabber, state, index) => {
			grabbers[index] = new RandomGenerator(30, true);
			grabbers[index].Generate();
		});

		Parallel.ForEach(grabbers, (grabber, state, index) => {
			for (var local = index + 1; local < grabbers.Length; local++)
				if (grabbers[index].Equals(grabbers[local]))
					Assert.Fail("Random number generator is not random");
		});
	}

	[Fact]
	public void NextDoubleShouldNotAlwaysReturnInts()
	{
		for (var i = 0; i < 10; i++)
		{
			var d = StaticRandom.NextDouble();
			if (System.Math.Abs((int)d - d) > 0)
				return;
		}

		Assert.Fail("NextDouble shouldn't return integer");
	}

	[Fact]
	public void NextShouldReturnDifferentValues()
	{
		var values = new int[100];
		for (var i = 0; i < values.Length; i++)
			values[i] = StaticRandom.Next();

		for (var i = 0; i < values.Length; i++)
		for (var j = i + 1; j < values.Length; j++)
			if (values[i] == values[j])
				Assert.Fail("Next should return different values");
	}

	[Fact]
	public void NextBytesShouldReturnDifferentValuesWithBuffer()
	{
		var values = new byte[10];
		StaticRandom.NextBytes(values);

		for (var i = 0; i < values.Length; i++)
		for (var j = i + 1; j < values.Length; j++)
			if (values[i] == values[j])
				Assert.Fail("NextBytes should return different values");
	}

	[Fact]
	public void NextBytesSpanShouldReturnDifferentValuesWithBuffer()
	{
		var values = new byte[10];
		StaticRandom.NextBytes(values.AsSpan());

		for (var i = 0; i < values.Length; i++)
		for (var j = i + 1; j < values.Length; j++)
			if (values[i] == values[j])
				Assert.Fail("NextBytes should return different values");
	}
}
