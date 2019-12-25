// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
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
            : this(resolver.Device.Type, options) { }

        public ResolverManager(DeviceType resolved, ResponsiveOptions options)
        {
            if (options == null)
                throw new ResponsiveMiddlewareOptionArgumentNullException(nameof(options));

            _resolved = resolved;
            _options = options;
        }

        public DeviceType Device => Default(_resolved);

        private DeviceType Default(DeviceType type)
        {
            if (type == DeviceType.Mobile) return _options.View.DefaultMobile;
            if (type == DeviceType.Tablet) return _options.View.DefaultTablet;
            if (type == DeviceType.Desktop) return _options.View.DefaultDesktop;

            return type;
        }
    }
}
