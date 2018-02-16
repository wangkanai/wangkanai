namespace Wangkanai.Detection
{
    public interface IDetection
    {
        IBrowser Browser { get; }
        ICrawler Crawler { get; }
        IDevice Device { get; }
        IEngine Engine { get; }
        IPlatform Platform { get; }
        IUserAgent UserAgent { get; }
    }
}