// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection;

/// <summary>
/// The <see cref="CrawlerOptions"/> class is the Crawler container for all the configuration settings of Crawler Resolver.
/// </summary>
public class CrawlerOptions
{
    /// <summary>
    /// Gets a list of crawlers name you would like to add to this application. 
    /// </summary>
    public List<string> Others { get; } = new List<string>();
}