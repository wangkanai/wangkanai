// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    public class ClientInfo
    {
        public UserAgent Useragent { get; } = new UserAgent();
        public Browser Browser { get; } = new Browser();
        public Device Device { get; } = new Device();
        public Engine Engine { get; } = new Engine();
        public Platform Platform { get; } = new Platform();

        public ClientInfo() { }

        public ClientInfo(string useragent)
        {
            Useragent = new UserAgent(useragent);
        }

        public ClientInfo(UserAgent useragent)
        {
            Useragent = useragent;
        }

        public ClientInfo(UserAgent useragent, Browser browser, Device device, Engine engine, Platform platform)
            : this(useragent)
        {
            Browser = browser;
            Device = device;
            Engine = engine;
            Platform = platform;
        }
    }
}