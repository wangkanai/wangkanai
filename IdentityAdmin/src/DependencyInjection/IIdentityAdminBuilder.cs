// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IIdentityAdminBuilder
    {
        IServiceCollection Services { get; }

        Type? UserType { get; }

        Type? RoleType { get; }
    }
}