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
        public ClientInfo(IDetectionService service, IDevice device, IBrowser browser)
        {
           if(service == null) throw new ArgumentNullException(nameof(service));
            UserAgent = service.UserAgent;
            Device = device;
            Browser = browser;
        }

        
    }
}
