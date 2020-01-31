// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.DependencyInjection.Options
{
    /// <summary>
    /// Provides programmatic configuration for the Detection library.
    /// </summary>
    public class DetectionOptions
    {
        /// <summary>
        /// Creates a new instamce of <see cref="DetectionOptions"/>
        /// </summary>
        public DetectionOptions()
        {
            Crawler = new CrawlerOptions();
            Responsive = new ResponsiveOptions();
        }
        
        /// <summary>
        /// Gets the default <see cref="CrawlerOptions"/> used by this application.
        /// </summary>
        public CrawlerOptions Crawler { get; }
        
        /// <summary>
        /// Gets the default <see cref="ResponsiveOptions"/> used by this application.
        /// </summary>
        public ResponsiveOptions Responsive { get; }
    }
}
