using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    public interface IEngineResolver : IResolver
    {
        IEngine Engine { get; }
    }
}