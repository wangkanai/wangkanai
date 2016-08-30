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
        public string Maker { get; set; }
    }
    public class Device
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public DeviceType Type { get; set; }
        public PointingMethod Pointing { get; set; }
    }

    public class Engine
    {

    }

    public class Platform
    {

    }

    public class Crawler
    {

    }

    public class Feature
    {

    }
}