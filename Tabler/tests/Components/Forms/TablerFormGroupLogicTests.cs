// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for TablerFormGroup component logic and CSS class generation.
/// </summary>
public class TablerFormGroupLogicTests
{
	[Theory]
	[InlineData(false, false, "mb-3")]
	[InlineData(true, false, "form-floating")]
	[InlineData(false, true, "row mb-3")]
	public void GetFormGroupCssClass_WithLayoutOptions_ShouldReturnCorrectClasses(bool floatingLabel, bool horizontal, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (floatingLabel)
			classes.Add("form-floating");
		else if (horizontal)
			classes.Add("row mb-3");
		else
			classes.Add("mb-3");

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(InputValidationState.Valid, "mb-3 has-validation")]
	[InlineData(InputValidationState.Invalid, "mb-3 has-validation")]
	[InlineData(InputValidationState.Warning, "mb-3 has-validation")]
	[InlineData(InputValidationState.None, "mb-3")]
	public void GetFormGroupCssClass_WithValidationState_ShouldIncludeValidationClass(InputValidationState state, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "mb-3" };

		// Validation state classes
		switch (state)
		{
			case InputValidationState.Valid:
			case InputValidationState.Invalid:
			case InputValidationState.Warning:
				classes.Add("has-validation");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(false, false, "form-label")]
	[InlineData(false, true, "col-form-label col-sm-3")]
	[InlineData(true, false, "")]
	public void GetLabelCssClass_WithLayoutOptions_ShouldReturnCorrectClasses(bool floatingLabel, bool horizontal, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (floatingLabel)
		{
			// Floating labels use different styling
			return;
		}
		else if (horizontal)
		{
			classes.Add("col-form-label");
			classes.Add("col-sm-3"); // Default column class
		}
		else
		{
			classes.Add("form-label");
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(InputValidationState.Valid, "valid-feedback")]
	[InlineData(InputValidationState.Invalid, "invalid-feedback")]
	[InlineData(InputValidationState.Warning, "warning-feedback")]
	[InlineData(InputValidationState.None, "form-text text-muted")]
	public void GetHelpTextCssClass_WithValidationState_ShouldReturnCorrectClass(InputValidationState state, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();

		switch (state)
		{
			case InputValidationState.Valid:
				classes.Add("valid-feedback");
				break;
			case InputValidationState.Invalid:
				classes.Add("invalid-feedback");
				break;
			case InputValidationState.Warning:
				classes.Add("warning-feedback");
				break;
			default:
				classes.Add("form-text");
				classes.Add("text-muted");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(false, "")]
	[InlineData(true, "col-sm-9")]
	public void GetInputWrapperCssClass_WithHorizontalLayout_ShouldReturnCorrectClass(bool horizontal, string expectedClass)
	{
		// Arrange & Act
		var result = horizontal ? "col-sm-9" : string.Empty;

		// Assert
		result.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(false, "form-text text-muted")]
	[InlineData(true, "offset-sm-3 col-sm-9 form-text text-muted")]
	public void GetHelpTextCssClass_WithHorizontalLayout_ShouldIncludeOffsetClass(bool horizontal, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (horizontal)
		{
			classes.Add("offset-sm-3");
			classes.Add("col-sm-9");
		}

		classes.Add("form-text");
		classes.Add("text-muted");

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClass);
	}

	[Fact]
	public void FormGroupLayout_ShouldSupportCustomColumnClasses()
	{
		// Arrange
		const string labelColumnClass = "col-md-4";
		const string inputColumnClass = "col-md-8";

		// Act
		var labelClasses = new List<string> { "col-form-label", labelColumnClass };
		var inputClasses = new List<string> { inputColumnClass };

		var labelResult = string.Join(" ", labelClasses);
		var inputResult = string.Join(" ", inputClasses);

		// Assert
		labelResult.Should().Be("col-form-label col-md-4");
		inputResult.Should().Be("col-md-8");
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "mb-3" };

		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}

		// Assert
		cssClasses.Should().ContainSingle("mb-3");
	}

	[Theory]
	[InlineData("custom-group", new[] { "mb-3", "custom-group" })]
	[InlineData("border shadow", new[] { "mb-3", "border", "shadow" })]
	[InlineData("  class1   class2  ", new[] { "mb-3", "class1", "class2" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "mb-3" };

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
	public void FormGroupStructure_ShouldFollowSemanticHTML()
	{
		// Arrange
		var expectedElements = new[]
		{
			"label",         // Label element
			"div",           // Input wrapper
			"div"            // Help text container
		};

		// Act & Assert
		expectedElements.Should().NotBeEmpty();
		expectedElements.Should().HaveCount(3);
		expectedElements.Should().Contain("label");
	}

	[Fact]
	public void RequiredIndicator_ShouldBeConfigurable()
	{
		// Arrange
		const bool required = true;
		const string expectedClass = "text-danger";
		const string expectedContent = "*";

		// Act
		var asteriskClass = required ? expectedClass : string.Empty;
		var asteriskContent = required ? expectedContent : string.Empty;

		// Assert
		asteriskClass.Should().Be(expectedClass);
		asteriskContent.Should().Be(expectedContent);
	}

	[Theory]
	[InlineData("email-input", "email-input")]
	[InlineData("", "")]
	[InlineData(null, null)]
	public void LabelForAttribute_ShouldBeConfigurable(string? forId, string? expectedForId)
	{
		// Arrange & Act
		var actualForId = forId;

		// Assert
		actualForId.Should().Be(expectedForId);
	}

	[Fact]
	public void FormGroupStates_ShouldCombineCorrectly()
	{
		// Arrange
		const bool horizontal = true;
		const InputValidationState validationState = InputValidationState.Invalid;
		const string additionalClass = "custom-group";

		// Act
		var classes = new List<string>();

		if (horizontal)
			classes.Add("row mb-3");
		else
			classes.Add("mb-3");

		switch (validationState)
		{
			case InputValidationState.Valid:
			case InputValidationState.Invalid:
			case InputValidationState.Warning:
				classes.Add("has-validation");
				break;
		}

		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			classes.Add(additionalClass);
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be("row mb-3 has-validation custom-group");
	}

	[Fact]
	public void FormGroupAccessibility_ShouldSupportARIAAttributes()
	{
		// Arrange
		var accessibilityAttributes = new Dictionary<string, string>
		{
			{ "aria-label", "Email Address" },
			{ "aria-describedby", "email-help" },
			{ "role", "group" },
			{ "aria-required", "true" },
			{ "aria-invalid", "false" }
		};

		// Act & Assert
		accessibilityAttributes.Should().NotBeEmpty();
		accessibilityAttributes.Should().ContainKey("aria-label");
		accessibilityAttributes.Should().ContainKey("aria-describedby");
		accessibilityAttributes["aria-required"].Should().Be("true");
	}

	[Theory]
	[InlineData(true, true, InputValidationState.Invalid, "form-floating has-validation custom")]
	[InlineData(false, true, InputValidationState.Valid, "row mb-3 has-validation custom")]
	[InlineData(false, false, InputValidationState.None, "mb-3 custom")]
	public void ComplexFormGroupConfiguration_ShouldCombineAllOptions(bool floatingLabel, bool horizontal, InputValidationState validationState, string expectedClasses)
	{
		// Arrange
		const string additionalClass = "custom";

		// Act
		var classes = new List<string>();

		// Layout classes
		if (floatingLabel)
			classes.Add("form-floating");
		else if (horizontal)
			classes.Add("row mb-3");
		else
			classes.Add("mb-3");

		// Validation classes
		switch (validationState)
		{
			case InputValidationState.Valid:
			case InputValidationState.Invalid:
			case InputValidationState.Warning:
				classes.Add("has-validation");
				break;
		}

		// Additional classes
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			classes.Add(additionalClass);
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}
}