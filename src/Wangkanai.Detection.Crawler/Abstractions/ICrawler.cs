namespace Wangkanai.Detection
{
    public interface ICrawler
    {
        string Name { get; set; }
        CrawlerType Type { get; set; }
        IVersion Version { get; set; }
    }
}
