// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.EntityFramework.Tests.Generators;

[TestSubject(typeof(NowDateTimeOffsetGenerator))]
public class NowDateTimeOffsetGeneratorTests
{
	private readonly NowDateTimeOffsetGenerator _generator = new();

	[Fact]
	public void GeneratesTemporaryValues_IsFalse()
	{
		Assert.False(_generator.GeneratesTemporaryValues);
	}

	[Fact]
	public void Next_WithNullEntityEntry_ThrowsArgumentNullException()
	{
		void Action() => _generator.Next(null!);

		Assert.Throws<ArgumentNullException>(Action);
	}

	// [Fact]
	// public void Next_WithValidEntityEntry_ReturnsCurrentDateTimeOffset()
	// {
	// 	var entityEntryMock = new Mock<EntityEntry>();
	// 	var now             = DateTimeOffset.Now;
	// 	var next            = _generator.Next(entityEntryMock.Object);
	//
	// 	Assert.True(next >= now);
	// }
}
