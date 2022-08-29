// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text;

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Services;

public class ResponsiveService : IResponsiveService
{
    public Device View => PreferView(_context, _defaultView);

    private readonly HttpContext _context;
    private readonly Device _defaultView;
    private const string ResponsiveContextKey = "Responsive";

    public ResponsiveService(IHttpContextAccessor accessor, IDeviceService deviceService, ResponsiveOptions? options)
    {
        if (accessor is null)
            throw new ArgumentNullException(nameof(accessor));
        if (accessor.HttpContext is null)
            throw new ArgumentNullException(nameof(accessor.HttpContext));
        if (deviceService is null)
            throw new ArgumentNullException(nameof(deviceService));

        options ??= new ResponsiveOptions();

        _context = accessor.HttpContext;
        _defaultView = DefaultView(deviceService.Type, options);
    }

    public void PreferSet(Device desktop)
        => _context.Session.SetString(ResponsiveContextKey, desktop.ToString());

    public void PreferClear()
        => _context.Session.Remove(ResponsiveContextKey);

    public bool HasPreferred()
        => _context.SafeSession() != null
           && _context.Session.Keys.Any(k => k == ResponsiveContextKey);

    private static Device PreferView(HttpContext context, Device defaultView)
    {
        if (context.SafeSession() is null)
            return defaultView;

        context.Session.TryGetValue(ResponsiveContextKey, out var raw);

        if (raw == null)
            return defaultView;

        Enum.TryParse<Device>(Encoding.ASCII.GetString(raw), out var preferView);

        if (preferView != Device.Unknown && preferView != defaultView)
            return preferView;

        return defaultView;
    }

    private static Device DefaultView(Device device, ResponsiveOptions options)
        => device switch
        {
            Device.Mobile => options.DefaultMobile,
            Device.Tablet => options.DefaultTablet,
            Device.Desktop => options.DefaultDesktop,
            _ => device
        };
}