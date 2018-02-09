namespace Wangkanai.Detection.Collections
{
    public class InternetExplorer : Browser
    {
        private readonly string _agent;

        public InternetExplorer(string agent)
        {
            _agent = agent.ToLower();

            var ie10 = "msie";
            if (_agent.Contains(ie10))
            {
                var first = _agent.IndexOf(ie10);
                var cut = _agent.Substring(first + ie10.Length + 1);
                var version = cut.Substring(0, cut.IndexOf(';'));
                Version = new Version(version);
                Type = BrowserType.IE;
            }

            if(_agent.Contains("ie 11.0"))
            {
                Type = BrowserType.IE;
                Version = new Version("11.0");
            }
        }
    }
}
