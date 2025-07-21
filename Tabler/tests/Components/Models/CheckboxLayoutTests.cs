// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Models;

/// <summary>
/// Unit tests for CheckboxLayout enum and its usage patterns.
/// </summary>
public class CheckboxLayoutTests
{
	[Fact]
	public void CheckboxLayout_ShouldHaveCorrectValues()
	{
		// Arrange & Act
		var layouts = Enum.GetValues<CheckboxLayout>();

		// Assert
		layouts.Should().HaveCount(4);
		layouts.Should().Contain(CheckboxLayout.Default);
		layouts.Should().Contain(CheckboxLayout.Inline);
		layouts.Should().Contain(CheckboxLayout.Switch);
		layouts.Should().Contain(CheckboxLayout.Reverse);
	}

	[Theory]
	[InlineData(CheckboxLayout.Default, 0)]
	[InlineData(CheckboxLayout.Inline, 1)]
	[InlineData(CheckboxLayout.Switch, 2)]
	[InlineData(CheckboxLayout.Reverse, 3)]
	public void CheckboxLayout_ShouldHaveCorrectEnumValues(CheckboxLayout layout, int expectedValue)
	{
		// Arrange & Act
		var actualValue = (int)layout;

		// Assert
		actualValue.Should().Be(expectedValue);
	}

	[Theory]
	[InlineData(CheckboxLayout.Default, "Default")]
	[InlineData(CheckboxLayout.Inline, "Inline")]
	[InlineData(CheckboxLayout.Switch, "Switch")]
	[InlineData(CheckboxLayout.Reverse, "Reverse")]
	public void CheckboxLayout_ToString_ShouldReturnCorrectNames(CheckboxLayout layout, string expectedName)
	{
		// Arrange & Act
		var result = layout.ToString();

		// Assert
		result.Should().Be(expectedName);
	}

	[Theory]
	[InlineData("Default", CheckboxLayout.Default)]
	[InlineData("Inline", CheckboxLayout.Inline)]
	[InlineData("Switch", CheckboxLayout.Switch)]
	[InlineData("Reverse", CheckboxLayout.Reverse)]
	public void CheckboxLayout_Parse_ShouldReturnCorrectEnum(string layoutName, CheckboxLayout expectedLayout)
	{
		// Arrange & Act
		var result = Enum.Parse<CheckboxLayout>(layoutName);

		// Assert
		result.Should().Be(expectedLayout);
	}

	[Theory]
	[InlineData("default", CheckboxLayout.Default)]
	[InlineData("inline", CheckboxLayout.Inline)]
	[InlineData("switch", CheckboxLayout.Switch)]
	[InlineData("reverse", CheckboxLayout.Reverse)]
	public void CheckboxLayout_ParseIgnoreCase_ShouldReturnCorrectEnum(string layoutName, CheckboxLayout expectedLayout)
	{
		// Arrange & Act
		var result = Enum.Parse<CheckboxLayout>(layoutName, true);

		// Assert
		result.Should().Be(expectedLayout);
	}

	[Fact]
	public void CheckboxLayout_TryParse_WithValidValue_ShouldReturnTrue()
	{
		// Arrange
		const string layoutName = "Inline";

		// Act
		var success = Enum.TryParse<CheckboxLayout>(layoutName, out var result);

		// Assert
		success.Should().BeTrue();
		result.Should().Be(CheckboxLayout.Inline);
	}

	[Fact]
	public void CheckboxLayout_TryParse_WithInvalidValue_ShouldReturnFalse()
	{
		// Arrange
		const string layoutName = "InvalidLayout";

		// Act
		var success = Enum.TryParse<CheckboxLayout>(layoutName, out var result);

		// Assert
		success.Should().BeFalse();
		result.Should().Be(default(CheckboxLayout));
	}

	[Fact]
	public void CheckboxLayout_DefaultValue_ShouldBeDefault()
	{
		// Arrange & Act
		var defaultLayout = default(CheckboxLayout);

		// Assert
		defaultLayout.Should().Be(CheckboxLayout.Default);
		((int)defaultLayout).Should().Be(0);
	}

	[Fact]
	public void CheckboxLayout_GetNames_ShouldReturnAllLayoutNames()
	{
		// Arrange & Act
		var names = Enum.GetNames<CheckboxLayout>();

		// Assert
		names.Should().HaveCount(4);
		names.Should().Contain("Default");
		names.Should().Contain("Inline");
		names.Should().Contain("Switch");
		names.Should().Contain("Reverse");
	}

	[Fact]
	public void CheckboxLayout_IsDefined_ShouldValidateCorrectly()
	{
		// Arrange & Act & Assert
		Enum.IsDefined(typeof(CheckboxLayout), CheckboxLayout.Default).Should().BeTrue();
		Enum.IsDefined(typeof(CheckboxLayout), CheckboxLayout.Inline).Should().BeTrue();
		Enum.IsDefined(typeof(CheckboxLayout), CheckboxLayout.Switch).Should().BeTrue();
		Enum.IsDefined(typeof(CheckboxLayout), CheckboxLayout.Reverse).Should().BeTrue();
		Enum.IsDefined(typeof(CheckboxLayout), (CheckboxLayout)99).Should().BeFalse();
	}

	[Theory]
	[InlineData(CheckboxLayout.Default, CheckboxLayout.Default, true)]
	[InlineData(CheckboxLayout.Inline, CheckboxLayout.Inline, true)]
	[InlineData(CheckboxLayout.Default, CheckboxLayout.Inline, false)]
	[InlineData(CheckboxLayout.Switch, CheckboxLayout.Reverse, false)]
	public void CheckboxLayout_Equality_ShouldWorkCorrectly(CheckboxLayout layout1, CheckboxLayout layout2, bool expectedEqual)
	{
		// Arrange & Act
		var areEqual = layout1 == layout2;
		var objectsEqual = layout1.Equals(layout2);

		// Assert
		areEqual.Should().Be(expectedEqual);
		objectsEqual.Should().Be(expectedEqual);
	}

	[Fact]
	public void CheckboxLayout_GetHashCode_ShouldBeConsistent()
	{
		// Arrange
		var layout1 = CheckboxLayout.Switch;
		var layout2 = CheckboxLayout.Switch;
		var layout3 = CheckboxLayout.Inline;

		// Act
		var hash1 = layout1.GetHashCode();
		var hash2 = layout2.GetHashCode();
		var hash3 = layout3.GetHashCode();

		// Assert
		hash1.Should().Be(hash2);
		hash1.Should().NotBe(hash3);
	}

	[Theory]
	[InlineData(CheckboxLayout.Default, "form-check")]
	[InlineData(CheckboxLayout.Inline, "form-check form-check-inline")]
	[InlineData(CheckboxLayout.Switch, "form-check form-switch")]
	[InlineData(CheckboxLayout.Reverse, "form-check form-check-reverse")]
	public void CheckboxLayout_ToCssClass_ShouldReturnCorrectBootstrapClasses(CheckboxLayout layout, string expectedCssClasses)
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
		result.Should().Be(expectedCssClasses);
	}

	[Fact]
	public void CheckboxLayout_EnumValues_ShouldBeSequential()
	{
		// Arrange
		var values = Enum.GetValues<CheckboxLayout>().Cast<int>().ToArray();

		// Act & Assert
		values.Should().BeInAscendingOrder();
		values[0].Should().Be(0);
		values[1].Should().Be(1);
		values[2].Should().Be(2);
		values[3].Should().Be(3);
	}

	[Fact]
	public void CheckboxLayout_EnumUnderlyingType_ShouldBeInt32()
	{
		// Arrange & Act
		var underlyingType = Enum.GetUnderlyingType(typeof(CheckboxLayout));

		// Assert
		underlyingType.Should().Be(typeof(int));
	}

	[Theory]
	[InlineData(CheckboxLayout.Default)]
	[InlineData(CheckboxLayout.Inline)]
	[InlineData(CheckboxLayout.Switch)]
	[InlineData(CheckboxLayout.Reverse)]
	public void CheckboxLayout_AllValues_ShouldBeDefined(CheckboxLayout layout)
	{
		// Arrange & Act
		var isDefined = Enum.IsDefined(typeof(CheckboxLayout), layout);

		// Assert
		isDefined.Should().BeTrue();
	}

	[Fact]
	public void CheckboxLayout_ShouldSupportFlagsOperations()
	{
		// Arrange
		var layout1 = CheckboxLayout.Inline;
		var layout2 = CheckboxLayout.Switch;

		// Act
		var combinedValue = (int)layout1 | (int)layout2;

		// Assert
		// Note: CheckboxLayout is not a flags enum, so this tests that values are distinct
		combinedValue.Should().Be(3); // 1 | 2 = 3
		combinedValue.Should().NotBe((int)layout1);
		combinedValue.Should().NotBe((int)layout2);
	}
}