// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Hosting;

public sealed class ResponsiveAttribute : Attribute, IResponsiveMetadata
{
    public ResponsiveAttribute(Device device)
    {
        Device = device;
    }

    public Device Device { get; }
}

public interface IResponsiveMetadata
{
    Device Device { get; }
}