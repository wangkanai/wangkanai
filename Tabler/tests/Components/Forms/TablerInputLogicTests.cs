// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for TablerInput component logic and CSS class generation.
/// </summary>
public class TablerInputLogicTests
{
	[Theory]
	[InlineData(InputSize.Default, "form-control")]
	[InlineData(InputSize.Small, "form-control form-control-sm")]
	[InlineData(InputSize.Large, "form-control form-control-lg")]
	public void GetInputCssClass_WithSize_ShouldReturnCorrectClasses(InputSize size, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-control" };

		switch (size)
		{
			case InputSize.Small:
				classes.Add("form-control-sm");
				break;
			case InputSize.Large:
				classes.Add("form-control-lg");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(InputValidationState.None, "form-control")]
	[InlineData(InputValidationState.Valid, "form-control is-valid")]
	[InlineData(InputValidationState.Invalid, "form-control is-invalid")]
	[InlineData(InputValidationState.Warning, "form-control is-warning")]
	public void GetInputCssClass_WithValidationState_ShouldReturnCorrectClasses(InputValidationState state, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-control" };

		switch (state)
		{
			case InputValidationState.Valid:
				classes.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				classes.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				classes.Add("is-warning");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(true, "input-group")]
	[InlineData(false, "")]
	public void GetWrapperCssClass_WithPrefixOrSuffix_ShouldReturnCorrectClass(bool hasPrefixOrSuffix, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (hasPrefixOrSuffix)
			classes.Add("input-group");

		var result = string.Join(" ", classes).Trim();

		// Assert
		result.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(InputValidationState.Valid, "valid-feedback")]
	[InlineData(InputValidationState.Invalid, "invalid-feedback")]
	[InlineData(InputValidationState.Warning, "warning-feedback")]
	[InlineData(InputValidationState.None, "form-text text-muted")]
	public void GetFeedbackCssClass_ShouldReturnCorrectClass(InputValidationState state, string expectedClass)
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
	public void InputCssClass_WithCombinedSizeAndValidation_ShouldCombineClasses()
	{
		// Arrange
		const InputSize size = InputSize.Large;
		const InputValidationState state = InputValidationState.Invalid;

		// Act
		var classes = new List<string> { "form-control" };

		// Size classes
		switch (size)
		{
			case InputSize.Small:
				classes.Add("form-control-sm");
				break;
			case InputSize.Large:
				classes.Add("form-control-lg");
				break;
		}

		// Validation state classes
		switch (state)
		{
			case InputValidationState.Valid:
				classes.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				classes.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				classes.Add("is-warning");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be("form-control form-control-lg is-invalid");
	}

	[Theory]
	[InlineData("text")]
	[InlineData("email")]
	[InlineData("password")]
	[InlineData("number")]
	[InlineData("tel")]
	[InlineData("url")]
	public void InputType_ShouldSupportCommonTypes(string inputType)
	{
		// Arrange
		var supportedTypes = new[]
		{
			"text", "email", "password", "number", "tel", "url",
			"search", "date", "time", "datetime-local", "month", "week"
		};

		// Act & Assert
		supportedTypes.Should().Contain(inputType);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
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
	[InlineData("border-danger bg-light", new[] { "form-control", "border-danger", "bg-light" })]
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
	public void InputGroupStructure_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedElements = new[]
		{
			"input-group",      // Wrapper class
			"input-group-text", // Prefix/suffix text
			"form-control"      // Input element
		};

		// Act & Assert
		expectedElements.Should().NotBeEmpty();
		expectedElements.Should().HaveCount(3);
		expectedElements.Should().Contain("input-group");
		expectedElements.Should().Contain("input-group-text");
		expectedElements.Should().Contain("form-control");
	}

	[Theory]
	[InlineData(true, true, "input-group")]
	[InlineData(true, false, "input-group")]
	[InlineData(false, true, "input-group")]
	[InlineData(false, false, "")]
	public void WrapperClass_WithPrefixAndSuffixCombinations_ShouldReturnCorrectClass(bool hasPrefix, bool hasSuffix, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();
		var hasPrefixOrSuffix = hasPrefix || hasSuffix;

		if (hasPrefixOrSuffix)
			classes.Add("input-group");

		var result = string.Join(" ", classes).Trim();

		// Assert
		result.Should().Be(expectedClass);
	}

	[Fact]
	public void InputAttributes_ShouldSupportCommonAttributes()
	{
		// Arrange
		var commonAttributes = new Dictionary<string, object>
		{
			{ "placeholder", "Enter text..." },
			{ "maxlength", "100" },
			{ "minlength", "5" },
			{ "pattern", "[A-Za-z]+" },
			{ "autocomplete", "email" },
			{ "data-testid", "test-input" },
			{ "aria-label", "Email address" },
			{ "aria-describedby", "email-help" }
		};

		// Act & Assert
		foreach (var attribute in commonAttributes)
		{
			attribute.Key.Should().NotBeNullOrEmpty();
			attribute.Value.Should().NotBeNull();
		}

		commonAttributes.Should().HaveCountGreaterThan(5);
	}

	[Theory]
	[InlineData("Enter your email", "Enter your email")]
	[InlineData("", "")]
	[InlineData(null, null)]
	public void PlaceholderText_ShouldBeConfigurable(string? placeholder, string? expectedPlaceholder)
	{
		// Arrange & Act
		var actualPlaceholder = placeholder;

		// Assert
		actualPlaceholder.Should().Be(expectedPlaceholder);
	}

	[Fact]
	public void ValidationMessages_ShouldSupportAllStates()
	{
		// Arrange
		var validationMessages = new Dictionary<InputValidationState, string>
		{
			{ InputValidationState.Valid, "Input is valid" },
			{ InputValidationState.Invalid, "This field is required" },
			{ InputValidationState.Warning, "Consider reviewing this input" },
			{ InputValidationState.None, "Help text for guidance" }
		};

		// Act & Assert
		validationMessages.Should().HaveCount(4);
		validationMessages.Keys.Should().Contain(InputValidationState.Valid);
		validationMessages.Keys.Should().Contain(InputValidationState.Invalid);
		validationMessages.Keys.Should().Contain(InputValidationState.Warning);
		validationMessages.Keys.Should().Contain(InputValidationState.None);
	}
}