// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// The IResponsiveOptions class is the top level container for all configuration settings of Responsive.
    /// </summary>
    public interface IResponsiveOptions
    {
        /// <summary>
        /// Get or set the option to configure the default view.
        /// </summary>
        IResponsiveViewOptions View { get; set; }
    }

    public interface IResponsiveViewOptions
    {
        DeviceType DefaultMobile { get; set; }
        DeviceType DefaultTablet { get; set; }
        DeviceType DefaultDesktop { get; set; }
    }
}
