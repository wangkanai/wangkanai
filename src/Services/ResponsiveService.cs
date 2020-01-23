// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class ResponsiveService : IResponsiveService
    {
        public Device View { get; }

        public ResponsiveService(IDeviceService deviceService, IUserPreferenceService preferenceService, DetectionOptions options)
        {
            if (options == null)
                options = new DetectionOptions();
            View = deviceService != null
                ? GetView(deviceService.Type, options?.Responsive)
                : Device.Desktop;
            if (preferenceService != null && preferenceService.Preferred != View)
                View = preferenceService.Preferred;
        }

        private static Device GetView(Device device, ResponsiveOptions options)
            => device switch
            {
                Device.Mobile  => options.DefaultMobile,
                Device.Tablet  => options.DefaultTablet,
                Device.Desktop => options.DefaultDesktop,
                _              => device
            };
    }
}