// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Base;

/// <summary>
/// Unit tests for TablerButton component logic and CSS class generation.
/// </summary>
public class TablerButtonLogicTests
{
	[Fact]
	public void ButtonColor_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		ButtonColor.None.Should().Be((ButtonColor)0);
		ButtonColor.Primary.Should().Be((ButtonColor)1);
		ButtonColor.Secondary.Should().Be((ButtonColor)2);
		ButtonColor.Success.Should().Be((ButtonColor)3);
		ButtonColor.Warning.Should().Be((ButtonColor)4);
		ButtonColor.Danger.Should().Be((ButtonColor)5);
		ButtonColor.Info.Should().Be((ButtonColor)6);
		ButtonColor.Light.Should().Be((ButtonColor)7);
		ButtonColor.Dark.Should().Be((ButtonColor)8);
		ButtonColor.Ghost.Should().Be((ButtonColor)9);
		ButtonColor.Outline.Should().Be((ButtonColor)10);
	}
	
	[Fact]
	public void ButtonSize_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		ButtonSize.Medium.Should().Be((ButtonSize)0);
		ButtonSize.Small.Should().Be((ButtonSize)1);
		ButtonSize.Large.Should().Be((ButtonSize)2);
	}
	
	[Fact]
	public void ButtonVariant_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		ButtonVariant.Solid.Should().Be((ButtonVariant)0);
		ButtonVariant.Outline.Should().Be((ButtonVariant)1);
		ButtonVariant.Ghost.Should().Be((ButtonVariant)2);
	}
	
	[Theory]
	[InlineData(ButtonColor.Primary, ButtonVariant.Solid, "btn-primary")]
	[InlineData(ButtonColor.Secondary, ButtonVariant.Solid, "btn-secondary")]
	[InlineData(ButtonColor.Success, ButtonVariant.Outline, "btn-outline-success")]
	[InlineData(ButtonColor.Warning, ButtonVariant.Ghost, "btn-ghost-warning")]
	[InlineData(ButtonColor.None, ButtonVariant.Solid, "")]
	public void GetColorClass_ShouldReturnCorrectCssClass(ButtonColor color, ButtonVariant variant, string expected)
	{
		// Arrange
		var colorName = color == ButtonColor.None ? string.Empty : color.ToString().ToLowerInvariant();
		
		// Act
		var result = variant switch
		{
			ButtonVariant.Outline when color != ButtonColor.None => $"btn-outline-{colorName}",
			ButtonVariant.Ghost when color != ButtonColor.None => $"btn-ghost-{colorName}",
			_ when color != ButtonColor.None => $"btn-{colorName}",
			_ => string.Empty
		};
		
		// Assert
		result.Should().Be(expected);
	}
	
	[Theory]
	[InlineData(ButtonSize.Small, "btn-sm")]
	[InlineData(ButtonSize.Medium, null)]
	[InlineData(ButtonSize.Large, "btn-lg")]
	public void GetSizeClass_ShouldReturnCorrectCssClass(ButtonSize size, string? expected)
	{
		// Arrange & Act
		var result = size switch
		{
			ButtonSize.Small => "btn-sm",
			ButtonSize.Large => "btn-lg",
			_ => null
		};
		
		// Assert
		result.Should().Be(expected);
	}
	
	[Fact]
	public void EnumValues_ShouldHaveUniqueIntegerValues()
	{
		// ButtonColor enum values should be unique
		var buttonColors = Enum.GetValues<ButtonColor>().Cast<int>().ToList();
		buttonColors.Should().OnlyHaveUniqueItems();
		
		// ButtonSize enum values should be unique
		var buttonSizes = Enum.GetValues<ButtonSize>().Cast<int>().ToList();
		buttonSizes.Should().OnlyHaveUniqueItems();
		
		// ButtonVariant enum values should be unique
		var buttonVariants = Enum.GetValues<ButtonVariant>().Cast<int>().ToList();
		buttonVariants.Should().OnlyHaveUniqueItems();
	}
	
	[Fact]
	public void ButtonColor_ToString_ShouldReturnEnumName()
	{
		// Arrange & Act & Assert
		ButtonColor.Primary.ToString().Should().Be("Primary");
		ButtonColor.Secondary.ToString().Should().Be("Secondary");
		ButtonColor.Success.ToString().Should().Be("Success");
		ButtonColor.Warning.ToString().Should().Be("Warning");
		ButtonColor.Danger.ToString().Should().Be("Danger");
	}
}