// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions;

public class DateTimeExtensionsTests
{
	[Fact]
	public void Truncate()
	{
		// Arrange
		var dateTime = new DateTime(2022, 1, 1, 12, 30, 30);
		var timeSpan = new TimeSpan(0, 0, 0, 0, 500);

		// Act
		var expected = new DateTime(2022, 1, 1, 12, 30, 30);
		var actual = dateTime.Truncate(timeSpan);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Truncate_Zero()
	{
		// Arrange
		var dateTime = new DateTime(2022, 1, 1, 12, 30, 30);
		var timeSpan = TimeSpan.Zero;

		// Act
		var expected = new DateTime(2022, 1, 1, 12, 30, 30);
		var actual = dateTime.Truncate(timeSpan);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Truncate_Negative()
	{
		// Arrange
		var dateTime = new DateTime(2022, 1, 1, 12, 45, 15);
		var timeSpan = new TimeSpan(-1, 0, 0, 0, 0);

		// Act
		var expected = new DateTime(2022, 1, 1, 0, 0, 0);
		var actual = dateTime.Truncate(timeSpan);

		// Assert
		Assert.Equal(expected, actual);
	}
}
