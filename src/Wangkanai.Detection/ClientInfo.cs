// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class ClientInfo : IClientInfo
    {
        public IUserAgent UserAgent { get; }
        public IDevice Device { get; }
        public IBrowser Browser { get; }
        public IEngine Engine { get; }
        public IPlatform Platform { get; }

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