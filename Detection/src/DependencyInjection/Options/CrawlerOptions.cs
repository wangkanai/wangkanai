// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Detection;

/// <summary>
///     The <see cref="CrawlerOptions" /> class is the Crawler container for all the configuration settings of Crawler
///     Resolver.
/// </summary>
public sealed class CrawlerOptions
{
	/// <summary>
	///     Gets a list of crawlers name you would like to add to this application.
	/// </summary>
	public List<string> Others { get; } = new();
}
