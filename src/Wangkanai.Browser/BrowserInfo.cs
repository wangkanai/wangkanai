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

        public BrowserInfo(UserAgent agent)
        {
            UserAgent = agent;
        }

        public BrowserInfo(UserAgent agent, Browser browser, Device device, Engine engine, Platform platform)
            : this(agent)
        {
            Browser = browser;
            Device = device;
            Engine = engine;
            Platform = platform;
        }
    }
}