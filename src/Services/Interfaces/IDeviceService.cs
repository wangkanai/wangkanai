// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>
/// Provides the APIs for query client <see cref="Device"/>.
/// </summary>
public interface IDeviceService
{
    /// <summary>
    /// Gets the <see cref="Device"/> of the request client.
    /// </summary>
    public Device Type { get; }
}