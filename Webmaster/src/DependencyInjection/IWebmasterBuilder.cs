// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Webmaster builder interface
/// </summary>
public interface IWebmasterBuilder
{
    /// <summary>
    /// Gets the <see cref="IServiceCollection" /> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection" /> services are attached to.
    /// </value>
    IServiceCollection Services { get; }
}