namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Helper functions for configuring responsive services
/// </summary>
public interface IResponsiveBuilder
{
    /// <summary>
    /// Gets the <see cref="IServiceCollection" /> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection" /> services are attached to.
    /// </value>
    IServiceCollection Services { get; }
}