// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the size variants for form input components.
/// Maps to Bootstrap form control sizing classes.
/// </summary>
public enum InputSize
{
	/// <summary>
	/// Default size for form controls.
	/// No additional CSS class is applied.
	/// </summary>
	Default = 0,

	/// <summary>
	/// Small size for compact form controls.
	/// Maps to CSS class: form-control-sm
	/// </summary>
	Small = 1,

	/// <summary>
	/// Large size for prominent form controls.
	/// Maps to CSS class: form-control-lg
	/// </summary>
	Large = 2
}