// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// Provides programmatic configuration for the Responsive framework.
    /// </summary>
    //[Obsolete("This is an experimental API, its might change when finalize.")]
    public class ResponsiveOptions : IResponsiveOptions
    {
        public IResponsiveViewOptions View { get; set; } = new ResponsiveViewOptions();

        public ResponsiveOptions() { }

        #region Obsolete
        [Obsolete("This will be refactor to add service of responsive in 2.0-beta-14. Use AddResponsive(options => {}) instead.")]
        public DeviceType MobileDefault { get; set; } = DeviceType.Mobile;
        [Obsolete("This will be refactor to add service of responsive in 2.0-beta-14. Use AddResponsive(options => {}) instead.")]
        public DeviceType TabletDefault { get; set; } = DeviceType.Tablet;
        [Obsolete("This will be refactor to add service of responsive in 2.0-beta-14. Use AddResponsive(options => {}) instead.")]
        public DeviceType DesktopDefault { get; set; } = DeviceType.Desktop;
        [Obsolete("This will be refactor to add service of responsive in 2.0-beta-14. Use AddResponsive(options => {}) instead.")]
        public ResponsiveOptions(DeviceType desktop, DeviceType tablet, DeviceType mobile)
        {
            DesktopDefault = desktop;
            TabletDefault = tablet;
            MobileDefault = mobile;
        }
        [Obsolete("This will be refactor to add service of responsive in 2.0-beta-14. Use AddResponsive(options => {}) instead.")]
        public DeviceType Default(DeviceType type)
        {
            if (type == DeviceType.Mobile) return MobileDefault;
            if (type == DeviceType.Tablet) return TabletDefault;
            if (type == DeviceType.Desktop) return DesktopDefault;

            return type;
        }
        #endregion Obsolete
    }

    public class ResponsiveViewOptions : IResponsiveViewOptions
    {
        public DeviceType DefaultMobile { get; set; } = DeviceType.Mobile;
        public DeviceType DefaultTablet { get; set; } = DeviceType.Tablet;
        public DeviceType DefaultDesktop { get; set; } = DeviceType.Desktop;
    }
}
