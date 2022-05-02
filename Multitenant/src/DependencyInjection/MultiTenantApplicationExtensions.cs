// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;

using Wangkanai.Multitenant.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class MultiTenantApplicationExtensions
{
    public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder app)
    {
        app = Check.NotNull(app);
        
        app.VerifyMarkerIsRegistered<MultiTenantMarkerService>();
        app.VerifyEndpointRoutingMiddlewareIsNotRegistered(UseMultiTenant);
        
        return app;
    }
}