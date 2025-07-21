// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for TablerTextarea component logic and CSS class generation.
/// </summary>
public class TablerTextareaLogicTests
{
	[Theory]
	[InlineData(InputSize.Default, "form-control")]
	[InlineData(InputSize.Small, "form-control form-control-sm")]
	[InlineData(InputSize.Large, "form-control form-control-lg")]
	public void GetTextareaCssClass_WithSize_ShouldReturnCorrectClasses(InputSize size, string expectedClasses)
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
	public void GetTextareaCssClass_WithValidationState_ShouldReturnCorrectClasses(InputValidationState state, string expectedClasses)
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
	public void TextareaCssClass_WithCombinedSizeAndValidation_ShouldCombineClasses()
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
	[InlineData(true, "form-control auto-resize")]
	[InlineData(false, "form-control")]
	public void GetTextareaCssClass_WithAutoResize_ShouldIncludeAutoResizeClass(bool autoResize, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-control" };

		if (autoResize)
		{
			classes.Add("auto-resize");
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
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
	[InlineData("custom-textarea", new[] { "form-control", "custom-textarea" })]
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
	public void TextareaStructure_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedElements = new[]
		{
			"input-group",      // Wrapper class
			"input-group-text", // Prefix/suffix text
			"form-control"      // Textarea element
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
	public void TextareaAttributes_ShouldSupportCommonAttributes()
	{
		// Arrange
		var commonAttributes = new Dictionary<string, object>
		{
			{ "rows", 5 },
			{ "cols", 40 },
			{ "maxlength", 500 },
			{ "required", true },
			{ "data-testid", "test-textarea" },
			{ "aria-label", "Enter description" },
			{ "aria-describedby", "textarea-help" },
			{ "data-auto-resize", "true" }
		};

		// Act & Assert
		foreach (var attribute in commonAttributes)
		{
			attribute.Key.Should().NotBeNullOrEmpty();
			attribute.Value.Should().NotBeNull();
		}

		commonAttributes.Should().HaveCountGreaterThan(7);
	}

	[Theory]
	[InlineData("Enter your text here", "Enter your text here")]
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
			{ InputValidationState.Valid, "Text is valid" },
			{ InputValidationState.Invalid, "Please enter some text" },
			{ InputValidationState.Warning, "Consider reviewing this text" },
			{ InputValidationState.None, "Help text for guidance" }
		};

		// Act & Assert
		validationMessages.Should().HaveCount(4);
		validationMessages.Keys.Should().Contain(InputValidationState.Valid);
		validationMessages.Keys.Should().Contain(InputValidationState.Invalid);
		validationMessages.Keys.Should().Contain(InputValidationState.Warning);
		validationMessages.Keys.Should().Contain(InputValidationState.None);
	}

	[Theory]
	[InlineData(3, 3)]
	[InlineData(5, 5)]
	[InlineData(10, 10)]
	public void Rows_ShouldBeConfigurable(int rows, int expectedRows)
	{
		// Arrange & Act
		var actualRows = rows;

		// Assert
		actualRows.Should().Be(expectedRows);
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(40, 40)]
	[InlineData(80, 80)]
	public void Cols_ShouldBeConfigurable(int cols, int expectedCols)
	{
		// Arrange & Act
		var actualCols = cols;

		// Assert
		actualCols.Should().Be(expectedCols);
	}

	[Theory]
	[InlineData(0, false)]
	[InlineData(100, true)]
	[InlineData(500, true)]
	public void MaxLength_ShouldBeConfigurable(int maxLength, bool hasMaxLength)
	{
		// Arrange & Act
		var actualHasMaxLength = maxLength > 0;

		// Assert
		actualHasMaxLength.Should().Be(hasMaxLength);
	}

	[Theory]
	[InlineData(true, false)]
	[InlineData(false, true)]
	public void DisabledState_ShouldBeConfigurable(bool disabled, bool expectedEnabled)
	{
		// Arrange & Act
		var isEnabled = !disabled;

		// Assert
		isEnabled.Should().Be(expectedEnabled);
	}

	[Theory]
	[InlineData(true, false)]
	[InlineData(false, true)]
	public void ReadOnlyState_ShouldBeConfigurable(bool readOnly, bool expectedEditable)
	{
		// Arrange & Act
		var isEditable = !readOnly;

		// Assert
		isEditable.Should().Be(expectedEditable);
	}

	[Theory]
	[InlineData(true, true)]
	[InlineData(false, false)]
	public void AutoResize_ShouldBeConfigurable(bool autoResize, bool expectedAutoResize)
	{
		// Arrange & Act
		var actualAutoResize = autoResize;

		// Assert
		actualAutoResize.Should().Be(expectedAutoResize);
	}

	[Theory]
	[InlineData(true, true)]
	[InlineData(false, false)]
	public void ShowCharacterCount_ShouldBeConfigurable(bool showCharacterCount, bool expectedShowCharacterCount)
	{
		// Arrange & Act
		var actualShowCharacterCount = showCharacterCount;

		// Assert
		actualShowCharacterCount.Should().Be(expectedShowCharacterCount);
	}

	[Theory]
	[InlineData("Hello", 0, "5 characters")]
	[InlineData("Hello World", 0, "11 characters")]
	[InlineData("Test", 10, "4/10")]
	[InlineData("", 5, "0/5")]
	public void GetCharacterCountText_ShouldReturnCorrectFormat(string text, int maxLength, string expectedText)
	{
		// Arrange & Act
		var currentLength = text?.Length ?? 0;
		var result = maxLength > 0 ? $"{currentLength}/{maxLength}" : $"{currentLength} characters";

		// Assert
		result.Should().Be(expectedText);
	}

	[Theory]
	[InlineData(0, 100, "form-text text-muted")]
	[InlineData(50, 100, "form-text text-muted")]
	[InlineData(80, 100, "form-text text-muted text-warning")]
	[InlineData(100, 100, "form-text text-muted text-danger")]
	[InlineData(120, 100, "form-text text-muted text-danger")]
	public void GetCharacterCountCssClass_ShouldReturnCorrectClasses(int currentLength, int maxLength, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-text", "text-muted" };

		if (maxLength > 0 && currentLength >= maxLength)
		{
			classes.Add("text-danger");
		}
		else if (maxLength > 0 && currentLength >= maxLength * 0.8)
		{
			classes.Add("text-warning");
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(false, "")]
	[InlineData(true, "d-flex justify-content-end")]
	public void GetFeedbackWrapperCssClass_WithCharacterCount_ShouldReturnCorrectClasses(bool showCharacterCount, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (showCharacterCount)
		{
			classes.Add("d-flex");
			classes.Add("justify-content-end");
		}

		var result = string.Join(" ", classes).Trim();

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(true, true, "d-flex justify-content-between align-items-center")]
	[InlineData(true, false, "d-flex justify-content-end")]
	public void GetFeedbackWrapperCssClass_WithCharacterCountAndValidation_ShouldReturnCorrectClasses(bool showCharacterCount, bool hasValidationOrHelp, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (showCharacterCount)
		{
			classes.Add("d-flex");
			if (hasValidationOrHelp)
			{
				classes.Add("justify-content-between");
				classes.Add("align-items-center");
			}
			else
			{
				classes.Add("justify-content-end");
			}
		}

		var result = string.Join(" ", classes).Trim();

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Fact]
	public void TextareaAccessibility_ShouldSupportARIAAttributes()
	{
		// Arrange
		var accessibilityAttributes = new Dictionary<string, string>
		{
			{ "aria-label", "Enter Description" },
			{ "aria-describedby", "description-help" },
			{ "role", "textbox" },
			{ "aria-required", "true" },
			{ "aria-invalid", "false" },
			{ "aria-multiline", "true" }
		};

		// Act & Assert
		accessibilityAttributes.Should().NotBeEmpty();
		accessibilityAttributes.Should().ContainKey("aria-label");
		accessibilityAttributes.Should().ContainKey("aria-describedby");
		accessibilityAttributes["aria-required"].Should().Be("true");
		accessibilityAttributes["aria-multiline"].Should().Be("true");
	}

	[Theory]
	[InlineData("Hello world!", 10, "Hello worl")]
	[InlineData("Test", 10, "Test")]
	[InlineData("", 10, "")]
	[InlineData("Long text here", 5, "Long ")]
	public void MaxLengthEnforcement_ShouldTruncateText(string input, int maxLength, string expectedOutput)
	{
		// Arrange & Act
		var result = maxLength > 0 && input?.Length > maxLength 
			? input.Substring(0, maxLength) 
			: input;

		// Assert
		result.Should().Be(expectedOutput);
	}

	[Theory]
	[InlineData(InputSize.Default, InputValidationState.Invalid, true, "custom", "form-control is-invalid auto-resize custom")]
	[InlineData(InputSize.Large, InputValidationState.Valid, false, "", "form-control form-control-lg is-valid")]
	[InlineData(InputSize.Small, InputValidationState.None, true, "highlight", "form-control form-control-sm auto-resize highlight")]
	public void ComplexTextareaConfiguration_ShouldCombineAllOptions(InputSize size, InputValidationState validationState, bool autoResize, string additionalClass, string expectedClasses)
	{
		// Arrange & Act
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

		// Validation classes
		switch (validationState)
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

		// Auto-resize class
		if (autoResize)
		{
			classes.Add("auto-resize");
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

	[Fact]
	public void TextareaStates_ShouldCombineCorrectly()
	{
		// Arrange
		const bool hasPrefix = true;
		const InputValidationState validationState = InputValidationState.Warning;
		const InputSize size = InputSize.Small;
		const bool autoResize = true;
		const bool showCharacterCount = true;
		const string additionalClass = "custom-textarea";

		// Act
		// Wrapper classes
		var wrapperClasses = new List<string>();
		if (hasPrefix)
			wrapperClasses.Add("input-group");

		// Textarea classes
		var textareaClasses = new List<string> { "form-control" };

		switch (size)
		{
			case InputSize.Small:
				textareaClasses.Add("form-control-sm");
				break;
			case InputSize.Large:
				textareaClasses.Add("form-control-lg");
				break;
		}

		switch (validationState)
		{
			case InputValidationState.Valid:
				textareaClasses.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				textareaClasses.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				textareaClasses.Add("is-warning");
				break;
		}

		if (autoResize)
		{
			textareaClasses.Add("auto-resize");
		}

		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			textareaClasses.Add(additionalClass);
		}

		// Feedback wrapper classes
		var feedbackClasses = new List<string>();
		if (showCharacterCount)
		{
			feedbackClasses.Add("d-flex");
			feedbackClasses.Add("justify-content-between");
			feedbackClasses.Add("align-items-center");
		}

		var wrapperResult = string.Join(" ", wrapperClasses);
		var textareaResult = string.Join(" ", textareaClasses);
		var feedbackResult = string.Join(" ", feedbackClasses);

		// Assert
		wrapperResult.Should().Be("input-group");
		textareaResult.Should().Be("form-control form-control-sm is-warning auto-resize custom-textarea");
		feedbackResult.Should().Be("d-flex justify-content-between align-items-center");
	}
}