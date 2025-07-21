// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Represents an option in a select dropdown component.
/// Provides value, display text, and state properties for dropdown options.
/// </summary>
public class SelectOption
{
	/// <summary>
	/// Initializes a new instance of the SelectOption class.
	/// </summary>
	public SelectOption()
	{
		Value = string.Empty;
		Text = string.Empty;
	}

	/// <summary>
	/// Initializes a new instance of the SelectOption class with specified value and text.
	/// </summary>
	/// <param name="value">The option value</param>
	/// <param name="text">The display text</param>
	public SelectOption(string value, string text)
	{
		Value = value;
		Text = text;
	}

	/// <summary>
	/// Initializes a new instance of the SelectOption class with specified value, text, and disabled state.
	/// </summary>
	/// <param name="value">The option value</param>
	/// <param name="text">The display text</param>
	/// <param name="disabled">Whether the option is disabled</param>
	public SelectOption(string value, string text, bool disabled)
	{
		Value = value;
		Text = text;
		Disabled = disabled;
	}

	/// <summary>
	/// The value of the option that will be submitted with the form.
	/// This is the actual data value associated with this option.
	/// </summary>
	public string Value { get; set; }

	/// <summary>
	/// The display text shown to the user for this option.
	/// This is what users see in the dropdown list.
	/// </summary>
	public string Text { get; set; }

	/// <summary>
	/// Whether this option is disabled and cannot be selected.
	/// Disabled options are typically shown grayed out.
	/// </summary>
	public bool Disabled { get; set; }

	/// <summary>
	/// Optional group name for organizing options into groups.
	/// Options with the same group name will be grouped together.
	/// </summary>
	public string? Group { get; set; }

	/// <summary>
	/// Creates a new SelectOption with the specified value and text.
	/// </summary>
	/// <param name="value">The option value</param>
	/// <param name="text">The display text</param>
	/// <returns>A new SelectOption instance</returns>
	public static SelectOption Create(string value, string text)
	{
		return new SelectOption(value, text);
	}

	/// <summary>
	/// Creates a new disabled SelectOption with the specified value and text.
	/// </summary>
	/// <param name="value">The option value</param>
	/// <param name="text">The display text</param>
	/// <returns>A new disabled SelectOption instance</returns>
	public static SelectOption CreateDisabled(string value, string text)
	{
		return new SelectOption(value, text, true);
	}

	/// <summary>
	/// Creates a collection of SelectOptions from a dictionary.
	/// </summary>
	/// <param name="items">Dictionary where keys are values and values are display text</param>
	/// <returns>Collection of SelectOption instances</returns>
	public static IEnumerable<SelectOption> FromDictionary(IDictionary<string, string> items)
	{
		return items.Select(kvp => new SelectOption(kvp.Key, kvp.Value));
	}

	/// <summary>
	/// Creates a collection of SelectOptions from an enum type.
	/// </summary>
	/// <typeparam name="TEnum">The enum type</typeparam>
	/// <returns>Collection of SelectOption instances</returns>
	public static IEnumerable<SelectOption> FromEnum<TEnum>() where TEnum : struct, Enum
	{
		return Enum.GetValues<TEnum>()
			.Select(value => new SelectOption(value.ToString(), value.ToString()));
	}

	/// <summary>
	/// Creates a collection of SelectOptions from an enum type with custom text formatting.
	/// </summary>
	/// <typeparam name="TEnum">The enum type</typeparam>
	/// <param name="textFormatter">Function to format the display text</param>
	/// <returns>Collection of SelectOption instances</returns>
	public static IEnumerable<SelectOption> FromEnum<TEnum>(Func<TEnum, string> textFormatter) where TEnum : struct, Enum
	{
		return Enum.GetValues<TEnum>()
			.Select(value => new SelectOption(value.ToString(), textFormatter(value)));
	}

	/// <summary>
	/// Returns a string representation of this SelectOption.
	/// </summary>
	/// <returns>String in format "Value: Text"</returns>
	public override string ToString()
	{
		return $"{Value}: {Text}";
	}

	/// <summary>
	/// Determines whether the specified object is equal to the current object.
	/// </summary>
	/// <param name="obj">The object to compare</param>
	/// <returns>True if the objects are equal</returns>
	public override bool Equals(object? obj)
	{
		if (obj is SelectOption other)
		{
			return string.Equals(Value, other.Value, StringComparison.Ordinal) &&
			       string.Equals(Text, other.Text, StringComparison.Ordinal) &&
			       Disabled == other.Disabled;
		}
		return false;
	}

	/// <summary>
	/// Returns a hash code for this instance.
	/// </summary>
	/// <returns>A hash code for this instance</returns>
	public override int GetHashCode()
	{
		return HashCode.Combine(Value, Text, Disabled);
	}
}