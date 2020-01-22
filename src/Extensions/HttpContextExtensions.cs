// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Models;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        private const string ResponsiveContextKey = "Responsive";

        public static bool IsNull(this HttpContext context)
            => context == null;

        public static void SetDevice(this HttpContext context, Device device)
            => context.Items[ResponsiveContextKey] = device;

        public static Device GetDevice(this HttpContext context)
        {
            if (context is null)
                throw new GetDeviceArgumentNullException(nameof(context));

            if (context.Items.TryGetValue(ResponsiveContextKey, out var responsiveContext))
                return (Device) responsiveContext;

            return Device.Desktop;
        }
    }
}
