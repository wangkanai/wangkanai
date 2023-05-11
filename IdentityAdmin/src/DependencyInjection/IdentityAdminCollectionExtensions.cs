// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.IdentityAdmin.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityAdminCollectionExtensions
    {
        public static IIdentityAdminBuilder AddIdentityAdmin<TUser>(this IServiceCollection services, Action<IdentityAdminOptions> setAction)
            => services.Configure(setAction)
                       .AddIdentityAdmin<TUser>();

        public static IIdentityAdminBuilder AddIdentityAdmin<TUser, TRole>(this IServiceCollection services, Action<IdentityAdminOptions> setAction)
            => services.Configure(setAction)
                       .AddIdentityAdmin<TUser, TRole>();

        public static IIdentityAdminBuilder AddIdentityAdmin<TUser>(this IServiceCollection services)
            => services.AddIdentityAdminBuilder(typeof(TUser))
                       .AddCommonServices();

        public static IIdentityAdminBuilder AddIdentityAdmin<TUser, TRole>(this IServiceCollection services)
            => services.AddIdentityAdminBuilder(typeof(TUser), typeof(TRole))
                       .AddCommonServices();

        private static IIdentityAdminBuilder AddCommonServices(this IIdentityAdminBuilder builder)
            => builder.AddRequiredPlatformServices()
                      .AddCoreServices()
                      .AddApiServices()
                      .AddMarkerService();

        internal static IIdentityAdminBuilder AddIdentityAdminBuilder(this IServiceCollection services, Type user)
            => AddIdentityAdminBuilder(services, user, null);

        internal static IIdentityAdminBuilder AddIdentityAdminBuilder(this IServiceCollection services, Type user, Type? role)
            => new IdentityAdminBuilder(user, role, services);
    }
}