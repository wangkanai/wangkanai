// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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