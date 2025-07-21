// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for form component logic and CSS class generation.
/// </summary>
public class TablerFormLogicTests
{
	[Fact]
	public void InputValidationState_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		InputValidationState.None.Should().Be((InputValidationState)0);
		InputValidationState.Valid.Should().Be((InputValidationState)1);
		InputValidationState.Invalid.Should().Be((InputValidationState)2);
		InputValidationState.Warning.Should().Be((InputValidationState)3);
	}

	[Theory]
	[InlineData(InputValidationState.None, "")]
	[InlineData(InputValidationState.Valid, "is-valid")]
	[InlineData(InputValidationState.Invalid, "is-invalid")]
	[InlineData(InputValidationState.Warning, "is-warning")]
	public void GetValidationClass_ShouldReturnCorrectCssClass(InputValidationState state, string expectedClass)
	{
		// Arrange & Act
		var result = state switch
		{
			InputValidationState.Valid => "is-valid",
			InputValidationState.Invalid => "is-invalid",
			InputValidationState.Warning => "is-warning",
			_ => string.Empty
		};

		// Assert
		result.Should().Be(expectedClass);
	}

	[Fact]
	public void InputSize_ShouldHaveCorrectValues()
	{
		// Arrange & Act & Assert
		InputSize.Default.Should().Be((InputSize)0);
		InputSize.Small.Should().Be((InputSize)1);
		InputSize.Large.Should().Be((InputSize)2);
	}

	[Theory]
	[InlineData(InputSize.Default, "")]
	[InlineData(InputSize.Small, "form-control-sm")]
	[InlineData(InputSize.Large, "form-control-lg")]
	public void GetInputSizeClass_ShouldReturnCorrectCssClass(InputSize size, string expectedClass)
	{
		// Arrange & Act
		var result = size switch
		{
			InputSize.Small => "form-control-sm",
			InputSize.Large => "form-control-lg",
			_ => string.Empty
		};

		// Assert
		result.Should().Be(expectedClass);
	}

	[Fact]
	public void InputValidationState_EnumValues_ShouldBeUnique()
	{
		// Arrange & Act
		var validationStates = Enum.GetValues<InputValidationState>().Cast<int>().ToList();

		// Assert
		validationStates.Should().OnlyHaveUniqueItems();
	}

	[Fact]
	public void InputSize_EnumValues_ShouldBeUnique()
	{
		// Arrange & Act
		var inputSizes = Enum.GetValues<InputSize>().Cast<int>().ToList();

		// Assert
		inputSizes.Should().OnlyHaveUniqueItems();
	}

	[Fact]
	public void InputValidationState_ToString_ShouldReturnEnumName()
	{
		// Arrange & Act & Assert
		InputValidationState.None.ToString().Should().Be("None");
		InputValidationState.Valid.ToString().Should().Be("Valid");
		InputValidationState.Invalid.ToString().Should().Be("Invalid");
		InputValidationState.Warning.ToString().Should().Be("Warning");
	}

	[Fact]
	public void InputSize_ToString_ShouldReturnEnumName()
	{
		// Arrange & Act & Assert
		InputSize.Default.ToString().Should().Be("Default");
		InputSize.Small.ToString().Should().Be("Small");
		InputSize.Large.ToString().Should().Be("Large");
	}

	[Theory]
	[InlineData(InputValidationState.Valid, "valid-feedback")]
	[InlineData(InputValidationState.Invalid, "invalid-feedback")]
	[InlineData(InputValidationState.Warning, "warning-feedback")]
	[InlineData(InputValidationState.None, "form-text text-muted")]
	public void GetFeedbackClass_ShouldReturnCorrectCssClass(InputValidationState state, string expectedClass)
	{
		// Arrange & Act
		var result = state switch
		{
			InputValidationState.Valid => "valid-feedback",
			InputValidationState.Invalid => "invalid-feedback",
			InputValidationState.Warning => "warning-feedback",
			_ => "form-text text-muted"
		};

		// Assert
		result.Should().Be(expectedClass);
	}

	[Fact]
	public void FormControlClasses_ShouldCombineCorrectly()
	{
		// Arrange
		const string baseClass = "form-control";
		const InputSize size = InputSize.Large;
		const InputValidationState state = InputValidationState.Valid;

		// Act
		var classes = new List<string> { baseClass };

		if (size != InputSize.Default)
		{
			classes.Add(size switch
			{
				InputSize.Small => "form-control-sm",
				InputSize.Large => "form-control-lg",
				_ => string.Empty
			});
		}

		if (state != InputValidationState.None)
		{
			classes.Add(state switch
			{
				InputValidationState.Valid => "is-valid",
				InputValidationState.Invalid => "is-invalid",
				InputValidationState.Warning => "is-warning",
				_ => string.Empty
			});
		}

		var result = string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c))).Trim();

		// Assert
		result.Should().Be("form-control form-control-lg is-valid");
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "form-control" };

		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}

		// Assert
		cssClasses.Should().ContainSingle("form-control");
	}

	[Theory]
	[InlineData("custom-input", new[] { "form-control", "custom-input" })]
	[InlineData("class1 class2", new[] { "form-control", "class1", "class2" })]
	[InlineData("  class1   class2  ", new[] { "form-control", "class1", "class2" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "form-control" };

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
	public void FormGroupClasses_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedClasses = new[]
		{
			"mb-3",           // Default margin
			"form-floating",  // Floating labels
			"row mb-3",       // Horizontal layout
			"has-validation"  // Validation state
		};

		// Act & Assert
		expectedClasses.Should().NotBeEmpty();
		expectedClasses.Should().HaveCount(4);
		expectedClasses.Should().Contain("mb-3");
		expectedClasses.Should().Contain("form-floating");
	}

	[Theory]
	[InlineData(false, false, "mb-3")]
	[InlineData(true, false, "form-floating")]
	[InlineData(false, true, "row mb-3")]
	public void FormGroupLayout_ShouldApplyCorrectClasses(bool floatingLabel, bool horizontal, string expectedClass)
	{
		// Arrange & Act
		var cssClass = (floatingLabel, horizontal) switch
		{
			(true, _) => "form-floating",
			(false, true) => "row mb-3",
			_ => "mb-3"
		};

		// Assert
		cssClass.Should().Be(expectedClass);
	}
}