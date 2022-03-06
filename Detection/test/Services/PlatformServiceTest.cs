// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;

using Xunit.Abstractions;

namespace Wangkanai.Detection.Services;

public class PlatformServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PlatformServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Null()
    {
        var resolver = MockService.PlatformService((string)null!);
        Assert.NotNull(resolver);
        Assert.Equal(Platform.Unknown, resolver.Name);
        Assert.Equal(Processor.Others, resolver.Processor);
    }

    [Fact]
    public void OutOfRange()
    {
        var resolver = MockService.PlatformService(string.Empty!);
        Assert.NotNull(resolver);
        Assert.Equal(Platform.Unknown, resolver.Name);
        Assert.Equal(Processor.Others, resolver.Processor);
    }

    [Theory]
    [InlineData("axios/0.19.2")]
    public void Bots(string agent)
    {
        var resolver = MockService.PlatformService(agent);
        Assert.NotNull(resolver);
        Assert.Equal(Platform.Others, resolver.Name);
        Assert.Equal(Processor.Others, resolver.Processor);
    }

    [Theory]
    [InlineData(Processor.x64, "Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData(Processor.x64, "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko")]
    [InlineData(Processor.x86, "Mozilla/5.0 (Windows x86; rv:19.0) Gecko/20100101 Firefox/19.0")]
    [InlineData(Processor.ARM, "Mozilla/5.0 (Windows NT 10.0; ARM; RM-1096) AppleWebKit/537.36 (KHTML like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393")]
    public void Windows(Processor processor, string agent)
    {
        var os       = Platform.Windows;
        var resolver = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
    }

    [Theory]
    [InlineData("Mozilla/5.0 (Android 4.4; Mobile; rv:41.0) Gecko/41.0 Firefox/41.0")]
    [InlineData("Mozilla/5.0 (Linux; Android 7.0; SM-T585 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36")]
    [InlineData("Mozilla/5.0 (Linux; Android 4.4.2); Nexus 5 Build/KOT49H) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Mobile Safari/537.36 OPR/20.0.1396.72047")]
    public void Android(string agent)
    {
        var os        = Platform.Android;
        var processor = Processor.ARM;
        var resolver  = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
    }

    [Theory]
    [InlineData("8.3", "Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12F69 Safari/600.1.4")]
    [InlineData("8.3", "Mozilla/5.0 -(iPod touch; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12F69 Safari/600.1.4")]
    [InlineData("14.7.1", "Mozilla/5.0 (iPhone; CPU iPhone OS 14_7_1 like Mac OS X) WebKit/8611 (KHTML, like Gecko) Mobile/18G82 [FBAN/FBIOS;FBDV/iPhone12,3;FBMD/iPhone;FBSN/iOS;FBSV/14.7.1;FBSS/3;FBID/phone;FBLC/en_GB;FBOP/5]")]
    public void iOS(string target, string agent)
    {
        var os        = Platform.iOS;
        var processor = Processor.ARM;
        var version   = new Version(target);
        var resolver  = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
        Assert.Equal(version, resolver.Version);
        _testOutputHelper.WriteLine(resolver.Version.ToString());
    }

    [Theory]
    [InlineData("4.3.5", "Mozilla/5.0 (iPad; U; CPU OS 4_3_5 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8L1 Safari/6533.18.5")]
    [InlineData("12.4.2", "Mozilla/5.0 (iPad; CPU OS 12_4_2 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.1.2 Mobile/15E148 Safari/604.1")]
    public void iPadOS(string target, string agent)
    {
        var os        = Platform.iPadOS;
        var processor = Processor.ARM;
        var version   = new Version(target);
        var resolver  = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
        Assert.Equal(version, resolver.Version);
        _testOutputHelper.WriteLine(resolver.Version.ToString());
    }

    [Theory]
    [InlineData(Processor.x64, "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0 Safari/605.1.15")]
    [InlineData(Processor.x64, "Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData(Processor.Others, "Mozilla/5.0 (Macintosh; PPC Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
    public void Mac(Processor processor, string agent)
    {
        var os       = Platform.Mac;
        var resolver = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
    }

    [Theory]
    [InlineData(Processor.x64, "Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.2.22) Gecko/20110904 Red Hat/3.6-1.el5_7 Firefox/3.6.22")]
    [InlineData(Processor.x86, "Mozilla/5.0 (X11; Red Hat Enterprise; Linux i686; rv:41.0) Gecko/20100101 Firefox/41.0")]
    [InlineData(Processor.ARM, "Mozilla/5.0 (Linux arm) Gecko/20110318 Firefox/4.0b13pre Fennec/4.0")]
    public void Linux(Processor processor, string agent)
    {
        var os       = Platform.Linux;
        var resolver = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
    }

    [Theory]
    [InlineData(Processor.x64, "Mozilla/5.0 (X11; CrOS x86_64 14092.77.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.107 Safari/537.36")]
    public void ChromeOS(Processor processor, string agent)
    {
        var os       = Platform.ChromeOS;
        var resolver = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
    }

    [Theory]
    [InlineData(Processor.x86, "Mozilla/4.79 [en] (X11; U; SunOS 5.10 i86pc)")]
    [InlineData(Processor.Others, "Mozilla/5.0 (X11; U; SunOS sun4u; en-US; rv:1.8.1.11) Gecko/20080118 Firefox/2.0.0.11")]
    public void Others(Processor processor, string agent)
    {
        var os       = Platform.Others;
        var resolver = MockService.PlatformService(agent);
        Assert.Equal(os, resolver.Name);
        Assert.Equal(processor, resolver.Processor);
    }

    [Theory]
    [InlineData("1.9.2.17", "Mozilla / 5.0(X11; U; Linux x86_64; en - US; rv: 1.9.2.17) Gecko/20110428 Fedora/3.6.17-1.fc13 Firefox/3.6.17")]                                       // Linux
    [InlineData("5.0.2", "Mozilla / 5.0(Linux; Android 5.0.2; SGH - T679 Build / LRX22G) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/37.0.0.0 Mobile Safari/537.36")] // Android
    [InlineData("5.1", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.7pre) Gecko/20070815 Firefox/2.0.0.6 Navigator/9.0b3")]                                            // XP
    [InlineData("6.1", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2302.0 Safari/537.36")]                                         // Vista
    [InlineData("8.4.1", "Mozilla/5.0 (iPhone; CPU iPhone OS 8_4_1 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12H321 Safari/600.1.4")]                 // iOS
    [InlineData("10.0", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; Touch; LCJB; rv:11.0) like Gecko")]                                                                      // Win10
    [InlineData("10.9.3", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A")]                               // OSX
    [InlineData("0.0", "iphone applewebkit")]
    [InlineData("0.0", "ipad applewebkit")]
    [InlineData("0.0", "android")]
    [InlineData("0.0", "windows")]
    [InlineData("0.0", "mac")]
    [InlineData("0.0", "linux")]
    [InlineData("0.0", "random")]
    [InlineData("0.0", "")] // Other
    public void GetVersion(string version, string agent)
    {
        var resolver = MockService.PlatformService(agent);
        Assert.Equal(new Version(version), resolver.Version);
    }
}