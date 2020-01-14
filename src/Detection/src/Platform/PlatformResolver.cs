// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    public class PlatformResolver : BaseResolver, IPlatformResolver
    {
        public IPlatformFactory Platform => _platform;

        private readonly IPlatformFactory _platform;

        public PlatformResolver(IUserAgentService service) : base(service)
        {
            if (service is null)
                throw new System.ArgumentNullException(nameof(service));
        }
    }
}
