// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    public static class DeviceHttpContextExtensions
    {
        private const string ResponsiveContextKey = "Responsive";

        public static void SetDevice(this HttpContext context, DeviceType device)
        {
            context.Items[ResponsiveContextKey] = device;
        }
        public static DeviceType GetDevice(this HttpContext context)
        {
            if (context == null) throw new GetDeviceArgumentNullException(nameof(context));

            object responsiveContext;
            if (context.Items.TryGetValue(ResponsiveContextKey, out responsiveContext))
                return (DeviceType)responsiveContext;

            return DeviceType.Desktop;
        }
    }
}
