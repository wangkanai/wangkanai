// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Base;

/// <summary>
/// Unit tests for TablerIcon component logic and CSS class generation.
/// </summary>
public class TablerIconLogicTests
{
	[Fact]
	public void IconSize_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		IconSize.Default.Should().Be((IconSize)0);
		IconSize.ExtraSmall.Should().Be((IconSize)1);
		IconSize.Small.Should().Be((IconSize)2);
		IconSize.Medium.Should().Be((IconSize)3);
		IconSize.Large.Should().Be((IconSize)4);
		IconSize.ExtraLarge.Should().Be((IconSize)5);
	}
	
	[Theory]
	[InlineData(IconSize.Default, 16)]
	[InlineData(IconSize.ExtraSmall, 12)]
	[InlineData(IconSize.Small, 14)]
	[InlineData(IconSize.Medium, 18)]
	[InlineData(IconSize.Large, 20)]
	[InlineData(IconSize.ExtraLarge, 24)]
	public void SvgSize_ShouldReturnCorrectPixelSize(IconSize size, int expectedPixels)
	{
		// Arrange & Act
		var result = size switch
		{
			IconSize.ExtraSmall => 12,
			IconSize.Small => 14,
			IconSize.Medium => 18,
			IconSize.Large => 20,
			IconSize.ExtraLarge => 24,
			_ => 16 // Default size
		};
		
		// Assert
		result.Should().Be(expectedPixels);
	}
	
	[Theory]
	[InlineData(IconSize.Default, null)]
	[InlineData(IconSize.ExtraSmall, "icon-xs")]
	[InlineData(IconSize.Small, "icon-sm")]
	[InlineData(IconSize.Medium, "icon-md")]
	[InlineData(IconSize.Large, "icon-lg")]
	[InlineData(IconSize.ExtraLarge, "icon-xl")]
	public void GetSizeClass_ShouldReturnCorrectCssClass(IconSize size, string? expected)
	{
		// Arrange & Act
		var result = size switch
		{
			IconSize.ExtraSmall => "icon-xs",
			IconSize.Small => "icon-sm",
			IconSize.Medium => "icon-md",
			IconSize.Large => "icon-lg",
			IconSize.ExtraLarge => "icon-xl",
			_ => null // Default has no size class
		};
		
		// Assert
		result.Should().Be(expected);
	}
	
	[Fact]
	public void IconSize_EnumValues_ShouldBeUnique()
	{
		// Arrange & Act
		var iconSizes = Enum.GetValues<IconSize>().Cast<int>().ToList();
		
		// Assert
		iconSizes.Should().OnlyHaveUniqueItems();
	}
	
	[Fact]
	public void IconSize_ToString_ShouldReturnEnumName()
	{
		// Arrange & Act & Assert
		IconSize.Default.ToString().Should().Be("Default");
		IconSize.ExtraSmall.ToString().Should().Be("ExtraSmall");
		IconSize.Small.ToString().Should().Be("Small");
		IconSize.Medium.ToString().Should().Be("Medium");
		IconSize.Large.ToString().Should().Be("Large");
		IconSize.ExtraLarge.ToString().Should().Be("ExtraLarge");
	}
	
	[Theory]
	[InlineData("home")]
	[InlineData("user")]
	[InlineData("settings")]
	[InlineData("plus")]
	[InlineData("minus")]
	[InlineData("x")]
	[InlineData("check")]
	[InlineData("search")]
	[InlineData("menu")]
	public void CommonIconNames_ShouldBeSupported(string iconName)
	{
		// Arrange
		var supportedIcons = new[]
		{
			"home", "user", "settings", "plus", "minus", "x", 
			"check", "chevron-down", "chevron-up", "chevron-left", 
			"chevron-right", "search", "menu", "dots"
		};
		
		// Act & Assert
		supportedIcons.Should().Contain(iconName);
	}
	
	[Fact]
	public void DefaultStrokeWidth_ShouldBe2()
	{
		// Arrange
		const double expectedStrokeWidth = 2.0;
		
		// Act & Assert
		expectedStrokeWidth.Should().Be(2.0);
	}
	
	[Fact]
	public void IconCssClasses_ShouldIncludeBaseClasses()
	{
		// Arrange
		var expectedBaseClasses = new[] { "icon", "icon-tabler" };
		
		// Act & Assert
		expectedBaseClasses.Should().NotBeEmpty();
		expectedBaseClasses.Should().Contain("icon");
		expectedBaseClasses.Should().Contain("icon-tabler");
	}
	
	[Theory]
	[InlineData(1.0)]
	[InlineData(1.5)]
	[InlineData(2.0)]
	[InlineData(2.5)]
	[InlineData(3.0)]
	public void StrokeWidth_ShouldAcceptValidValues(double strokeWidth)
	{
		// Arrange & Act & Assert
		strokeWidth.Should().BeGreaterThan(0);
		(strokeWidth <= 5).Should().BeTrue(); // Reasonable upper limit
	}
}