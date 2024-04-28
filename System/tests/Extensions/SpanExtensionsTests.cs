// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class SpanExtensionsTests
{
	[Fact]
	public void IsNull()
	{
		// Arrange
		Span<int> _null = null;

		// Act
		var result = _null.IsNull();

		// Assert
		Assert.True(result);

	}

	[Fact]
	public void IsEmpty()
	{
		// Arrange
		var span = new Span<int>();

		// Act
		var result = span.IsEmpty();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void IsNullOrEmpty()
	{
		// Arrange
		Span<int> _null = null;
		Span<int> _empty = new Span<int>();
		Span<int> _span = new Span<int>(new int[] { 1, 2, 3 });

		// Assert
		Assert.True(_null.IsNullOrEmpty());
		Assert.True(_empty.IsNullOrEmpty());
		Assert.False(_span.IsNullOrEmpty());
	}
}
