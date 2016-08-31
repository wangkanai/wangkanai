// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    public class ClientInfo
    {
        public UserAgent UserAgent { get; }
        public Browser Browser { get; }
        public Device Device { get; }
        public Engine Engine { get; }
        public Platform Platform { get; }

        public ClientInfo() { }

        public ClientInfo(UserAgent agent)
        {
            UserAgent = agent;
        }

        public ClientInfo(UserAgent agent, Browser browser, Device device, Engine engine, Platform platform)
            : this(agent)
        {
            Browser = browser;
            Device = device;
            Engine = engine;
            Platform = platform;
        }
    }
}