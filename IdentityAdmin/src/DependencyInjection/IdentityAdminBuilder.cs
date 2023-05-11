// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class IdentityAdminBuilder : IIdentityAdminBuilder
    {
        /// <summary>
        /// Creates a new instanace of <see cref="IdentityAdminBuilder"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
        public IdentityAdminBuilder(Type? user, IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            UserType = user ?? throw new ArgumentNullException(nameof(user));
        }

        /// <summary>
        /// Creates a new instanace of <see cref="IdentityAdminBuilder"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
        public IdentityAdminBuilder(Type? user, Type? role, IServiceCollection services)
            : this(user, services)
        {
            RoleType = role ?? throw new ArgumentNullException(nameof(role));
        }

        public Type? UserType { get; }

        public Type? RoleType { get; }

        /// <summary>
        /// Gets the <see cref="IServiceCollection" /> services are attached to.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection" /> services are attached to.
        /// </value>
        public IServiceCollection Services { get; }
    }
}