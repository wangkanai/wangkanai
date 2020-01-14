// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Collections
{
    public class Firefox : BrowserFactory
    {
        private readonly string _agent;

        public Firefox(string agent)
        {
            _agent = agent.ToLower();
            var firefox = Browser.Firefox.ToString().ToLower();

            if (_agent.Contains(firefox))
            {
                Version = GetVersion(_agent, firefox);
                Type = Browser.Firefox;
            }
        }
    }
}
