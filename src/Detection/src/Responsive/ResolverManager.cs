// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection;
using Wangkanai.Detection.DependencyInjection.Options;

namespace Wangkanai.Detection.Responsive
{
    public class ResolverManager
    {
        private readonly ResponsiveOptions _options;
        private readonly Device _resolved;

        public ResolverManager(IDeviceResolver resolver, ResponsiveOptions options)
            : this(resolver.Device.Type, options) { }

        public ResolverManager(Device resolved, ResponsiveOptions options)
        {
            if (options is null)
                throw new ResponsiveMiddlewareOptionArgumentNullException(nameof(options));

            _resolved = resolved;
            _options = options;
        }

        public Device Device => Default(_resolved);

        private Device Default(Device type)
        {
            if (type is Device.Mobile)
                return _options.DefaultMobile;
            if (type is Device.Tablet)
                return _options.DefaultTablet;
            if (type is Device.Desktop)
                return _options.DefaultDesktop;

            return type;
        }
    }
}
