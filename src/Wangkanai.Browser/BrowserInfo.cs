// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    public class BrowserInfo
    {
        public UserAgent UserAgent { get; }
        public Device Device { get; }
        public Engine Engine { get; }
        public Platform Platform { get; }
        public bool IsCrawler { get; }
        public bool IsMobile => Device.Type == DeviceType.Mobile;
        public bool IsTablet => Device.Type == DeviceType.Tablet;

        public BrowserInfo() { }

        public BrowserInfo(UserAgent agent, Device device, Engine engine, Platform platform)
        {
            UserAgent = agent;
            Device = device;
            Engine = engine;
            Platform = platform;
        }
    }

    public class Device
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public DeviceType Type { get; set; }
    }
    public enum DeviceType
    {
        Desktop,
        Tablet,
        Mobile
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