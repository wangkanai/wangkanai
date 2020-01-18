// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUserAgentService _userAgentService;

        public DeviceService(IUserAgentService userAgentService)
        {
            _userAgentService = userAgentService;
        }
    }
}
