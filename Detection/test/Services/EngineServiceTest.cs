// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

public class EngineServiceTest
{
    [Fact]
    public void Null()
    {
        var resolver = MockService.EngineService((string)null!);
        Assert.NotNull(resolver);
        Assert.Equal(Engine.Unknown, resolver.Name);
    }

    [Theory]
    [InlineData(Engine.Trident, "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko")]
    [InlineData(Engine.Trident, "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)")]
    [InlineData(Engine.Trident, "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)")]
    [InlineData(Engine.Trident, "Mozilla/5.0 (IE 11.0; Windows NT 6.3; Trident/7.0; .NET4.0E; .NET4.0C; rv:11.0) like Gecko")]
    public void Trident(Engine engine, string agent)
    {
        var resolver = MockService.EngineService(agent);
        Assert.Equal(engine, resolver.Name);
    }


    [Theory]
    [InlineData(Engine.WebKit, "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0 Safari/605.1.15")]
    [InlineData(Engine.WebKit, "Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1")]
    [InlineData(Engine.WebKit, "Mozilla/5.0 (Macintosh; U; PPC Mac OS X; de-ch) AppleWebKit/85 (KHTML, like Gecko) Safari/85")]
    public void WebKit(Engine engine, string agent)
    {
        var resolver = MockService.EngineService(agent);
        Assert.Equal(engine, resolver.Name);
    }

    [Theory]
    [InlineData(Engine.Blink, "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.19 (KHTML, like Gecko) Chrome/1.0.154.53 Safari/525.19")]
    [InlineData(Engine.Blink, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.90 Atom/4.0.0.141 Safari/537.36")]
    [InlineData(Engine.Blink, "Mozilla/5.0 (X11; U; Linux x86_64; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Ubuntu/10.10 Chromium/8.0.552.237 Chrome/8.0.552.237 Safari/534.10")]
    public void Blink(Engine engine, string agent)
    {
        var resolver = MockService.EngineService(agent);
        Assert.Equal(engine, resolver.Name);
    }

    [Theory]
    [InlineData(Engine.Edge, "Mozilla/5.0 (Windows Mobile 10; Android 8.0.0; Microsoft; Lumia 950XL) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.116 Mobile Safari/537.36 Edge/40.15254.369")]
    [InlineData(Engine.Edge, "Mozilla/5.0 (Windows NT 10.0; Win64; x64; Xbox; Xbox One) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36 Edge/40.15063.0")]
    [InlineData(Engine.Edge, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.74 Safari/537.36 Edg/79.0.309.43")]
    [InlineData(Engine.Edge, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.9200")]
    public void Edge(Engine engine, string agent)
    {
        var resolver = MockService.EngineService(agent);
        Assert.Equal(engine, resolver.Name);
    }

    [Theory]
    [InlineData(Engine.Gecko, "Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData(Engine.Gecko, "Mozilla/5.0 (Windows NT 5.1; rv:11.0) Gecko Firefox/11.0 (via ggpht.com GoogleImageProxy)")]
    [InlineData(Engine.Gecko, "Mozilla/5.0 (Linux arm) Gecko/20110318 Firefox/4.0b13pre Fennec/4.0")]
    public void Gecko(Engine engine, string agent)
    {
        var resolver = MockService.EngineService(agent);
        Assert.Equal(engine, resolver.Name);
    }

    [Theory]
    [InlineData(Engine.Servo, "Mozilla/5.0 (Android; Mobile; rv:1.0) Servo/1.0 Firefox/36.0")]
    [InlineData(Engine.Servo, "Mozilla/5.0 (Mobile; rv:1.0) Servo/1.0 Firefox/36.0")]
    [InlineData(Engine.Servo, "Mozilla/5.0 (X11; Linux i686) Servo/1.0 Firefox/36.0")]
    public void Servo(Engine engine, string agent)
    {
        var resolver = MockService.EngineService(agent);
        Assert.Equal(engine, resolver.Name);
    }
}