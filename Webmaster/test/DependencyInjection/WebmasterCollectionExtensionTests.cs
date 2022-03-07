// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Wangkanai.Webmaster;

public class WebmasterCollectionExtensionTests
{
    [Fact]
    public void AddRequiredPlatformServices_ReturnsExpected()
    {
        var services = new ServiceCollection();
        var builder  = services.AddWebmasterBuilder();
    }
}