// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the layout variants for checkbox components.
/// Maps to Bootstrap form check layout classes.
/// </summary>
public enum CheckboxLayout
{
	/// <summary>
	/// Default checkbox layout with label to the right.
	/// Uses standard form-check class structure.
	/// </summary>
	Default = 0,

	/// <summary>
	/// Inline checkbox layout for horizontal arrangements.
	/// Maps to CSS class: form-check-inline
	/// </summary>
	Inline = 1,

	/// <summary>
	/// Switch-style checkbox layout.
	/// Maps to CSS class: form-switch
	/// </summary>
	Switch = 2,

	/// <summary>
	/// Reverse checkbox layout with label to the left.
	/// Maps to CSS class: form-check-reverse
	/// </summary>
	Reverse = 3
}