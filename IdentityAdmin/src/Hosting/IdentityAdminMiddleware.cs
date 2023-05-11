// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Wangkanai.IdentityAdmin.Configuration;

namespace Wangkanai.IdentityAdmin.Hosting
{
    public class IdentityAdminMiddleware
    {
        private readonly RequestDelegate                  _next;
        private readonly IdentityAdminOptions             _options;
        private readonly ILogger<IdentityAdminMiddleware> _logger;

        public IdentityAdminMiddleware(RequestDelegate next, IdentityAdminOptions options, ILogger<IdentityAdminMiddleware> logger)
        {
            _next    = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger  = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (_options.Demo)
                _logger.LogDebug("IdentityAdmin is in Demo mode.");

            await _next(context);
        }
    }
}