using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    public interface IBrowserResolver : IResolver
    {
        IBrowser Browser { get; }
    }
}