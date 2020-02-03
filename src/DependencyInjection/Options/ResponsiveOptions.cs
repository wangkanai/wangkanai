// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.DependencyInjection.Options
{
    /// <summary>
    /// The <see cref="ResponsiveOptions"/> class is the View container for all configuration settings of Responsive.
    /// </summary>
    public class ResponsiveOptions
    {
        /// <summary>
        /// Gets or sets a value that determine the default view for Mobile
        /// </summary>
        public Device DefaultMobile { get; set; } = Device.Mobile;

        /// <summary>
        /// Gets or sets a value that determine the default view for Tablet
        /// </summary>
        public Device DefaultTablet { get; set; } = Device.Tablet;

        /// <summary>
        /// Gets or sets a value that determine the default view for Desktop
        /// </summary>
        public Device DefaultDesktop { get; set; } = Device.Desktop;

        /// <summary>
        /// Gets or sets a value that determine the responsive middleware to include Web Api Endpoint also.
        /// </summary>
        /// <remarks><b>false</b> by default</remarks>
        public bool IncludeWebApi { get; set; } = false;

        /// <summary>
        /// Gets or sets a value that determine the responsive middleware is totally disable.
        /// </summary>
        /// <remarks><b>false</b> by default</remarks>
        public bool Disable { get; set; } = false;
        
        /// <summary>
        /// Gets or sets a value that determine the default Web Api convention path.
        /// </summary>
        public string PathWebApi { get; set; } = "/api";

        /// <summary>
        /// Gets or sets a value that determine the default Views path of your web application.
        /// </summary>
        public string PathViews { get; set; } = "/Views";
        
        /// <summary>
        /// Gets or sets a value that determine the default Pages path of your web application.
        /// </summary>
        public string PathPages { get; set; } = "/Pages";
    }
}