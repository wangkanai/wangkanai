namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Helper functions for configuring analytics services.
    /// </summary>
    public interface IAnalyticsBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        IServiceCollection Services { get; }
    }
}