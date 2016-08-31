using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{
    public interface IClientBuilder
    {
        IServiceCollection Services { get; }
    }
}
