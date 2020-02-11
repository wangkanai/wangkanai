using System;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    public sealed class ResponsiveAttribute : Attribute, IResponsiveMetadata
    {
        public ResponsiveAttribute(Device device)
        {
            Device = device;
        }

        public Device Device { get; }
    }
}