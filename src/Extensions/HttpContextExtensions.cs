// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Models;

namespace Microsoft.AspNetCore.Http
{
    internal static class HttpContextExtensions
    {
        private const string ResponsiveContextKey = "Responsive";
        private const string PreferenceContextKey = "Preference";
        private const string MarkContextKey       = "Mark";

        public static void SetDevice(this HttpContext context, Device device)
            => context.Items[ResponsiveContextKey] = device;

        public static Device GetDevice(this HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (context.Items is null)
                throw new ArgumentNullException(nameof(context.Items));

            return context.Items.TryGetValue(ResponsiveContextKey, out var responsive)
                       ? (Device) responsive
                       : Device.Desktop;
        }
    }
}