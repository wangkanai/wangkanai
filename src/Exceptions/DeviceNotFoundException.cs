// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Wangkanai.Detection.Test")]

namespace Wangkanai.Detection
{
    public class DeviceNotFoundException : ArgumentException
    {
        private readonly string _invalidDeviceName; // unrecognized device name

        public DeviceNotFoundException() : base(DefaultMessage) { }

        public DeviceNotFoundException(string message) : base(message) { }

        public DeviceNotFoundException(string paramName, string message) : base(message, paramName) { }

        public DeviceNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public DeviceNotFoundException(string message, string invalidDeviceName, Exception innerException) : base(message, innerException)
            => _invalidDeviceName = invalidDeviceName;

        public DeviceNotFoundException(string paramName, string invalidDeviceName, string message) : base(message, paramName)
            => _invalidDeviceName = invalidDeviceName;

        private static string DefaultMessage => "Device Not Supported";
        public virtual string InvalidDeviceName => _invalidDeviceName;

        public override string Message
        {
            get
            {
                var s = base.Message;
                return _invalidDeviceName != null ? s + Environment.NewLine + InvalidDeviceName : s;
            }
        }
    }
}
