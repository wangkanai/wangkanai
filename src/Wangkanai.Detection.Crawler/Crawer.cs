namespace Wangkanai.Detection
{
    public class Crawer : ICrawler
    {
        public string Name { get; set; }
        public IVersion Version { get; set; }

        public Crawer() { }
        public Crawer(string name)
        {
            Name = name;
        }
        public Crawer(string name, string version) : this(name)
        {
            Version = new Version(version);
        }
    }
}