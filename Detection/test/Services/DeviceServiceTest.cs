// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

public class DeviceServiceTest
{
    [Fact]
    public void UserAgentIsNull()
    {
        var resolver = MockService.DeviceService(null!);
        Assert.NotNull(resolver);
    }

    #region Tablet

    [Theory]
    [InlineData("Mozilla/5.0 (Android 4.4; Tablet; rv:41.0) Gecko/41.0 Firefox/41.0")]
    [InlineData("Mozilla/5.0 (Tablet; rv:26.0) Gecko/26.0 Firefox/26.0")]
    [InlineData("Mozilla/5.0 (iPad; U; CPU OS 4_3_5 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8L1 Safari/6533.18.5")]
    [InlineData("Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A406 Safari/8536.25")]
    [InlineData("Mozilla/5.0 (Linux; Android 7.0; SM-T585 Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36")]
    [InlineData("Mozilla/5.0 (Linux; Android 4.4.4; SM-T561 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.98 Safari/537.36")]
    [InlineData("Mozilla/5.0 (Linux; Android 5.1.1; KFAUWI) AppleWebKit/537.36 (KHTML, like Gecko) Silk/77.2.19 like Chrome/77.0.3865.92 Safari/537.36")]
    public void Tablet(string agent)
    {
        var resolver = MockService.DeviceService(agent);
        Assert.Equal(Device.Tablet, resolver.Type);
    }

    #endregion

    #region Mobile

    [Theory]
    [InlineData("Mozilla/5.0 (iPhone; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12F69 Safari/600.1.4")]
    [InlineData("Mozilla/5.0 (Linux; Android 4.4.2); Nexus 5 Build/KOT49H) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Mobile Safari/537.36 OPR/20.0.1396.72047")]
    [InlineData("Mozilla/5.0 (iPod touch; CPU iPhone OS 8_3 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) FxiOS/1.0 Mobile/12F69 Safari/600.1.4")]
    [InlineData("Mozilla/5.0 (Android 4.4; Mobile; rv:41.0) Gecko/41.0 Firefox/41.0")]
    [InlineData("Mozilla/5.0 (Maemo; Linux armv7l; rv:10.0.1) Gecko/20100101 Firefox/10.0.1 Fennec/10.0.1")]
    [InlineData("Mozilla/5.0 (Mobile; rv:26.0) Gecko/26.0 Firefox/26.0")]
    [InlineData("Mozilla/5.0 (Linux; Android 4.0.4; Galaxy Nexus Build/IMM76B) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.133 Mobile Safari/535.19")]
    [InlineData("Mozilla/5.0 (iPhone; U; CPU iPhone OS 5_1_1 like Mac OS X; en) AppleWebKit/534.46.0 (KHTML, like Gecko) CriOS/19.0.1084.60 Mobile/9B206 Safari/7534.48.3")]
    [InlineData("Mozilla/5.0 (iPhone; U; CPU like Mac OS X; en) AppleWebKit/420+ (KHTML, like Gecko) Version/3.0 Mobile/1A543 Safari/419.3")]
    public void MobileKeywords(string agent)
    {
        var resolver = MockService.DeviceService(agent);
        Assert.Equal(Device.Mobile, resolver.Type);
    }

    [Theory]
    [InlineData("EricssonT68/R101")]
    [InlineData("Nokia9210/2.0 Symbian-Crystal/6.1 Nokia/2.1")]
    [InlineData("SAMSUNG-SGH-R220/1.0 UP/4.1.19k")]
    [InlineData("SonyEricssonT68/R201A")]
    [InlineData("WinWAP 3.0 PRO")]
    public void MobilePrefix(string agent)
    {
        var resolver = MockService.DeviceService(agent);
        Assert.Equal(Device.Mobile, resolver.Type);
    }

    #endregion

    #region Desktop

    [Theory]
    [InlineData("Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData("Mozilla/5.0 (Windows NT x.y; Win64; x64; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData("Mozilla/5.0 (Windows NT x.y; WOW64; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36")]
    [InlineData("Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData("Mozilla/5.0 (Macintosh; PPC Mac OS X x.y; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData("Mozilla/5.0 (X11; Linux i686; rv:10.0) Gecko/20100101 Firefox/10.0")]
    [InlineData("Mozilla/5.0 (X11; Linux x86_64; rv:10.0) Gecko/20100101 Firefox/10.0")]
    public void Desktop(string agent)
    {
        var resolver = MockService.DeviceService(agent);
        Assert.Equal(Device.Desktop, resolver.Type);
    }

    #endregion

    #region TV

    [Theory]
    [InlineData("Mozilla/5.0 (SMART-TV; Linux; Tizen 2.3) AppleWebkit/538.1 (KHTML, like Gecko) SamsungBrowser/1.0 TV Safari/538.1")]
    [InlineData("AppleCoreMedia/1.0.0.12B466 (Apple TV; U; CPU OS 8_1_3 like Mac OS X; en_us)")]
    [InlineData("Mozilla/5.0 (Web0S; Linux/SmartTV) AppleWebKit/537.36 (KHTML, like Gecko) QtWebEngine/5.2.1 Chr0me/38.0.2125.122 Safari/537.36 LG BrowserService/8.00.00(LGE; 60UH6550-UB; 03.00.15; 1; DTV_W16N); webOS.TV-2016; LG NetCast.TV-2013 Compatible (LGE, 60UH6550-UB, wireless)")]
    [InlineData("Mozilla/5.0 (Linux; BRAVIA 4K 2015 Build/LMY48E.S265) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.101 Safari/537.36 OPR/28.0.1754.0")]
    [InlineData("Mozilla/5.0 (Linux; U; Linux; ja-jp; DTV; TSBNetTV/T3E01CD.0203.DDD) AppleWebKit/536(KHTML, like Gecko) NX/3.0 (DTV; HTML; R1.0;) DTVNetBrowser/2.2 (000039;T3E01CD;0203;DDD) InettvBrowser/2.2 (000039;T3E01CD;0203;DDD)")]
    [InlineData("Opera/9.80 (Linux armv7l; HbbTV/1.2.1 (; Philips; 40HFL5010T12; ; PHILIPSTV; CE-HTML/1.0 NETTV/4.4.1 SmartTvA/3.0.0 Firmware/004.002.036.135 (PhilipsTV, 3.1.1,)en) ) Presto/2.12.407 Version/12.50")]
    public void Tv(string agent)
    {
        var resolver = MockService.DeviceService(agent);
        Assert.Equal(Device.Tv, resolver.Type);
    }

    #endregion
}