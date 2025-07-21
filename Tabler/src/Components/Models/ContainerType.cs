// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the types of Bootstrap/Tabler container layouts.
/// Maps to standard Bootstrap container classes for responsive design.
/// </summary>
public enum ContainerType
{
	/// <summary>
	/// Default responsive container that adapts to viewport size.
	/// Maps to CSS class: container
	/// </summary>
	Default = 0,

	/// <summary>
	/// Fluid container that spans 100% width at all breakpoints.
	/// Maps to CSS class: container-fluid
	/// </summary>
	Fluid = 1,

	/// <summary>
	/// Container with max-width for small devices and up (≥576px).
	/// Maps to CSS class: container-sm
	/// </summary>
	Small = 2,

	/// <summary>
	/// Container with max-width for medium devices and up (≥768px).
	/// Maps to CSS class: container-md
	/// </summary>
	Medium = 3,

	/// <summary>
	/// Container with max-width for large devices and up (≥992px).
	/// Maps to CSS class: container-lg
	/// </summary>
	Large = 4,

	/// <summary>
	/// Container with max-width for extra large devices and up (≥1200px).
	/// Maps to CSS class: container-xl
	/// </summary>
	ExtraLarge = 5,

	/// <summary>
	/// Container with max-width for extra extra large devices and up (≥1400px).
	/// Maps to CSS class: container-xxl
	/// </summary>
	ExtraExtraLarge = 6
}