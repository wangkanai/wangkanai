// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Wangkanai.Detection.Collections
{
    public class Opera : Browser
    {
        private readonly string _agent;

        public Opera(string agent)
        {
            _agent = agent.ToLower();
            var opera12 = BrowserType.Opera.ToString().ToLower();

            if(_agent.Contains(opera12))
            {
                Version = GetVersion(_agent, "version");
                Type = BrowserType.Opera;
            }

            var opera15 = "opr";
            if (_agent.Contains(opera15))
            {
                Version = GetVersion(_agent, opera15);
                Type = BrowserType.Opera;
            }
        }
    }
}
