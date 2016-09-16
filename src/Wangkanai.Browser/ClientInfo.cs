// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    public class ClientInfo : IClientInfo
    {
        public UserAgent UserAgent { get; }
        public Device Device { get; }
        public Browser Browser { get; }
        public Engine Engine { get; }
        public Platform Platform { get; }

        private readonly IClientService _service;

        public ClientInfo(IClientService service, IDeviceResolver deviceResolver)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (deviceResolver == null) throw new ArgumentNullException(nameof(deviceResolver));

            _service = service;
            UserAgent = service.UserAgent;
            Device = deviceResolver.Device;
            Browser = new Browser();   // waiting for implementation
            Engine = new Engine();     // waiting for implementation
            Platform = new Platform(); // waiting for implementation  
        }

        //public ClientInfo(string useragent)
        //{
        //    Useragent = new UserAgent(useragent);
        //}

        //public ClientInfo(UserAgent useragent)
        //{
        //    Useragent = useragent;
        //}

        //public ClientInfo(UserAgent useragent, Browser browser, Device device, Engine engine, Platform platform)
        //    : this(useragent)
        //{
        //    Browser = browser;
        //    Device = device;
        //    Engine = engine;
        //    Platform = platform;
        //}
    }
}