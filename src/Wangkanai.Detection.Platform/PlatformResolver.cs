// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wangkanai.Detection
{
    public class PlatformResolver : BaseResolver, IPlatformResolver
    {
        public IPlatform Platform => _platform;

        private readonly IPlatform _platform;

        public PlatformResolver(IUserAgentService service) : base(service)
        {

        }
    }
}
