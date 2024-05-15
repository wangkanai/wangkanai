// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.EntityFramework.Generators;

public class NowDateTimeGeneratorTests
{
	[Fact]
	public void GeneratesTemporaryValuesDefault()
	{
		var generator = new NowDateTimeGenerator();
		Assert.False(generator.GeneratesTemporaryValues);
	}

	[Fact]
	public void GeneratesTemporaryValuesOffset()
	{
		var generator = new NowDateTimeOffsetGenerator();
		Assert.False(generator.GeneratesTemporaryValues);
	}

	[Fact]
	public void Next_WithNullEntry_ThrowsArgumentNullException()
	{
		var generator = new NowDateTimeOffsetGenerator();

		Assert.Throws<ArgumentNullException>(() => generator.Next(null));
	}

	[Fact]
	public void Next_WithValidEntry_ReturnsCurrentDateTimeOffset()
	{

	}
}

