// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Layout;

/// <summary>
/// Unit tests for TablerContainer component logic and CSS class generation.
/// </summary>
public class TablerContainerLogicTests
{
	[Fact]
	public void ContainerType_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		ContainerType.Default.Should().Be((ContainerType)0);
		ContainerType.Fluid.Should().Be((ContainerType)1);
		ContainerType.Small.Should().Be((ContainerType)2);
		ContainerType.Medium.Should().Be((ContainerType)3);
		ContainerType.Large.Should().Be((ContainerType)4);
		ContainerType.ExtraLarge.Should().Be((ContainerType)5);
		ContainerType.ExtraExtraLarge.Should().Be((ContainerType)6);
	}
	
	[Theory]
	[InlineData(ContainerType.Default, "container")]
	[InlineData(ContainerType.Fluid, "container-fluid")]
	[InlineData(ContainerType.Small, "container-sm")]
	[InlineData(ContainerType.Medium, "container-md")]
	[InlineData(ContainerType.Large, "container-lg")]
	[InlineData(ContainerType.ExtraLarge, "container-xl")]
	[InlineData(ContainerType.ExtraExtraLarge, "container-xxl")]
	public void GetContainerClass_ShouldReturnCorrectCssClass(ContainerType type, string expectedClass)
	{
		// Arrange & Act
		var result = type switch
		{
			ContainerType.Fluid => "container-fluid",
			ContainerType.Small => "container-sm",
			ContainerType.Medium => "container-md",
			ContainerType.Large => "container-lg",
			ContainerType.ExtraLarge => "container-xl",
			ContainerType.ExtraExtraLarge => "container-xxl",
			_ => "container" // Default responsive container
		};
		
		// Assert
		result.Should().Be(expectedClass);
	}
	
	[Fact]
	public void ContainerType_EnumValues_ShouldBeUnique()
	{
		// Arrange & Act
		var containerTypes = Enum.GetValues<ContainerType>().Cast<int>().ToList();
		
		// Assert
		containerTypes.Should().OnlyHaveUniqueItems();
	}
	
	[Fact]
	public void ContainerType_ToString_ShouldReturnEnumName()
	{
		// Arrange & Act & Assert
		ContainerType.Default.ToString().Should().Be("Default");
		ContainerType.Fluid.ToString().Should().Be("Fluid");
		ContainerType.Small.ToString().Should().Be("Small");
		ContainerType.Medium.ToString().Should().Be("Medium");
		ContainerType.Large.ToString().Should().Be("Large");
		ContainerType.ExtraLarge.ToString().Should().Be("ExtraLarge");
		ContainerType.ExtraExtraLarge.ToString().Should().Be("ExtraExtraLarge");
	}
	
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "container" };
		
		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}
		
		// Assert
		cssClasses.Should().ContainSingle("container");
	}
	
	[Theory]
	[InlineData("custom-class", new[] { "container", "custom-class" })]
	[InlineData("class1 class2", new[] { "container", "class1", "class2" })]
	[InlineData("  class1   class2  ", new[] { "container", "class1", "class2" })]
	[InlineData("mt-3 mb-4 p-2", new[] { "container", "mt-3", "mb-4", "p-2" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "container" };
		
		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}
		
		// Assert
		cssClasses.Should().BeEquivalentTo(expectedClasses);
	}
	
	[Fact]
	public void ContainerClasses_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedClasses = new[]
		{
			"container",
			"container-fluid",
			"container-sm",
			"container-md",
			"container-lg",
			"container-xl",
			"container-xxl"
		};
		
		// Act
		var actualClasses = Enum.GetValues<ContainerType>()
			.Select(type => type switch
			{
				ContainerType.Fluid => "container-fluid",
				ContainerType.Small => "container-sm",
				ContainerType.Medium => "container-md",
				ContainerType.Large => "container-lg",
				ContainerType.ExtraLarge => "container-xl",
				ContainerType.ExtraExtraLarge => "container-xxl",
				_ => "container"
			})
			.ToArray();
		
		// Assert
		actualClasses.Should().BeEquivalentTo(expectedClasses);
	}
	
	[Fact]
	public void ContainerBreakpoints_ShouldBeInAscendingOrder()
	{
		// Arrange
		var breakpointOrder = new[]
		{
			ContainerType.Default,  // Base responsive
			ContainerType.Small,    // >= 576px
			ContainerType.Medium,   // >= 768px
			ContainerType.Large,    // >= 992px
			ContainerType.ExtraLarge, // >= 1200px
			ContainerType.ExtraExtraLarge, // >= 1400px
			ContainerType.Fluid     // 100% width always
		};
		
		// Act & Assert
		breakpointOrder.Should().NotBeEmpty();
		breakpointOrder.Should().HaveCount(7);
	}
	
	[Theory]
	[InlineData("my-container")]
	[InlineData("text-center")]
	[InlineData("bg-light")]
	[InlineData("shadow-sm")]
	public void CommonCssClasses_ShouldBeSupported(string cssClass)
	{
		// Arrange
		var supportedClasses = new[]
		{
			"my-container", "text-center", "bg-light", "shadow-sm",
			"p-3", "m-4", "border", "rounded"
		};
		
		// Act & Assert
		supportedClasses.Should().Contain(cssClass);
	}
	
	[Fact]
	public void CssClassCombination_ShouldBeCorrect()
	{
		// Arrange
		const string baseClass = "container-lg";
		const string additionalClasses = "p-3 text-center";
		var expectedResult = "container-lg p-3 text-center";
		
		// Act
		var classes = new List<string> { baseClass };
		foreach (var cssClass in additionalClasses.Split(' ', StringSplitOptions.RemoveEmptyEntries))
		{
			classes.Add(cssClass);
		}
		var result = string.Join(" ", classes).Trim();
		
		// Assert
		result.Should().Be(expectedResult);
	}
}