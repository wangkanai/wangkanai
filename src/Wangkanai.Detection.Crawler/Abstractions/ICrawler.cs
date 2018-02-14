namespace Wangkanai.Detection
{
    public interface ICrawler
    {
        string Name { get; set; }
        IVersion Version { get; set; }
    }
}
