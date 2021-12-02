// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection;

/// <summary>
/// Provides programmatic configuration for the Detection library.
/// </summary>
public class DetectionOptions
{
    /// <summary>
    /// Gets the default <see cref="CrawlerOptions"/> used by this application.
    /// </summary>
    public CrawlerOptions Crawler { get; } = new CrawlerOptions();
        
    /// <summary>
    /// Gets the default <see cref="ResponsiveOptions"/> used by this application.
    /// </summary>
    public ResponsiveOptions Responsive { get; } = new ResponsiveOptions();
}