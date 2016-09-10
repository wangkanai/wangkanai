// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Browser.Resolvers
{
    public sealed class DeviceResolver {
        private readonly IClientService _service;
        public DeviceResolver(IClientService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service);

            _service = service;
        }
        private Device Resolve()
        {
            return new Device();
        }
    }
}