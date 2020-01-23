// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Responsive;
using Wangkanai.Detection.Services;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsiveMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponsiveMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context, IResponsiveService responsive)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            //var cookie = new CookieManager(context);
            //var preference = new UserPreference(detection, cookie);

            //var preference = new PreferenceManager();

            // need return detect and preferred for the ViewLocation
            context.SetDevice(responsive.View);

            await _next(context);
        }
    }
}
