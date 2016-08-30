// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    public class BrowserInfo
    {
        public UserAgent UserAgent { get; }
        public Browser Browser { get; }
        public Device Device { get; }
        public Engine Engine { get; }
        public Platform Platform { get; }

        public BrowserInfo() { }
        public BrowserInfo(UserAgent agent) { }

        public BrowserInfo(UserAgent agent, Browser browser, Device device, Engine engine, Platform platform)
            : this(agent)
        {
            Browser = browser;
            Device = device;
            Engine = engine;
            Platform = platform;
        }
    }

    public class Browser
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public BrowserType Type { get; set; }
        public byte Bits { get; set; }
        public string Version { get; set; }
    }

    public class Device
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public DeviceType Type { get; set; }
        public PointingMethod Pointing { get; set; }
    }

    public class Platform
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public byte Bits { get; set; }
        public PlatformType Type { get; set; }
    }

    public class Crawler
    {
        public string Name { get; set; }
    }

    public class Feature
    {
        public bool Frames { get; set; }
        public bool Iframes { get; set; }
        public bool Cookie { get; set; }
        public bool Javascript { get; set; }
        public bool Vbscript { get; set; }
        public bool Javaapplets { get; set; }
        public bool ActiveX { get; set; }
    }
}