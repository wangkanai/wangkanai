// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// The IResponsiveViewOptions class is the View container for all configuration settings of Responsive.
    /// </summary>
    public interface IResponsiveViewOptions
    {
        DeviceType DefaultMobile { get; set; }
        DeviceType DefaultTablet { get; set; }
        DeviceType DefaultDesktop { get; set; }
    }
}
