// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    public class ClientInfo : IClientInfo
    {
        public IUserAgent UserAgent { get; }
        public IDevice Device { get; }
        public IBrowser Browser { get; }
        public IEngine Engine { get; }
        public IPlatform Platform { get; }
        public ClientInfo(IDetectionService service, IDevice device, IBrowser browser, IEngine engine, IPlatform platform)
        {
           if(service == null) throw new ArgumentNullException(nameof(service));
            UserAgent = service.UserAgent;
            Device = device;
            Browser = browser;
            Engine = engine;
            Platform = platform;
        }
    }
}
