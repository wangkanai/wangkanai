// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions;

internal static class HttpContextExtensions
{
    private const string ResponsiveContextKey = "Responsive";

    public static ISession? SafeSession(this HttpContext httpContext)
        => httpContext.Features.Get<ISessionFeature?>() == null
               ? null
               : httpContext.Session;

    public static void SetDevice(this HttpContext context, Device device)
        => context.Items[ResponsiveContextKey] = device;

    public static Device GetDevice(this HttpContext context)
    {
        Check.NotNull(context);
        Check.NotNull(context.Items);

        return context.Items.TryGetValue(ResponsiveContextKey, out var responsive)
                   ? (Device)(responsive ?? Device.Unknown)
                   : Device.Desktop;
    }
}