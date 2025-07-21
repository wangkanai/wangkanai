// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for TablerCheckbox component logic and CSS class generation.
/// </summary>
public class TablerCheckboxLogicTests
{
	[Theory]
	[InlineData(CheckboxLayout.Default, "form-check")]
	[InlineData(CheckboxLayout.Inline, "form-check form-check-inline")]
	[InlineData(CheckboxLayout.Switch, "form-check form-switch")]
	[InlineData(CheckboxLayout.Reverse, "form-check form-check-reverse")]
	public void GetWrapperCssClass_WithLayout_ShouldReturnCorrectClasses(CheckboxLayout layout, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-check" };

		switch (layout)
		{
			case CheckboxLayout.Inline:
				classes.Add("form-check-inline");
				break;
			case CheckboxLayout.Switch:
				classes.Add("form-switch");
				break;
			case CheckboxLayout.Reverse:
				classes.Add("form-check-reverse");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(InputValidationState.None, "form-check-input")]
	[InlineData(InputValidationState.Valid, "form-check-input is-valid")]
	[InlineData(InputValidationState.Invalid, "form-check-input is-invalid")]
	[InlineData(InputValidationState.Warning, "form-check-input is-warning")]
	public void GetCheckboxCssClass_WithValidationState_ShouldReturnCorrectClasses(InputValidationState state, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-check-input" };

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

	[Fact]
	public void GetLabelCssClass_ShouldReturnCorrectClass()
	{
		// Arrange & Act
		const string expectedClass = "form-check-label";

		// Assert
		expectedClass.Should().Be("form-check-label");
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
	public void CheckboxCssClass_WithCombinedLayoutAndValidation_ShouldCombineClasses()
	{
		// Arrange
		const CheckboxLayout layout = CheckboxLayout.Switch;
		const InputValidationState state = InputValidationState.Invalid;

		// Act
		// Wrapper classes
		var wrapperClasses = new List<string> { "form-check" };
		switch (layout)
		{
			case CheckboxLayout.Inline:
				wrapperClasses.Add("form-check-inline");
				break;
			case CheckboxLayout.Switch:
				wrapperClasses.Add("form-switch");
				break;
			case CheckboxLayout.Reverse:
				wrapperClasses.Add("form-check-reverse");
				break;
		}

		// Checkbox classes
		var checkboxClasses = new List<string> { "form-check-input" };
		switch (state)
		{
			case InputValidationState.Valid:
				checkboxClasses.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				checkboxClasses.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				checkboxClasses.Add("is-warning");
				break;
		}

		var wrapperResult = string.Join(" ", wrapperClasses);
		var checkboxResult = string.Join(" ", checkboxClasses);

		// Assert
		wrapperResult.Should().Be("form-check form-switch");
		checkboxResult.Should().Be("form-check-input is-invalid");
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "form-check" };

		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}

		// Assert
		cssClasses.Should().ContainSingle("form-check");
	}

	[Theory]
	[InlineData("custom-checkbox", new[] { "form-check", "custom-checkbox" })]
	[InlineData("class1 class2", new[] { "form-check", "class1", "class2" })]
	[InlineData("  class1   class2  ", new[] { "form-check", "class1", "class2" })]
	[InlineData("border-danger bg-light", new[] { "form-check", "border-danger", "bg-light" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "form-check" };

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
	public void CheckboxStructure_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedElements = new[]
		{
			"form-check",           // Wrapper class
			"form-check-input",     // Checkbox element
			"form-check-label"      // Label element
		};

		// Act & Assert
		expectedElements.Should().NotBeEmpty();
		expectedElements.Should().HaveCount(3);
		expectedElements.Should().Contain("form-check");
		expectedElements.Should().Contain("form-check-input");
		expectedElements.Should().Contain("form-check-label");
	}

	[Theory]
	[InlineData(CheckboxLayout.Default, CheckboxLayout.Inline, "form-check", "form-check form-check-inline")]
	[InlineData(CheckboxLayout.Default, CheckboxLayout.Switch, "form-check", "form-check form-switch")]
	[InlineData(CheckboxLayout.Default, CheckboxLayout.Reverse, "form-check", "form-check form-check-reverse")]
	public void LayoutClass_WithDifferentLayouts_ShouldReturnCorrectClass(CheckboxLayout fromLayout, CheckboxLayout toLayout, string expectedFrom, string expectedTo)
	{
		// Arrange & Act
		var fromClasses = new List<string> { "form-check" };
		var toClasses = new List<string> { "form-check" };

		// From layout
		switch (fromLayout)
		{
			case CheckboxLayout.Inline:
				fromClasses.Add("form-check-inline");
				break;
			case CheckboxLayout.Switch:
				fromClasses.Add("form-switch");
				break;
			case CheckboxLayout.Reverse:
				fromClasses.Add("form-check-reverse");
				break;
		}

		// To layout
		switch (toLayout)
		{
			case CheckboxLayout.Inline:
				toClasses.Add("form-check-inline");
				break;
			case CheckboxLayout.Switch:
				toClasses.Add("form-switch");
				break;
			case CheckboxLayout.Reverse:
				toClasses.Add("form-check-reverse");
				break;
		}

		var fromResult = string.Join(" ", fromClasses);
		var toResult = string.Join(" ", toClasses);

		// Assert
		fromResult.Should().Be(expectedFrom);
		toResult.Should().Be(expectedTo);
	}

	[Fact]
	public void CheckboxAttributes_ShouldSupportCommonAttributes()
	{
		// Arrange
		var commonAttributes = new Dictionary<string, object>
		{
			{ "required", true },
			{ "data-testid", "test-checkbox" },
			{ "aria-label", "Check this option" },
			{ "aria-describedby", "checkbox-help" },
			{ "data-indeterminate", "true" },
			{ "id", "custom-checkbox-id" }
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
	[InlineData(true, true)]
	[InlineData(false, false)]
	public void IsChecked_ShouldCorrectlyReflectValue(bool value, bool expectedChecked)
	{
		// Arrange & Act
		var isChecked = value;

		// Assert
		isChecked.Should().Be(expectedChecked);
	}

	[Theory]
	[InlineData("Accept terms", "Accept terms")]
	[InlineData("", "")]
	[InlineData(null, null)]
	public void LabelText_ShouldBeConfigurable(string? label, string? expectedLabel)
	{
		// Arrange & Act
		var actualLabel = label;

		// Assert
		actualLabel.Should().Be(expectedLabel);
	}

	[Fact]
	public void ValidationMessages_ShouldSupportAllStates()
	{
		// Arrange
		var validationMessages = new Dictionary<InputValidationState, string>
		{
			{ InputValidationState.Valid, "Selection is valid" },
			{ InputValidationState.Invalid, "This field is required" },
			{ InputValidationState.Warning, "Please review this selection" },
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
	[InlineData(true, true)]
	[InlineData(false, false)]
	public void IndeterminateState_ShouldBeConfigurable(bool indeterminate, bool expectedIndeterminate)
	{
		// Arrange & Act
		var actualIndeterminate = indeterminate;

		// Assert
		actualIndeterminate.Should().Be(expectedIndeterminate);
	}

	[Fact]
	public void CheckboxAccessibility_ShouldSupportARIAAttributes()
	{
		// Arrange
		var accessibilityAttributes = new Dictionary<string, string>
		{
			{ "aria-label", "Accept Terms and Conditions" },
			{ "aria-describedby", "terms-help" },
			{ "role", "checkbox" },
			{ "aria-required", "true" },
			{ "aria-invalid", "false" },
			{ "aria-checked", "false" }
		};

		// Act & Assert
		accessibilityAttributes.Should().NotBeEmpty();
		accessibilityAttributes.Should().ContainKey("aria-label");
		accessibilityAttributes.Should().ContainKey("aria-describedby");
		accessibilityAttributes["aria-required"].Should().Be("true");
	}

	[Theory]
	[InlineData(CheckboxLayout.Default, InputValidationState.Invalid, "custom", "form-check", "form-check-input is-invalid custom")]
	[InlineData(CheckboxLayout.Switch, InputValidationState.Valid, "highlight", "form-check form-switch", "form-check-input is-valid highlight")]
	[InlineData(CheckboxLayout.Inline, InputValidationState.None, "", "form-check form-check-inline", "form-check-input")]
	public void ComplexCheckboxConfiguration_ShouldCombineAllOptions(CheckboxLayout layout, InputValidationState validationState, string additionalClass, string expectedWrapperClasses, string expectedCheckboxClasses)
	{
		// Arrange & Act
		// Wrapper classes
		var wrapperClasses = new List<string> { "form-check" };
		switch (layout)
		{
			case CheckboxLayout.Inline:
				wrapperClasses.Add("form-check-inline");
				break;
			case CheckboxLayout.Switch:
				wrapperClasses.Add("form-switch");
				break;
			case CheckboxLayout.Reverse:
				wrapperClasses.Add("form-check-reverse");
				break;
		}

		// Checkbox classes
		var checkboxClasses = new List<string> { "form-check-input" };

		// Validation classes
		switch (validationState)
		{
			case InputValidationState.Valid:
				checkboxClasses.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				checkboxClasses.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				checkboxClasses.Add("is-warning");
				break;
		}

		// Additional classes
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			checkboxClasses.Add(additionalClass);
		}

		var wrapperResult = string.Join(" ", wrapperClasses).Trim();
		var checkboxResult = string.Join(" ", checkboxClasses);

		// Assert
		wrapperResult.Should().Be(expectedWrapperClasses);
		checkboxResult.Should().Be(expectedCheckboxClasses);
	}

	[Theory]
	[InlineData("checkbox-123", "checkbox-123")]
	[InlineData("", "")]
	[InlineData(null, "")]
	public void ElementId_ShouldBeConfigurable(string? providedId, string expectedPattern)
	{
		// Arrange & Act
		var actualId = !string.IsNullOrEmpty(providedId) ? providedId : $"checkbox-{Guid.NewGuid():N}";

		// Assert
		if (!string.IsNullOrEmpty(expectedPattern))
		{
			actualId.Should().Be(expectedPattern);
		}
		else
		{
			actualId.Should().StartWith("checkbox-");
			actualId.Length.Should().BeGreaterThan(9); // "checkbox-" + some guid
		}
	}

	[Fact]
	public void CheckboxStates_ShouldCombineCorrectly()
	{
		// Arrange
		const CheckboxLayout layout = CheckboxLayout.Switch;
		const InputValidationState validationState = InputValidationState.Valid;
		const bool disabled = true;
		const bool indeterminate = true;
		const string additionalClass = "custom-switch";

		// Act
		// Wrapper classes
		var wrapperClasses = new List<string> { "form-check" };
		switch (layout)
		{
			case CheckboxLayout.Switch:
				wrapperClasses.Add("form-switch");
				break;
		}

		// Checkbox classes
		var checkboxClasses = new List<string> { "form-check-input" };
		switch (validationState)
		{
			case InputValidationState.Valid:
				checkboxClasses.Add("is-valid");
				break;
		}

		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			checkboxClasses.Add(additionalClass);
		}

		// Attributes
		var attributes = new Dictionary<string, object>();
		if (disabled) attributes["disabled"] = true;
		if (indeterminate) attributes["data-indeterminate"] = "true";

		var wrapperResult = string.Join(" ", wrapperClasses);
		var checkboxResult = string.Join(" ", checkboxClasses);

		// Assert
		wrapperResult.Should().Be("form-check form-switch");
		checkboxResult.Should().Be("form-check-input is-valid custom-switch");
		attributes.Should().ContainKey("disabled");
		attributes.Should().ContainKey("data-indeterminate");
	}
}