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
                Type = BrowserType.Safari;
                string version = null;
                try
                {
                    version = _agent.Substring(_agent.IndexOf("version/") + "version/".Length);
                    version = version.Substring(0, version.IndexOf(" "));
                    Version = version.ToVersion();
                }
                catch
                {
                }
            }
        }
    }
}
