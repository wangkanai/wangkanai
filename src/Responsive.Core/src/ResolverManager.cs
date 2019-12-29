// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public class ResolverManager
    {
        private readonly ResponsiveOptions _options;
        private readonly Device _resolved;

        public ResolverManager(IDeviceResolver resolver, ResponsiveOptions options)
            : this(resolver.Device.Type, options) { }

        public ResolverManager(Device resolved, ResponsiveOptions options)
        {
            if (options == null)
                throw new ResponsiveMiddlewareOptionArgumentNullException(nameof(options));

            _resolved = resolved;
            _options = options;
        }

        public Device Device => Default(_resolved);

        private Device Default(Device type)
        {
            if (type == Device.Mobile) return _options.View.DefaultMobile;
            if (type == Device.Tablet) return _options.View.DefaultTablet;
            if (type == Device.Desktop) return _options.View.DefaultDesktop;

            return type;
        }
    }
}
