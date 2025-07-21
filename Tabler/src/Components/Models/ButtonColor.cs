// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the color variants for Tabler button components.
/// Maps to Tabler CSS button color classes.
/// </summary>
public enum ButtonColor
{
	/// <summary>
	/// Default button color (no specific color class applied)
	/// </summary>
	None = 0,
	
	/// <summary>
	/// Primary button color (btn-primary)
	/// </summary>
	Primary,
	
	/// <summary>
	/// Secondary button color (btn-secondary)
	/// </summary>
	Secondary,
	
	/// <summary>
	/// Success button color (btn-success)
	/// </summary>
	Success,
	
	/// <summary>
	/// Warning button color (btn-warning)
	/// </summary>
	Warning,
	
	/// <summary>
	/// Danger button color (btn-danger)
	/// </summary>
	Danger,
	
	/// <summary>
	/// Info button color (btn-info)
	/// </summary>
	Info,
	
	/// <summary>
	/// Light button color (btn-light)
	/// </summary>
	Light,
	
	/// <summary>
	/// Dark button color (btn-dark)
	/// </summary>
	Dark,
	
	/// <summary>
	/// Ghost button variant (btn-ghost-primary, etc.)
	/// </summary>
	Ghost,
	
	/// <summary>
	/// Outline button variant (btn-outline-primary, etc.)
	/// </summary>
	Outline
}