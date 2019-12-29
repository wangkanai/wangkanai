// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Collections
{
    public class Chrome : BrowserFactory
    {
        private readonly string _agent;

        public Chrome(string agent)
        {
            _agent = agent.ToLower();
            var chrome = Browser.Chrome.ToString().ToLower();

            if (_agent.Contains(chrome))
            {
                var first = _agent.IndexOf(chrome);
                string cut;
                try
                {
                    cut = _agent.Substring(first + chrome.Length + 1);
                }
                catch
                {
                    cut = _agent.Substring(first + chrome.Length);
                }
                var version = cut.Substring(0, cut.Contains(" ") ? cut.IndexOf(' ') : cut.Length);
                Version = GetVersion(_agent, chrome);
                Type = Browser.Chrome;
            }
        }
    }
}
