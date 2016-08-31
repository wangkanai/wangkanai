using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    public interface IBrowserBuilder
    {
        IServiceCollection Services { get; }
    }
}
