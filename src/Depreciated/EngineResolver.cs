// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection
{
    [Obsolete]
    public class EngineResolver : BaseResolver, IEngineResolver
    {
        public IEngineFactory Engine => _engine;

        private readonly IEngineFactory _engine;

        public EngineResolver(IUserAgentService service) : base(service)
        {
            if (service is null)
                throw new System.ArgumentNullException(nameof(service));
        }
    }
}
