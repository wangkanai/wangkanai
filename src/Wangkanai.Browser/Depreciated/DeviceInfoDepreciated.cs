// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Wangkanai.Browser.Depreciated;

namespace Wangkanai.Browser
{
    public class DeviceInfoDepreciated
    {
        public string Name => Device.ToString().ToLowerInvariant();
        public DeviceTypes Device { get; }
        public DeviceInfoDepreciated(string name)
        {
            if(name==null) throw new ArgumentNullException(nameof(name));
            Device = GetDeviceInfo(name);
        }

        public DeviceInfoDepreciated(DeviceTypes types)
        {
            Device = types;            
        }

        private DeviceTypes GetDeviceInfo(string name)
        {
            DeviceTypes device;
            if(!Enum.TryParse(name, true, out device))
                throw new DeviceNotFoundException("name", name, "Device Not Found");

            return device;
        }
    }
}