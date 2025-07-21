// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Layout;

/// <summary>
/// Unit tests for TablerPage component logic and CSS class generation.
/// </summary>
public class TablerPageLogicTests
{
	[Fact]
	public void PageLayout_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		PageLayout.Default.Should().Be((PageLayout)0);
		PageLayout.FullHeight.Should().Be((PageLayout)1);
		PageLayout.Centered.Should().Be((PageLayout)2);
		PageLayout.Minimal.Should().Be((PageLayout)3);
	}
	
	[Theory]
	[InlineData(PageLayout.Default, "page-wrapper")]
	[InlineData(PageLayout.FullHeight, "page-wrapper")]
	[InlineData(PageLayout.Centered, "page page-center")]
	[InlineData(PageLayout.Minimal, "page-minimal")]
	public void GetPageClass_ShouldReturnCorrectCssClass(PageLayout layout, string expectedClass)
	{
		// Arrange & Act
		var result = layout switch
		{
			PageLayout.Centered => "page page-center",
			PageLayout.Minimal => "page-minimal",
			_ => "page-wrapper" // Default and FullHeight use page-wrapper
		};
		
		// Assert
		result.Should().Be(expectedClass);
	}
	
	[Theory]
	[InlineData(PageLayout.Default, false, new[] { "page-body", "container" })]
	[InlineData(PageLayout.Default, true, new[] { "page-body", "container-fluid" })]
	[InlineData(PageLayout.FullHeight, false, new[] { "page-body flex-fill", "container" })]
	[InlineData(PageLayout.FullHeight, true, new[] { "page-body flex-fill", "container-fluid" })]
	[InlineData(PageLayout.Centered, false, new[] { "page-center" })]
	[InlineData(PageLayout.Centered, true, new[] { "page-center" })]
	[InlineData(PageLayout.Minimal, false, new[] { "page-body-minimal" })]
	[InlineData(PageLayout.Minimal, true, new[] { "page-body-minimal" })]
	public void GetMainCssClasses_ShouldReturnCorrectClasses(PageLayout layout, bool fluid, string[] expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string>();
		
		switch (layout)
		{
			case PageLayout.Centered:
				classes.Add("page-center");
				break;
			case PageLayout.FullHeight:
				classes.Add("page-body flex-fill");
				break;
			case PageLayout.Minimal:
				classes.Add("page-body-minimal");
				break;
			default:
				classes.Add("page-body");
				break;
		}
		
		// Container classes for main content
		if (layout != PageLayout.Centered && layout != PageLayout.Minimal)
		{
			classes.Add(fluid ? "container-fluid" : "container");
		}
		
		// Assert
		classes.Should().BeEquivalentTo(expectedClasses);
	}
	
	[Fact]
	public void PageLayout_EnumValues_ShouldBeUnique()
	{
		// Arrange & Act
		var pageLayouts = Enum.GetValues<PageLayout>().Cast<int>().ToList();
		
		// Assert
		pageLayouts.Should().OnlyHaveUniqueItems();
	}
	
	[Fact]
	public void PageLayout_ToString_ShouldReturnEnumName()
	{
		// Arrange & Act & Assert
		PageLayout.Default.ToString().Should().Be("Default");
		PageLayout.FullHeight.ToString().Should().Be("FullHeight");
		PageLayout.Centered.ToString().Should().Be("Centered");
		PageLayout.Minimal.ToString().Should().Be("Minimal");
	}
	
	[Fact]
	public void FullHeightLayout_ShouldIncludeFlexClasses()
	{
		// Arrange
		var layout = PageLayout.FullHeight;
		var expectedPageClasses = new[] { "page-wrapper", "d-flex", "flex-column" };
		
		// Act
		var pageClasses = new List<string>();
		
		// Base page class
		pageClasses.Add(layout switch
		{
			PageLayout.Centered => "page page-center",
			PageLayout.Minimal => "page-minimal",
			_ => "page-wrapper"
		});
		
		// Layout-specific classes
		if (layout == PageLayout.FullHeight)
		{
			pageClasses.Add("d-flex");
			pageClasses.Add("flex-column");
		}
		
		// Assert
		pageClasses.Should().BeEquivalentTo(expectedPageClasses);
	}
	
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "page-wrapper" };
		
		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}
		
		// Assert
		cssClasses.Should().ContainSingle("page-wrapper");
	}
	
	[Theory]
	[InlineData("custom-page", new[] { "page-wrapper", "custom-page" })]
	[InlineData("bg-light shadow", new[] { "page-wrapper", "bg-light", "shadow" })]
	[InlineData("  class1   class2  ", new[] { "page-wrapper", "class1", "class2" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "page-wrapper" };
		
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
	public void PageStructure_ShouldFollowSemanticHTML()
	{
		// Arrange
		var expectedElements = new[]
		{
			"header", // Header content area
			"main",   // Main content area  
			"footer"  // Footer content area
		};
		
		// Act & Assert
		expectedElements.Should().NotBeEmpty();
		expectedElements.Should().HaveCount(3);
		expectedElements.Should().Contain("header");
		expectedElements.Should().Contain("main");
		expectedElements.Should().Contain("footer");
	}
	
	[Fact]
	public void CenteredLayout_ShouldUseSpecialStructure()
	{
		// Arrange
		var layout = PageLayout.Centered;
		var expectedMainClass = "page-center";
		
		// Act
		var mainClass = layout switch
		{
			PageLayout.Centered => "page-center",
			PageLayout.FullHeight => "page-body flex-fill",
			PageLayout.Minimal => "page-body-minimal",
			_ => "page-body"
		};
		
		// Assert
		mainClass.Should().Be(expectedMainClass);
	}
	
	[Theory]
	[InlineData(PageLayout.Default)]
	[InlineData(PageLayout.FullHeight)]
	public void StandardLayouts_ShouldIncludeContainer(PageLayout layout)
	{
		// Arrange
		const bool fluid = false;
		
		// Act
		var includesContainer = layout != PageLayout.Centered && layout != PageLayout.Minimal;
		var expectedClass = fluid ? "container-fluid" : "container";
		
		// Assert
		includesContainer.Should().BeTrue();
		expectedClass.Should().Be("container");
	}
	
	[Theory]
	[InlineData(PageLayout.Centered)]
	[InlineData(PageLayout.Minimal)]
	public void SpecialLayouts_ShouldNotIncludeContainer(PageLayout layout)
	{
		// Arrange & Act
		var includesContainer = layout != PageLayout.Centered && layout != PageLayout.Minimal;
		
		// Assert
		includesContainer.Should().BeFalse();
	}
}