﻿// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.IdentityAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityAdminApiCollectionExtensions
    {
        public static IIdentityAdminBuilder AddApiServices(this IIdentityAdminBuilder builder)
        {
            if (builder is null)
                throw new System.ArgumentNullException(nameof(builder));
            
            // What am i going to add?
        
            return builder;
        }
    }
}