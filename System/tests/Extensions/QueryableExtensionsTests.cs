// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;

namespace Wangkanai.Extensions;

public class QueryableExtensionsTests
{
	private readonly List<Blog> _empty = [];

	private readonly List<Blog> _normal =
	[
		new() { Id = 1, Title = "Title 1" },
		new() { Id = 2, Title = "Title 2" },
		new() { Id = 3, Title = "Title 3" }
	];

	private readonly List<Blog> _random =
	[
		new() { Id = 1, Title = "Title 2" },
		new() { Id = 2, Title = "Title 3" },
		new() { Id = 3, Title = "Title 1" }
	];

	private readonly List<Blog> _descend =
	[
		new() { Id = 1, Title = "Title 3" },
		new() { Id = 2, Title = "Title 2" },
		new() { Id = 3, Title = "Title 1" }
	];

	[Fact]
	public void OrderBy_Empty()
	{
		// Arrange
		var source = _empty.AsQueryable();

		// Act
		var result = source.OrderBy("Title");

		// Assert
		Assert.NotNull(result);
		result.Should().BeEmpty();
	}

	[Fact]
	public void OrderBy_Normal()
	{
		// Arrange
		var source = _normal.AsQueryable();

		// Act
		var result = source.OrderBy("Title");

		// Assert
		Assert.NotNull(result);
		result.First().Title.Should().Be("Title 1");
		result.Last().Title.Should().Be("Title 3");
	}

	[Fact]
	public void OrderBy_Random()
	{
		// Arrange
		var source = _random.AsQueryable();

		// Act
		var result = source.OrderBy("Title");

		// Assert
		Assert.NotNull(result);
		result.First().Title.Should().Be("Title 1");
		result.Last().Title.Should().Be("Title 3");
	}

	[Fact]
	public void ApplyOrder_Normal()
	{
		// Arrange
		var source = _normal.AsQueryable();

		// Act
		var result = source.ApplyOrder("Title", nameof(Queryable.OrderBy));

		// Assert
		Assert.NotNull(result);
		result.First().Title.Should().Be("Title 1");
		result.Last().Title.Should().Be("Title 3");
	}

	[Fact]
	public void ApplyOrder_Descend()
	{
		// Arrange
		var source = _normal.AsQueryable();

		// Act
		var result = source.ApplyOrder("Title", nameof(Queryable.OrderByDescending));

		// Assert
		Assert.NotNull(result);
		result.First().Title.Should().Be("Title 3");
		result.Last().Title.Should().Be("Title 1");
	}
}

public class Blog
{
	public int Id { get; set; }
	public string? Title { get; set; }
}
