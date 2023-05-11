// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Collections;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.System.Extensions;

namespace Wangkanai.Detection.Services;

public sealed class DeviceService : IDeviceService
{
    private readonly IUserAgentService _userAgentService;

    private Device? _type;

    public DeviceService(IUserAgentService userAgentService)
    {
        _userAgentService = userAgentService;
    }

    public Device Type => _type ??= DeviceFromUserAgent();

    private Device DeviceFromUserAgent()
    {
        var agent = _userAgentService.UserAgent.ToLower();

        if (IsTablet(agent))
            return Device.Tablet;
        if (IsTV(agent))
            return Device.Tv;
        if (IsMobile(agent))
            return Device.Mobile;
        if (agent.Contains(Device.Watch))
            return Device.Watch;
        if (agent.Contains(Device.Console))
            return Device.Console;
        if (agent.Contains(Device.Car))
            return Device.Car;
        if (agent.Contains(Device.IoT))
            return Device.IoT;

        return Device.Desktop;
    }

    private static bool IsTablet(string agent)
    {
        return agent.SearchContains(TabletCollection.KeywordsSearchTree);
    }

    private static bool IsMobile(string agent)
    {
        return agent.Length >= 4 && agent.SearchStartsWith(MobileCollection.PrefixesSearchTree) ||
               agent.SearchContains(MobileCollection.KeywordsSearchTree);
    }

    private static bool IsTV(string agent)
    {
        return agent.Contains(Device.Tv) || agent.Contains("bravia", StringComparison.Ordinal);
    }
}