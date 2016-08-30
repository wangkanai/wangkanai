using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser.Abstractions
{
    public interface IBrowserBuilder
    {
        IServiceCollection Services { get; }
    }
}
