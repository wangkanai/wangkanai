// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public class ResponsiveViewOptions : IResponsiveViewOptions
    {
        public DeviceType DefaultMobile { get; set; }
        public DeviceType DefaultTablet { get; set; }
        public DeviceType DefaultDesktop { get; set; }
    }
}
