namespace Wangkanai.Detection
{
    public class BrowserResolver : IBrowserResolver
    {
        public IBrowser Browser { get; }
        public IUserAgent UserAgent { get; }
    }
}