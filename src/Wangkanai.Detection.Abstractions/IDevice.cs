// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public interface IDevice
    {
        DeviceType Type { get; set; }
        bool IsCrawler { get; set; }
    }
}