// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the style variants for Tabler button components.
/// Controls the visual appearance of buttons (solid, outline, ghost).
/// </summary>
public enum ButtonVariant
{
	/// <summary>
	/// Solid button style (default Tabler button appearance)
	/// </summary>
	Solid = 0,
	
	/// <summary>
	/// Outline button style (btn-outline-*)
	/// </summary>
	Outline,
	
	/// <summary>
	/// Ghost button style (btn-ghost-*)
	/// </summary>
	Ghost
}