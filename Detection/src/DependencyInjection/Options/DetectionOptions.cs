// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection;

/// <summary>
///     Provides programmatic configuration for the Detection library.
/// </summary>
public sealed class DetectionOptions
{
	/// <summary>
	///     Gets the default <see cref="CrawlerOptions" /> used by this application.
	/// </summary>
	public CrawlerOptions Crawler { get; } = new();
}
