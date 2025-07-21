// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Tabler.Models;

/// <summary>
/// Defines the layout patterns for TablerPage component.
/// Different layout types provide various page structures for different use cases.
/// </summary>
public enum PageLayout
{
	/// <summary>
	/// Standard page layout with header, main content area, and optional footer.
	/// Uses responsive container and follows typical web page structure.
	/// Maps to: page-wrapper with page-body and container
	/// </summary>
	Default = 0,

	/// <summary>
	/// Full-height layout that fills the entire viewport height.
	/// Useful for dashboards and applications requiring maximum vertical space.
	/// Maps to: page-wrapper with d-flex flex-column and flex-fill
	/// </summary>
	FullHeight = 1,

	/// <summary>
	/// Centered layout for login pages, error pages, and modal-like content.
	/// Centers content both horizontally and vertically on the page.
	/// Maps to: page page-center without container wrapper
	/// </summary>
	Centered = 2,

	/// <summary>
	/// Minimal layout with no containers or standard page structure.
	/// Provides maximum flexibility for custom layouts and landing pages.
	/// Maps to: page-minimal with minimal styling
	/// </summary>
	Minimal = 3
}