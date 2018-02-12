// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DeviceNotFoundException : ArgumentException
    {
        private string _invalidDeviceName; // unrecognized device name
        private static string DefaultMessage => "Device Not Supported";
        public virtual string InvalidDeviceName => _invalidDeviceName;

        public DeviceNotFoundException()
            : base(DefaultMessage) { }
        public DeviceNotFoundException(string message)
            : base(message) { }
        public DeviceNotFoundException(string paramName, string message)
            : base(message, paramName) { }
        public DeviceNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public DeviceNotFoundException(string message, string invalidDeviceName, Exception innerException)
            : base(message, innerException)
        {
            _invalidDeviceName = invalidDeviceName;
        }

        public DeviceNotFoundException(string paramName, string invalidDeviceName, string message)
            : base(message, paramName)
        {
            _invalidDeviceName = invalidDeviceName;
        }
        public override string Message
        {
            get
            {
                var s = base.Message;
                if (_invalidDeviceName != null)
                    return s + Environment.NewLine + InvalidDeviceName;

                return s;
            }
        }
    }
}