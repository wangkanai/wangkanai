// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    //[Obsolete("This is an experimental API, its might change when finalize.")]
    public class ResponsiveOptions : IResponsiveOptions
    {
        public DeviceType DefaultMobile { get; set; } = DeviceType.Mobile;
        public DeviceType DefaultTablet { get; set; } = DeviceType.Tablet;
        public DeviceType DefaultDesktop { get; set; } = DeviceType.Desktop;
        public IResponsiveViewOptions View { get; set; } = new ResponsiveViewOptions();

        public ResponsiveOptions() { }

        #region Obsolete
        public DeviceType MobileDefault { get; set; } = DeviceType.Mobile;
        public DeviceType TabletDefault { get; set; } = DeviceType.Tablet;
        public DeviceType DesktopDefault { get; set; } = DeviceType.Desktop;

        public ResponsiveOptions(DeviceType desktop, DeviceType tablet, DeviceType mobile)
        {
            DesktopDefault = desktop;
            TabletDefault = tablet;
            MobileDefault = mobile;
        }

        public DeviceType Default(DeviceType type)
        {
            if (type == DeviceType.Mobile) return MobileDefault;
            if (type == DeviceType.Tablet) return TabletDefault;
            if (type == DeviceType.Desktop) return DesktopDefault;

            return type;
        }
        #endregion
    }
}
