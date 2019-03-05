// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;
using Wangkanai.Responsive;

namespace Microsoft.AspNetCore.Http
{
    public static class DeviceHttpContextExtensions
    {
        private const string ResponsiveContextKey = "Responsive";

        public static void SetDevice(this HttpContext context, UserPerference perference)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (perference == null) throw new ArgumentNullException(nameof(perference));

            context.Items[ResponsiveContextKey] = perference;
        }
        public static UserPerference GetDevice(this HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            object responsiveContext;
            if (context.Items.TryGetValue(ResponsiveContextKey, out responsiveContext))
                return responsiveContext as UserPerference;

            return new UserPerference(DeviceType.Desktop, DeviceType.Desktop);
        }
    }
}
