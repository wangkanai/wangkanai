// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Webmaster.Builders;

/// <summary>
/// Helper functions for configuring Webmaster services.
/// </summary>
public class WebmasterBuilder : IWebmasterBuilder
{
    /// <summary>
    /// Creates a new instance of <see cref="WebmasterBuilder"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
    public WebmasterBuilder(IServiceCollection services)
        => Services = services ?? throw new ArgumentNullException(nameof(services));

    /// <summary>
    /// Gets the <see cref="IServiceCollection" /> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection" /> services are attached to.
    /// </value>
    public IServiceCollection Services { get; private set; }
}