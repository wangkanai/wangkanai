// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Collections
{
    public class Edge : Browser
    {
        private readonly string _agent;

        public Edge(string agent)
        {
            _agent = agent.ToLower();
            var edge = BrowserType.Edge.ToString().ToLower();

            if (_agent.Contains(edge))
            {
                Version = GetVersion(_agent, edge);
                Type = BrowserType.Edge;
            }
        }
    }
}
