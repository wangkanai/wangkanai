// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

public class DetectionService : IDetectionService
{
    public UserAgent UserAgent { get; }
    public IDeviceService Device { get; }
    public ICrawlerService Crawler { get; }
    public IBrowserService Browser { get; }
    public IEngineService Engine { get; }
    public IPlatformService Platform { get; }

    public DetectionService(
        IUserAgentService userAgentService,
        IDeviceService device,
        ICrawlerService crawler,
        IBrowserService browser,
        IEngineService engine,
        IPlatformService platform)
    {
        UserAgent = userAgentService.UserAgent;
        Device = device;
        Crawler = crawler;
        Browser = browser;
        Engine = engine;
        Platform = platform;
    }
}