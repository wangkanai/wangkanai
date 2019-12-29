// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// The IResponsiveViewOptions class is the View container for all configuration settings of Responsive.
    /// </summary>
    public class ViewLocationOptions
    {
        /// <summary>
        /// Gets or sets a value that determines the default view for Mobile
        /// </summary>
        public Device DefaultMobile { get; set; } = Device.Mobile;
        /// <summary>
        /// Gets or sets a value that determines the default view for Tablet
        /// </summary>
        public Device DefaultTablet { get; set; } = Device.Tablet;
        /// <summary>
        /// Gets or sets a value that determines the default view for Desktop
        /// </summary>
        public Device DefaultDesktop { get; set; } = Device.Desktop;
    }
}
