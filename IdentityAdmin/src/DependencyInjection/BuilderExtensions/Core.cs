// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using Wangkanai.IdentityAdmin;
using Wangkanai.IdentityAdmin.Configuration;
using Wangkanai.IdentityAdmin.Internal;
using Wangkanai.IdentityAdmin.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CoreCollectionExtensions
    {
        public static IIdentityAdminBuilder AddRequiredPlatformServices(this IIdentityAdminBuilder builder)
        {
            // Hosting doesn't add IHttpContextAccessor by default
            builder.Services.AddHttpContextAccessor();

            // Add IdentityAdmin Options
            builder.Services.AddOptions();
            builder.Services.AddSingleton(
                resolver => resolver.GetRequiredService<IOptions<IdentityAdminOptions>>().Value);
            
            return builder;
        }

        public static IIdentityAdminBuilder AddCoreServices(this IIdentityAdminBuilder builder)
        {
            builder.Services.TryAddSingleton<IdentityAdminService, IdentityAdminService>();
            
            return builder;
        }

        public static IIdentityAdminBuilder AddMarkerService(this IIdentityAdminBuilder builder)
        {
            builder.Services.TryAddSingleton<IdentityAdminMarkerService, IdentityAdminMarkerService>();

            return builder;
        }
    }
}