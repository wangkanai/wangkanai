// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions;

public class AttributeExtensionsTests
{
	[Fact]
	public void SplitString_Should_Return_Empty_Array_When_Original_Is_Null()
	{
		// Arrange
		var attribute = new TestAttribute();
		var original  = (string?)null!;
		var separator = ',';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void SplitString_Should_Return_Empty_Array_When_Original_Is_Empty()
	{
		// Arrange
		var attribute = new TestAttribute();
		var original  = string.Empty;
		var separator = ',';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void SplitString_Should_Return_Array_When_Original_Has_Value()
	{
		// Arrange
		var attribute = new TestAttribute();
		var original  = "a,b,c";
		var separator = ',';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.NotEmpty(result);
		Assert.Equal(3, result.Length);
		Assert.Equal("a", result[0]);
		Assert.Equal("b", result[1]);
		Assert.Equal("c", result[2]);
	}

	[Fact]
	public void SplitString_Should_Return_Array_When_Original_Has_Value_With_Separator()
	{
		// Arrange
		var attribute = new TestAttribute { Separator = ';' };
		var original  = "a;b;c";
		var separator = ';';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.NotEmpty(result);
		Assert.Equal(3, result.Length);
		Assert.Equal("a", result[0]);
		Assert.Equal("b", result[1]);
		Assert.Equal("c", result[2]);
	}

	[Fact]
	public void SplitString_Should_Return_Array_When_Original_Has_Value_With_Separator_With_Whitespace()
	{
		// Arrange
		var attribute = new TestAttribute { Separator = ';' };
		var original  = "a; b; c";
		var separator = ';';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.NotEmpty(result);
		Assert.Equal(3, result.Length);
		Assert.Equal("a", result[0]);
		Assert.Equal("b", result[1]);
		Assert.Equal("c", result[2]);
	}

	[Fact]
	public void SplitString_Should_Return_Array_When_Original_Has_Value_With_Separator_With_Whitespace_And_Empty()
	{
		// Arrange
		var attribute = new TestAttribute { Separator = ';' };
		var original  = "a; b; ;c";
		var separator = ';';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.NotEmpty(result);
		Assert.Equal(3, result.Length);
		Assert.Equal("a", result[0]);
		Assert.Equal("b", result[1]);
		Assert.Equal("c", result[2]);
	}

	[Fact]
	public void SplitString_Should_Return_Array_When_Original_Has_Value_With_Separator_With_Whitespace_And_Empty_And_Null()
	{
		// Arrange
		var attribute = new TestAttribute { Separator = ';' };
		var original  = "a; b; ;c;";
		var separator = ';';

		// Act
		var result = attribute.SplitString(original, separator);

		// Assert
		Assert.NotEmpty(result);
		Assert.Equal(3, result.Length);
		Assert.Equal("a", result[0]);
		Assert.Equal("b", result[1]);
		Assert.Equal("c", result[2]);
	}
}

public sealed class TestAttribute : Attribute
{
	public string? Original  { get; set; }
	public char    Separator { get; set; }
}
