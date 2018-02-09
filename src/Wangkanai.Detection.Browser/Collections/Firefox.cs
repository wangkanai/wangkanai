using System;
using System.Collections.Generic;
using System.Text;

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
                var first = _agent.IndexOf(firefox);
                var version = _agent.Substring(first + firefox.Length + 1);
                //var version = cut.Substring(0, cut.IndexOf(' '));
                Version = new Version(version);
                Type = BrowserType.Firefox;
            }
        }
    }
}
