using System;
using System.Collections.Generic;
using System.Text;

namespace Wangkanai.Detection.Collections
{
    internal class Firefox : BrowserFactory
    {
        private readonly string _agent;

        public Firefox(string agent)
        {
            _agent = agent.ToLower();
            var firefox = BrowserType.Firefox.ToString().ToLower();

            if(_agent.Contains(firefox))
            {
                var first = _agent.IndexOf(firefox);
                var version = _agent.Substring(first + firefox.Length + 1);
                Version = new Version(version);
                Valid = true;
            }
        }
    }
}
