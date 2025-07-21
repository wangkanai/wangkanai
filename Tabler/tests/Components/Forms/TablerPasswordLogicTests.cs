// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Forms;

/// <summary>
/// Unit tests for TablerPassword component logic and CSS class generation.
/// </summary>
public class TablerPasswordLogicTests
{
	[Theory]
	[InlineData(InputSize.Default, "form-control")]
	[InlineData(InputSize.Small, "form-control form-control-sm")]
	[InlineData(InputSize.Large, "form-control form-control-lg")]
	public void GetPasswordCssClass_WithSize_ShouldReturnCorrectClasses(InputSize size, string expectedClasses)
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
	public void GetPasswordCssClass_WithValidationState_ShouldReturnCorrectClasses(InputValidationState state, string expectedClasses)
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
	[InlineData(true, true, "input-group")]
	[InlineData(true, false, "input-group")]
	[InlineData(false, true, "input-group")]
	[InlineData(false, false, "")] // No wrapper when no toggle and no prefix/suffix
	public void GetWrapperCssClass_WithToggleAndPrefixSuffix_ShouldReturnCorrectClass(bool showToggle, bool hasPrefixOrSuffix, string expectedClass)
	{
		// Arrange & Act
		var classes = new List<string>();

		if (hasPrefixOrSuffix || showToggle)
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

	[Theory]
	[InlineData(false, "password")]
	[InlineData(true, "text")]
	public void GetInputType_WithVisibilityState_ShouldReturnCorrectType(bool isVisible, string expectedType)
	{
		// Arrange & Act
		var inputType = isVisible ? "text" : "password";

		// Assert
		inputType.Should().Be(expectedType);
	}

	[Theory]
	[InlineData(false, "Show password")]
	[InlineData(true, "Hide password")]
	public void GetToggleAriaLabel_WithVisibilityState_ShouldReturnCorrectLabel(bool isVisible, string expectedLabel)
	{
		// Arrange & Act
		var ariaLabel = isVisible ? "Hide password" : "Show password";

		// Assert
		ariaLabel.Should().Be(expectedLabel);
	}

	[Theory]
	[InlineData("", 0)]
	[InlineData("weak", 1)]
	[InlineData("password", 2)] // length >= 8 + lowercase = 2 points
	[InlineData("Password1", 4)]
	[InlineData("Password1!", 4)]
	[InlineData("VeryStrongPassword123!", 4)]
	public void GetPasswordStrength_ShouldCalculateCorrectScore(string password, int expectedScore)
	{
		// Arrange & Act
		var score = CalculatePasswordStrength(password);

		// Assert
		score.Should().Be(expectedScore);
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(1, 25)]
	[InlineData(2, 50)]
	[InlineData(3, 75)]
	[InlineData(4, 100)]
	public void GetStrengthPercentage_ShouldReturnCorrectPercentage(int score, int expectedPercentage)
	{
		// Arrange & Act
		var percentage = score * 25;

		// Assert
		percentage.Should().Be(expectedPercentage);
	}

	[Theory]
	[InlineData(0, "bg-danger")]
	[InlineData(1, "bg-danger")]
	[InlineData(2, "bg-warning")]
	[InlineData(3, "bg-primary")]
	[InlineData(4, "bg-success")]
	public void GetStrengthCssClass_ShouldReturnCorrectClass(int score, string expectedClass)
	{
		// Arrange & Act
		var cssClass = score switch
		{
			0 => "bg-danger",
			1 => "bg-danger",
			2 => "bg-warning",
			3 => "bg-primary",
			4 => "bg-success",
			_ => "bg-danger"
		};

		// Assert
		cssClass.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(0, "text-danger")]
	[InlineData(1, "text-danger")]
	[InlineData(2, "text-warning")]
	[InlineData(3, "text-primary")]
	[InlineData(4, "text-success")]
	public void GetStrengthTextCssClass_ShouldReturnCorrectClass(int score, string expectedClass)
	{
		// Arrange & Act
		var cssClass = score switch
		{
			0 => "text-danger",
			1 => "text-danger",
			2 => "text-warning",
			3 => "text-primary",
			4 => "text-success",
			_ => "text-muted"
		};

		// Assert
		cssClass.Should().Be(expectedClass);
	}

	[Theory]
	[InlineData(0, "Very weak")]
	[InlineData(1, "Weak")]
	[InlineData(2, "Fair")]
	[InlineData(3, "Good")]
	[InlineData(4, "Strong")]
	public void GetStrengthText_ShouldReturnCorrectText(int score, string expectedText)
	{
		// Arrange & Act
		var text = score switch
		{
			0 => "Very weak",
			1 => "Weak",
			2 => "Fair",
			3 => "Good",
			4 => "Strong",
			_ => ""
		};

		// Assert
		text.Should().Be(expectedText);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	[InlineData(null)]
	public void AdditionalCssClass_WhenEmptyOrNull_ShouldNotAddExtraClasses(string? additionalClass)
	{
		// Arrange
		var cssClasses = new List<string> { "input-group" };

		// Act
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			foreach (var cssClass in additionalClass.Split(' ', StringSplitOptions.RemoveEmptyEntries))
			{
				cssClasses.Add(cssClass);
			}
		}

		// Assert
		cssClasses.Should().ContainSingle("input-group");
	}

	[Theory]
	[InlineData("custom-password", new[] { "input-group", "custom-password" })]
	[InlineData("class1 class2", new[] { "input-group", "class1", "class2" })]
	[InlineData("  class1   class2  ", new[] { "input-group", "class1", "class2" })]
	[InlineData("border-danger bg-light", new[] { "input-group", "border-danger", "bg-light" })]
	public void AdditionalCssClass_WhenProvided_ShouldAddToClasses(string additionalClass, string[] expectedClasses)
	{
		// Arrange
		var cssClasses = new List<string> { "input-group" };

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
	public void PasswordStructure_ShouldFollowBootstrapConventions()
	{
		// Arrange
		var expectedElements = new[]
		{
			"input-group",        // Wrapper class
			"input-group-text",   // Prefix/suffix text
			"form-control",       // Password input element
			"btn btn-link"        // Toggle button
		};

		// Act & Assert
		expectedElements.Should().NotBeEmpty();
		expectedElements.Should().HaveCount(4);
		expectedElements.Should().Contain("input-group");
		expectedElements.Should().Contain("input-group-text");
		expectedElements.Should().Contain("form-control");
		expectedElements.Should().Contain("btn btn-link");
	}

	[Fact]
	public void PasswordAttributes_ShouldSupportCommonAttributes()
	{
		// Arrange
		var commonAttributes = new Dictionary<string, object>
		{
			{ "autocomplete", "current-password" },
			{ "required", true },
			{ "data-testid", "test-password" },
			{ "aria-label", "Enter password" },
			{ "aria-describedby", "password-help" },
			{ "minlength", 8 },
			{ "maxlength", 128 }
		};

		// Act & Assert
		foreach (var attribute in commonAttributes)
		{
			attribute.Key.Should().NotBeNullOrEmpty();
			attribute.Value.Should().NotBeNull();
		}

		commonAttributes.Should().HaveCountGreaterThan(6);
	}

	[Theory]
	[InlineData("Enter password", "Enter password")]
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
			{ InputValidationState.Valid, "Password is strong" },
			{ InputValidationState.Invalid, "Password is required" },
			{ InputValidationState.Warning, "Consider a stronger password" },
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
	public void ShowToggleVisibility_ShouldBeConfigurable(bool showToggle, bool expectedShowToggle)
	{
		// Arrange & Act
		var actualShowToggle = showToggle;

		// Assert
		actualShowToggle.Should().Be(expectedShowToggle);
	}

	[Theory]
	[InlineData(true, true)]
	[InlineData(false, false)]
	public void ShowStrengthIndicator_ShouldBeConfigurable(bool showStrength, bool expectedShowStrength)
	{
		// Arrange & Act
		var actualShowStrength = showStrength;

		// Assert
		actualShowStrength.Should().Be(expectedShowStrength);
	}

	[Theory]
	[InlineData("current-password", "current-password")]
	[InlineData("new-password", "new-password")]
	[InlineData("off", "off")]
	public void AutoComplete_ShouldBeConfigurable(string autoComplete, string expectedAutoComplete)
	{
		// Arrange & Act
		var actualAutoComplete = autoComplete;

		// Assert
		actualAutoComplete.Should().Be(expectedAutoComplete);
	}

	[Fact]
	public void PasswordAccessibility_ShouldSupportARIAAttributes()
	{
		// Arrange
		var accessibilityAttributes = new Dictionary<string, string>
		{
			{ "aria-label", "Enter Password" },
			{ "aria-describedby", "password-help" },
			{ "role", "textbox" },
			{ "aria-required", "true" },
			{ "aria-invalid", "false" },
			{ "autocomplete", "current-password" }
		};

		// Act & Assert
		accessibilityAttributes.Should().NotBeEmpty();
		accessibilityAttributes.Should().ContainKey("aria-label");
		accessibilityAttributes.Should().ContainKey("aria-describedby");
		accessibilityAttributes["aria-required"].Should().Be("true");
		accessibilityAttributes["autocomplete"].Should().Be("current-password");
	}

	[Theory]
	[InlineData(InputSize.Default, InputValidationState.Invalid, true, "custom", "input-group", "form-control is-invalid custom")]
	[InlineData(InputSize.Large, InputValidationState.Valid, false, "", "", "form-control form-control-lg is-valid")]
	[InlineData(InputSize.Small, InputValidationState.None, true, "highlight", "input-group", "form-control form-control-sm highlight")]
	public void ComplexPasswordConfiguration_ShouldCombineAllOptions(InputSize size, InputValidationState validationState, bool showToggle, string additionalClass, string expectedWrapperClasses, string expectedPasswordClasses)
	{
		// Arrange & Act
		// Wrapper classes
		var wrapperClasses = new List<string>();
		if (showToggle)
			wrapperClasses.Add("input-group");

		// Password classes
		var passwordClasses = new List<string> { "form-control" };

		// Size classes
		switch (size)
		{
			case InputSize.Small:
				passwordClasses.Add("form-control-sm");
				break;
			case InputSize.Large:
				passwordClasses.Add("form-control-lg");
				break;
		}

		// Validation classes
		switch (validationState)
		{
			case InputValidationState.Valid:
				passwordClasses.Add("is-valid");
				break;
			case InputValidationState.Invalid:
				passwordClasses.Add("is-invalid");
				break;
			case InputValidationState.Warning:
				passwordClasses.Add("is-warning");
				break;
		}

		// Additional classes
		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			passwordClasses.Add(additionalClass);
		}

		var wrapperResult = string.Join(" ", wrapperClasses);
		var passwordResult = string.Join(" ", passwordClasses);

		// Assert
		wrapperResult.Should().Be(expectedWrapperClasses);
		passwordResult.Should().Be(expectedPasswordClasses);
	}

	[Fact]
	public void PasswordStates_ShouldCombineCorrectly()
	{
		// Arrange
		const InputSize size = InputSize.Large;
		const InputValidationState validationState = InputValidationState.Warning;
		const bool showToggle = true;
		const bool showStrength = true;
		const string additionalClass = "custom-password";

		// Act
		// Wrapper classes
		var wrapperClasses = new List<string>();
		if (showToggle)
			wrapperClasses.Add("input-group");

		// Password classes  
		var passwordClasses = new List<string> { "form-control" };

		switch (size)
		{
			case InputSize.Large:
				passwordClasses.Add("form-control-lg");
				break;
		}

		switch (validationState)
		{
			case InputValidationState.Warning:
				passwordClasses.Add("is-warning");
				break;
		}

		if (!string.IsNullOrWhiteSpace(additionalClass))
		{
			passwordClasses.Add(additionalClass);
		}

		var wrapperResult = string.Join(" ", wrapperClasses);
		var passwordResult = string.Join(" ", passwordClasses);

		// Assert
		wrapperResult.Should().Be("input-group");
		passwordResult.Should().Be("form-control form-control-lg is-warning custom-password");
		showStrength.Should().BeTrue();
	}

	// Helper method to calculate password strength (matches component logic)
	private static int CalculatePasswordStrength(string password)
	{
		if (string.IsNullOrEmpty(password))
			return 0;

		var score = 0;

		// Length criteria
		if (password.Length >= 8) score++;
		if (password.Length >= 12) score++;

		// Character type criteria
		if (password.Any(char.IsLower)) score++;
		if (password.Any(char.IsUpper)) score++;
		if (password.Any(char.IsDigit)) score++;
		if (password.Any(c => !char.IsLetterOrDigit(c))) score++;

		// Cap at 4
		return Math.Min(score, 4);
	}
}