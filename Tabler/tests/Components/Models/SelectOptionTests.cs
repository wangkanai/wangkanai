// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using FluentAssertions;
using Wangkanai.Tabler.Models;

namespace Wangkanai.Tabler.Tests.Components.Models;

/// <summary>
/// Unit tests for SelectOption model and utility methods.
/// </summary>
public class SelectOptionTests
{
	[Fact]
	public void DefaultConstructor_ShouldInitializeWithEmptyValues()
	{
		// Arrange & Act
		var option = new SelectOption();

		// Assert
		option.Value.Should().Be(string.Empty);
		option.Text.Should().Be(string.Empty);
		option.Disabled.Should().BeFalse();
		option.Group.Should().BeNull();
	}

	[Fact]
	public void ValueTextConstructor_ShouldInitializeCorrectly()
	{
		// Arrange
		const string value = "option1";
		const string text = "Option 1";

		// Act
		var option = new SelectOption(value, text);

		// Assert
		option.Value.Should().Be(value);
		option.Text.Should().Be(text);
		option.Disabled.Should().BeFalse();
		option.Group.Should().BeNull();
	}

	[Fact]
	public void FullConstructor_ShouldInitializeCorrectly()
	{
		// Arrange
		const string value = "option1";
		const string text = "Option 1";
		const bool disabled = true;

		// Act
		var option = new SelectOption(value, text, disabled);

		// Assert
		option.Value.Should().Be(value);
		option.Text.Should().Be(text);
		option.Disabled.Should().Be(disabled);
		option.Group.Should().BeNull();
	}

	[Fact]
	public void Create_ShouldReturnCorrectOption()
	{
		// Arrange
		const string value = "test";
		const string text = "Test Option";

		// Act
		var option = SelectOption.Create(value, text);

		// Assert
		option.Value.Should().Be(value);
		option.Text.Should().Be(text);
		option.Disabled.Should().BeFalse();
	}

	[Fact]
	public void CreateDisabled_ShouldReturnDisabledOption()
	{
		// Arrange
		const string value = "test";
		const string text = "Test Option";

		// Act
		var option = SelectOption.CreateDisabled(value, text);

		// Assert
		option.Value.Should().Be(value);
		option.Text.Should().Be(text);
		option.Disabled.Should().BeTrue();
	}

	[Fact]
	public void FromDictionary_ShouldCreateOptionsFromDictionary()
	{
		// Arrange
		var items = new Dictionary<string, string>
		{
			{ "value1", "Text 1" },
			{ "value2", "Text 2" },
			{ "value3", "Text 3" }
		};

		// Act
		var options = SelectOption.FromDictionary(items).ToList();

		// Assert
		options.Should().HaveCount(3);
		options[0].Value.Should().Be("value1");
		options[0].Text.Should().Be("Text 1");
		options[1].Value.Should().Be("value2");
		options[1].Text.Should().Be("Text 2");
		options[2].Value.Should().Be("value3");
		options[2].Text.Should().Be("Text 3");
	}

	[Fact]
	public void FromEnum_ShouldCreateOptionsFromEnum()
	{
		// Act
		var options = SelectOption.FromEnum<TestEnum>().ToList();

		// Assert
		options.Should().HaveCount(3);
		options.Should().Contain(o => o.Value == "Option1" && o.Text == "Option1");
		options.Should().Contain(o => o.Value == "Option2" && o.Text == "Option2");
		options.Should().Contain(o => o.Value == "Option3" && o.Text == "Option3");
	}

	[Fact]
	public void FromEnum_WithFormatter_ShouldCreateOptionsWithCustomText()
	{
		// Act
		var options = SelectOption.FromEnum<TestEnum>(e => $"Custom {e}").ToList();

		// Assert
		options.Should().HaveCount(3);
		options.Should().Contain(o => o.Value == "Option1" && o.Text == "Custom Option1");
		options.Should().Contain(o => o.Value == "Option2" && o.Text == "Custom Option2");
		options.Should().Contain(o => o.Value == "Option3" && o.Text == "Custom Option3");
	}

	[Fact]
	public void ToString_ShouldReturnFormattedString()
	{
		// Arrange
		var option = new SelectOption("value1", "Text 1");

		// Act
		var result = option.ToString();

		// Assert
		result.Should().Be("value1: Text 1");
	}

	[Fact]
	public void Equals_WithSameValues_ShouldReturnTrue()
	{
		// Arrange
		var option1 = new SelectOption("value1", "Text 1", false);
		var option2 = new SelectOption("value1", "Text 1", false);

		// Act & Assert
		option1.Equals(option2).Should().BeTrue();
		option1.Equals((object)option2).Should().BeTrue();
	}

	[Fact]
	public void Equals_WithDifferentValues_ShouldReturnFalse()
	{
		// Arrange
		var option1 = new SelectOption("value1", "Text 1", false);
		var option2 = new SelectOption("value2", "Text 1", false);

		// Act & Assert
		option1.Equals(option2).Should().BeFalse();
	}

	[Fact]
	public void Equals_WithDifferentText_ShouldReturnFalse()
	{
		// Arrange
		var option1 = new SelectOption("value1", "Text 1", false);
		var option2 = new SelectOption("value1", "Text 2", false);

		// Act & Assert
		option1.Equals(option2).Should().BeFalse();
	}

	[Fact]
	public void Equals_WithDifferentDisabled_ShouldReturnFalse()
	{
		// Arrange
		var option1 = new SelectOption("value1", "Text 1", false);
		var option2 = new SelectOption("value1", "Text 1", true);

		// Act & Assert
		option1.Equals(option2).Should().BeFalse();
	}

	[Fact]
	public void Equals_WithNull_ShouldReturnFalse()
	{
		// Arrange
		var option = new SelectOption("value1", "Text 1");

		// Act & Assert
		option.Equals(null).Should().BeFalse();
	}

	[Fact]
	public void Equals_WithDifferentType_ShouldReturnFalse()
	{
		// Arrange
		var option = new SelectOption("value1", "Text 1");
		var other = "not an option";

		// Act & Assert
		option.Equals(other).Should().BeFalse();
	}

	[Fact]
	public void GetHashCode_WithSameValues_ShouldReturnSameHash()
	{
		// Arrange
		var option1 = new SelectOption("value1", "Text 1", false);
		var option2 = new SelectOption("value1", "Text 1", false);

		// Act
		var hash1 = option1.GetHashCode();
		var hash2 = option2.GetHashCode();

		// Assert
		hash1.Should().Be(hash2);
	}

	[Fact]
	public void GetHashCode_WithDifferentValues_ShouldReturnDifferentHash()
	{
		// Arrange
		var option1 = new SelectOption("value1", "Text 1", false);
		var option2 = new SelectOption("value2", "Text 1", false);

		// Act
		var hash1 = option1.GetHashCode();
		var hash2 = option2.GetHashCode();

		// Assert
		hash1.Should().NotBe(hash2);
	}

	[Fact]
	public void Group_Property_ShouldBeSettable()
	{
		// Arrange
		var option = new SelectOption("value1", "Text 1");
		const string groupName = "Group A";

		// Act
		option.Group = groupName;

		// Assert
		option.Group.Should().Be(groupName);
	}

	[Theory]
	[InlineData("", "")]
	[InlineData("value1", "text1")]
	[InlineData("complex-value", "Complex Text Value")]
	public void Properties_ShouldBeSettableAndGettable(string value, string text)
	{
		// Arrange
		var option = new SelectOption();

		// Act
		option.Value = value;
		option.Text = text;

		// Assert
		option.Value.Should().Be(value);
		option.Text.Should().Be(text);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Disabled_Property_ShouldBeSettable(bool disabled)
	{
		// Arrange
		var option = new SelectOption("value", "text");

		// Act
		option.Disabled = disabled;

		// Assert
		option.Disabled.Should().Be(disabled);
	}

	[Fact]
	public void FromDictionary_WithEmptyDictionary_ShouldReturnEmpty()
	{
		// Arrange
		var items = new Dictionary<string, string>();

		// Act
		var options = SelectOption.FromDictionary(items);

		// Assert
		options.Should().BeEmpty();
	}

	[Fact]
	public void FromEnum_WithEmptyEnum_ShouldHandleCorrectly()
	{
		// Act
		var options = SelectOption.FromEnum<EmptyEnum>().ToList();

		// Assert
		options.Should().BeEmpty();
	}

	/// <summary>
	/// Test enum for FromEnum tests.
	/// </summary>
	public enum TestEnum
	{
		Option1,
		Option2,
		Option3
	}

	/// <summary>
	/// Empty enum for edge case testing.
	/// </summary>
	public enum EmptyEnum
	{
		// No values
	}
}