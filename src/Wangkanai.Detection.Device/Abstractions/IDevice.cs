// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    /// <summary>
    /// IDevice is the interface result of the <see cref="DeviceResolver"/>.
    /// </summary>
    public interface IDevice
    {
        DeviceType Type { get; set; }
        bool Crawler { get; set; }
    }
}