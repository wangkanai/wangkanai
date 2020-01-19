// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Collections
{
    public class Edge : BrowserFactory
    {
        private readonly string _agent;

        public Edge(string agent)
        {
            _agent = agent.ToLower();
            var edge = Browser.Edge.ToString().ToLower();

            if (_agent.Contains(edge))
            {
                Version = GetVersion(_agent, edge);
                Type = Browser.Edge;
            }
        }
    }
}
