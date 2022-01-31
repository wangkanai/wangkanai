using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Helper functions for configuring analytics services.
    /// </summary>
    public class AnalyticsBuilder : IAnalyticsBuilder
    {
        /// <summary>
        /// Creates a new instance of <see cref="AnalyticsBuilder"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
        public AnalyticsBuilder(IServiceCollection services)
            => Services = services ?? throw new ArgumentNullException(nameof(services));

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> services are attached to.
        /// </summary>
        public IServiceCollection Services { get; }
    }
}