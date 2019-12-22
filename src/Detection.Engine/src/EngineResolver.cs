// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class EngineResolver : BaseResolver, IEngineResolver
    {
        public IEngine Engine => _engine;

        private readonly IEngine _engine;

        public EngineResolver(IUserAgentService service) : base(service)
        {
        }
    }
}
