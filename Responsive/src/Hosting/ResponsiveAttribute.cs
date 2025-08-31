// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Responsive.Hosting;

public sealed class ResponsiveAttribute : Attribute, IResponsiveMetadata
{
   public ResponsiveAttribute(Device device) => Device = device;

   public Device Device { get; }
}