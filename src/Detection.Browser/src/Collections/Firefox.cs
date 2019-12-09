// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Wangkanai.Detection.Collections
{
    public class Firefox : Browser
    {
        private readonly string _agent;

        public Firefox(string agent)
        {
            _agent = agent.ToLower();
            var firefox = BrowserType.Firefox.ToString().ToLower();

            if(_agent.Contains(firefox))
            {
                Version = GetVersion(_agent, firefox);
                Type = BrowserType.Firefox;
            }
        }
    }
}
