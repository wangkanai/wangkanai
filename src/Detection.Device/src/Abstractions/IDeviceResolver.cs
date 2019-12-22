// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    /// <summary>
    /// Get device resolver to generate the device result
    /// </summary>
    public interface IDeviceResolver : IResolver
    {
        IDevice Device { get; }
    }
}
