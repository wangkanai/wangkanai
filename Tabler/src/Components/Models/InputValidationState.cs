// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the validation states for form input components.
/// Used to indicate the current validation status and apply appropriate styling.
/// </summary>
public enum InputValidationState
{
	/// <summary>
	/// Default state with no validation feedback.
	/// No special styling is applied.
	/// </summary>
	None = 0,

	/// <summary>
	/// Valid state indicating successful validation.
	/// Maps to CSS class: is-valid
	/// Displays success styling and feedback.
	/// </summary>
	Valid = 1,

	/// <summary>
	/// Invalid state indicating validation failure.
	/// Maps to CSS class: is-invalid
	/// Displays error styling and feedback messages.
	/// </summary>
	Invalid = 2,

	/// <summary>
	/// Warning state for non-critical validation issues.
	/// Maps to CSS class: is-warning (custom Tabler extension)
	/// Displays warning styling and feedback.
	/// </summary>
	Warning = 3
}