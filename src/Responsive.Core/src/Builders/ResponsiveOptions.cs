// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// Provides programmatic configuration for the Responsive framework.
    /// </summary>
    public class ResponsiveOptions
    {
        /// <summary>
        /// Get or set the option to configure the default view.
        /// </summary>
        public ResponsiveViewOptions View { get; set; } = new ResponsiveViewOptions();
    }

    /// <summary>
    /// The IResponsiveViewOptions class is the View container for all configuration settings of Responsive.
    /// </summary>
    public class ResponsiveViewOptions
    {
        /// <summary>
        /// Gets or sets a value that determines the default view for Mobile
        /// </summary>
        public DeviceType DefaultMobile { get; set; } = DeviceType.Mobile;
        /// <summary>
        /// Gets or sets a value that determines the default view for Tablet
        /// </summary>
        public DeviceType DefaultTablet { get; set; } = DeviceType.Tablet;
        /// <summary>
        /// Gets or sets a value that determines the default view for Desktop
        /// </summary>
        public DeviceType DefaultDesktop { get; set; } = DeviceType.Desktop;
    }
}
