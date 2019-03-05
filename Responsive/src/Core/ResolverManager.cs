// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public class ResolverManager : IDeviceManager
    {
        private readonly ResponsiveOptions _options;
        private readonly DeviceType _resolved;

        public ResolverManager(IDeviceResolver resolver, ResponsiveOptions options)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            _resolved = resolver.Device.Type;
            _options = options;
        }
        public ResolverManager(DeviceType resolved, ResponsiveOptions options)
        {
            _resolved = resolved;
            _options = options;
        }

        public DeviceType Device => _options.Default(_resolved);
    }
}
