// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public class ResponsiveMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ResponsiveOptions _options;

        public ResponsiveMiddleware(RequestDelegate next, IOptions<ResponsiveOptions> options)
        {
            if (next is null)
                throw new ResponsiveMiddlewareNextArgumentNullException(nameof(next));
            if (options is null)
                throw new ResponsiveMiddlewareOptionArgumentNullException(nameof(options));

            _next = next;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context, IDeviceResolver resolver)
        {
            if (context is null)
                throw new ResponsiveMiddlewareInvokeArgumentNullException(nameof(context));

            var detection = new ResolverManager(resolver, _options);
            //var cookie = new CookieManager(context);
            //var preference = new UserPreference(detection, cookie);

            //var preference = new PreferenceManager();

            // need return detect and preferred for the ViewLocation
            context.SetDevice(detection.Device);

            await _next(context);
        }
    }
}
