// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.DependencyInjection.Options
{
    public class ResponsiveOptions
    {
        // Cause circular dependency if add Device as reference
        // Need another way to configure this options

        //public Device DefaultMobile { get; set; } = Device.Mobile;
        //public Device DefaultTablet { get; set; } = Device.Tablet;
        //public Device DefaultDesktop { get; set; } = Device.Desktop;
    }
}
