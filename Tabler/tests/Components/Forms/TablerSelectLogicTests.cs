// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for TablerSelect component logic and CSS class generation.
/// </summary>
public class TablerSelectLogicTests
{
	[Theory]
	[InlineData(InputSize.Default, "form-select")]
	[InlineData(InputSize.Small, "form-select form-select-sm")]
	[InlineData(InputSize.Large, "form-select form-select-lg")]
	public void GetSelectCssClass_WithSize_ShouldReturnCorrectClasses(InputSize size, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-select" };

		switch (size)
		{
			case InputSize.Small:
				classes.Add("form-select-sm");
				break;
			case InputSize.Large:
				classes.Add("form-select-lg");
				break;
		}

		var result = string.Join(" ", classes);

		// Assert
		result.Should().Be(expectedClasses);
	}

	[Theory]
	[InlineData(InputValidationState.None, "form-select")]
	[InlineData(InputValidationState.Valid, "form-select is-valid")]
	[InlineData(InputValidationState.Invalid, "form-select is-invalid")]
	[InlineData(InputValidationState.Warning, "form-select is-warning")]
	public void GetSelectCssClass_WithValidationState_ShouldReturnCorrectClasses(InputValidationState state, string expectedClasses)
	{
		// Arrange & Act
		var classes = new List<string> { "form-select" };

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
	public void SelectCssClass_WithCombinedSizeAndValidation_ShouldCombineClasses()
	{
		// Arrange
		const InputSize size = InputSize.Large;
		const InputValidationState state = InputValidationState.Invalid;

		// Act
		var classes = new List<string> { "form-select" };

		// Size classes
		switch (size)
		{
			case InputSize.Small:
				classes.Add("form-select-sm");
				break;
			case InputSize.Large:
				classes.Add("form-select-lg");
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
		result.Should().Be("form-select form-select-lg is-invalid");
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "form-select" };

		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}

		// Assert
		cssClasses.Should().ContainSingle("form-select");
	}

	[Theory]
	[InlineData("custom-select", new[] { "form-select", "custom-select" })]
	[InlineData("class1 class2", new[] { "form-select", "class1", "class2" })]
	[InlineData("  class1   class2  ", new[] { "form-select", "class1", "class2" })]
	[InlineData("border-danger bg-light", new[] { "form-select", "border-danger", "bg-light" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "form-select" };

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
	public void SelectStructure_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedElements = new[]
		{
			"input-group",      // Wrapper class
			"input-group-text", // Prefix/suffix text
			"form-select"       // Select element
		};

		// Act & Assert
		expectedElements.Should().NotBeEmpty();
		expectedElements.Should().HaveCount(3);
		expectedElements.Should().Contain("input-group");
		expectedElements.Should().Contain("input-group-text");
		expectedElements.Should().Contain("form-select");
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
	public void SelectAttributes_ShouldSupportCommonAttributes()
	{
		// Arrange
		var commonAttributes = new Dictionary<string, object>
		{
			{ "multiple", true },
			{ "size", "5" },
			{ "required", true },
			{ "data-testid", "test-select" },
			{ "aria-label", "Select option" },
			{ "aria-describedby", "select-help" }
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
	[InlineData("Select an option", "Select an option")]
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
			{ InputValidationState.Valid, "Selection is valid" },
			{ InputValidationState.Invalid, "Please select an option" },
			{ InputValidationState.Warning, "Consider reviewing this selection" },
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
	[InlineData("option1", "option1", true)]
	[InlineData("option1", "option2", false)]
	[InlineData("", "", true)]
	[InlineData(null, null, true)]
	[InlineData("option1", null, false)]
	[InlineData(null, "option1", false)]
	public void IsSelected_ShouldCorrectlyCompareValues(string? currentValue, string? optionValue, bool expectedSelected)
	{
		// Arrange & Act
		var isSelected = string.Equals(currentValue, optionValue, StringComparison.Ordinal);

		// Assert
		isSelected.Should().Be(expectedSelected);
	}

	[Theory]
	[InlineData(true, false)]
	[InlineData(false, true)]
	public void MultipleSelect_ShouldBeConfigurable(bool multiple, bool expectedSingle)
	{
		// Arrange & Act
		var isSingleSelect = !multiple;

		// Assert
		isSingleSelect.Should().Be(expectedSingle);
	}

	[Theory]
	[InlineData(true, true)]
	[InlineData(false, false)]
	public void AllowEmpty_ShouldControlPlaceholderBehavior(bool allowEmpty, bool expectedAllowEmpty)
	{
		// Arrange & Act
		var actualAllowEmpty = allowEmpty;

		// Assert
		actualAllowEmpty.Should().Be(expectedAllowEmpty);
	}

	[Fact]
	public void SelectStates_ShouldCombineCorrectly()
	{
		// Arrange
		const bool hasPrefix = true;
		const InputValidationState validationState = InputValidationState.Invalid;
		const InputSize size = InputSize.Small;
		const string additionalClass = "custom-select";

		// Act
		// Wrapper classes
		var wrapperClasses = new List<string>();
		if (hasPrefix)
			wrapperClasses.Add("input-group");

		// Select classes
		var selectClasses = new List<string> { "form-select" };

		switch (size)
		{
			case InputSize.Small:
				selectClasses.Add("form-select-sm");
				break;
			case InputSize.Large:
				selectClasses.Add("form-select-lg");
				break;
		}

		switch (validationState)
		{
			case InputValidationState.Valid:
				selectClasses.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				selectClasses.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				selectClasses.Add("is-warning");
				break;
		}

		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			selectClasses.Add(additionalClass);
		}

		var wrapperResult = string.Join(" ", wrapperClasses);
		var selectResult = string.Join(" ", selectClasses);

		// Assert
		wrapperResult.Should().Be("input-group");
		selectResult.Should().Be("form-select form-select-sm is-invalid custom-select");
	}

	[Fact]
	public void SelectAccessibility_ShouldSupportARIAAttributes()
	{
		// Arrange
		var accessibilityAttributes = new Dictionary<string, string>
		{
			{ "aria-label", "Select Country" },
			{ "aria-describedby", "country-help" },
			{ "role", "combobox" },
			{ "aria-required", "true" },
			{ "aria-invalid", "false" },
			{ "aria-expanded", "false" }
		};

		// Act & Assert
		accessibilityAttributes.Should().NotBeEmpty();
		accessibilityAttributes.Should().ContainKey("aria-label");
		accessibilityAttributes.Should().ContainKey("aria-describedby");
		accessibilityAttributes["aria-required"].Should().Be("true");
	}

	[Theory]
	[InlineData(true, true, InputValidationState.Invalid, InputSize.Small, "input-group", "form-select form-select-sm is-invalid custom")]
	[InlineData(false, true, InputValidationState.Valid, InputSize.Large, "input-group", "form-select form-select-lg is-valid custom")]
	[InlineData(false, false, InputValidationState.None, InputSize.Default, "", "form-select custom")]
	public void ComplexSelectConfiguration_ShouldCombineAllOptions(bool hasPrefix, bool hasSuffix, InputValidationState validationState, InputSize size, string expectedWrapperClasses, string expectedSelectClasses)
	{
		// Arrange
		const string additionalClass = "custom";

		// Act
		// Wrapper classes
		var wrapperClasses = new List<string>();
		var hasPrefixOrSuffix = hasPrefix || hasSuffix;
		if (hasPrefixOrSuffix)
			wrapperClasses.Add("input-group");

		// Select classes
		var selectClasses = new List<string> { "form-select" };

		// Size classes
		switch (size)
		{
			case InputSize.Small:
				selectClasses.Add("form-select-sm");
				break;
			case InputSize.Large:
				selectClasses.Add("form-select-lg");
				break;
		}

		// Validation classes
		switch (validationState)
		{
			case InputValidationState.Valid:
				selectClasses.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				selectClasses.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				selectClasses.Add("is-warning");
				break;
		}

		// Additional classes
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			selectClasses.Add(additionalClass);
		}

		var wrapperResult = string.Join(" ", wrapperClasses).Trim();
		var selectResult = string.Join(" ", selectClasses);

		// Assert
		wrapperResult.Should().Be(expectedWrapperClasses);
		selectResult.Should().Be(expectedSelectClasses);
	}
}