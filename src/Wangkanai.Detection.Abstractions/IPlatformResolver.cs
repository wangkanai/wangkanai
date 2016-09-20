using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    public interface IPlatformResolver : IResolver
    {
        IPlatform Platform { get; }
    }
}