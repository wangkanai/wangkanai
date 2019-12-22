// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Collections
{
    public class Safari : Browser
    {
        private readonly string _agent;

        public Safari(string agent)
        {
            _agent = agent.ToLower();
            var safari = BrowserType.Safari.ToString().ToLower();

            if (_agent.Contains(safari))
            {
                Version = GetVersion(_agent, safari);
                Type = BrowserType.Safari;
            }
        }
    }
}
